using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:CustomControls;assembly=CustomControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class HAWE_CircuitHorizontalLine : ProgressBar
    {
        static HAWE_CircuitHorizontalLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HAWE_CircuitHorizontalLine), new FrameworkPropertyMetadata(typeof(HAWE_CircuitHorizontalLine)));
        }

        #region OnApplyTemplate

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GetControls();

            SetControls();
        }

        private void GetControls()
        {
            MainCanvas = GetTemplateChild("PART_MainCanvas") as Canvas;
        }

        private void SetControls()
        {
            if (IsFlowActive)
                SetWithFlowArrows();
            else
                SetWithoutFlowArrows();
        }

        #endregion

        #region Private fields

        private double _arrowWidthToHeightRatio = 1.5;
        private double arrowSpeed = 30; // [mm/s]

        #endregion

        #region PART_OuterBorder

        private Canvas _mainCanvas;
        private Canvas MainCanvas
        {
            get { return _mainCanvas; }
            set
            {
                //if (_canvas != null)
                //{
                //    _border.MouseMove -= PART_DisplayGrid_MouseMove;
                //    _border.DragOver -= PART_DisplayGrid_DragOver;
                //    _border.Drop -= PART_DisplayGrid_Drop;
                //}

                _mainCanvas = value;

                if (_mainCanvas != null)
                {
                    //_border.AllowDrop = true;
                    //_border.MouseMove += PART_DisplayGrid_MouseMove;
                    //_border.DragOver += PART_DisplayGrid_DragOver;
                    //_border.Drop += PART_DisplayGrid_Drop;
                }
            }
        }

        private void SetWithFlowArrows()
        {
            if (MainCanvas == null)
                return;

            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.Width;
            double canvasHeight = MainCanvas.Height;

            if (canvasWidth <= 0 || canvasHeight <= 0)
                return;

            if (!IsValidDoubleValue(canvasWidth) || !IsValidDoubleValue(canvasHeight))
                return;

            double correctedArrowHeight = canvasHeight;
            double correctedArrowWidth = canvasHeight * _arrowWidthToHeightRatio;
            int totalArrowCount = Convert.ToInt32(Math.Floor(canvasWidth / correctedArrowWidth));

            if (totalArrowCount < 1)
                return;

            var storyBoard = new Storyboard();

            for (int currentArrowCount = 0; currentArrowCount < totalArrowCount; currentArrowCount++)
            {
                HAWE_FlowArrow hAWE_FlowArrow = new HAWE_FlowArrow();
                hAWE_FlowArrow.Name = $"HAWE_FlowArrow_{currentArrowCount}";
                hAWE_FlowArrow.Height = correctedArrowHeight;
                hAWE_FlowArrow.Width = correctedArrowWidth;
                if(IsFlowBrushBlue)
                {
                    hAWE_FlowArrow.ArrowBrushOne = Brushes.DarkBlue;
                    hAWE_FlowArrow.ArrowBrushTwo = Brushes.Aquamarine;
                }
                else
                {
                    hAWE_FlowArrow.ArrowBrushOne = Brushes.DarkRed;
                    hAWE_FlowArrow.ArrowBrushTwo = Brushes.Red;
                }
                Canvas.SetTop(hAWE_FlowArrow, 0);
                Canvas.SetLeft(hAWE_FlowArrow, (correctedArrowWidth * currentArrowCount));
                MainCanvas.Children.Add(hAWE_FlowArrow);

                var initialDuration = (canvasWidth - (currentArrowCount * correctedArrowWidth)) / arrowSpeed;
                TimeSpan finalMovementBeginTime = TimeSpan.FromSeconds(initialDuration);

                var initialMovement = new DoubleAnimation()
                {
                    From = (correctedArrowWidth * currentArrowCount),
                    To = canvasWidth - correctedArrowWidth,
                    BeginTime = new TimeSpan(0, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(initialDuration),
                    RepeatBehavior = new RepeatBehavior(1),
                };

                var finalRepeatMovement = new DoubleAnimation()
                {
                    From = 0,
                    To = canvasWidth - correctedArrowWidth,
                    BeginTime = finalMovementBeginTime,
                    Duration = TimeSpan.FromSeconds(canvasWidth / arrowSpeed),
                    RepeatBehavior = RepeatBehavior.Forever,
                };

                Storyboard.SetTarget(initialMovement, hAWE_FlowArrow);
                Storyboard.SetTargetProperty(initialMovement, new PropertyPath(Canvas.LeftProperty));

                Storyboard.SetTarget(finalRepeatMovement, hAWE_FlowArrow);
                Storyboard.SetTargetProperty(finalRepeatMovement, new PropertyPath(Canvas.LeftProperty));

                storyBoard.Children.Add(initialMovement);
                storyBoard.Children.Add(finalRepeatMovement);

            }

            Border leftBorder = new Border();
            leftBorder.Height = this.Height;
            leftBorder.Width = correctedArrowWidth;
            leftBorder.Background = Brushes.Gray;
            leftBorder.BorderBrush = Brushes.Black;
            leftBorder.BorderThickness = new Thickness(1, 1, 0, 1);
            Canvas.SetTop(leftBorder, 0);
            Canvas.SetLeft(leftBorder, 0);
            MainCanvas.Children.Add(leftBorder);

            Border rightBorder = new Border();
            rightBorder.Height = canvasHeight;
            rightBorder.Width = correctedArrowWidth;
            rightBorder.Background = Brushes.Gray;
            rightBorder.BorderBrush = Brushes.Black;
            rightBorder.BorderThickness = new Thickness(0, 1, 1, 1);
            Canvas.SetTop(rightBorder, 0);
            Canvas.SetRight(rightBorder, 0);
            MainCanvas.Children.Add(rightBorder);


            storyBoard.Begin(this);
            MainCanvas.Background = Brushes.White;
        }

        private void SetWithoutFlowArrows()
        {
            if (MainCanvas == null)
                return;

            double canvasWidth = MainCanvas.Width;
            double canvasHeight = MainCanvas.Height;

            if (canvasWidth <= 0 || canvasHeight <= 0)
                return;

            if (!IsValidDoubleValue(canvasWidth) || !IsValidDoubleValue(canvasHeight))
                return;

            MainCanvas.Children.Clear();
            MainCanvas.Background = Brushes.Black;
        }

        #endregion

        #region Dependancy Properties

        #region Flow Color

        /// <summary>
        /// FlowColor
        /// </summary>
        public Brush FlowColor
        {
            get { return (Brush)GetValue(FlowColorProperty); }
            set { SetValue(FlowColorProperty, value); }
        }

        /// <summary>
        /// FlowColorProperty
        /// </summary>
        public static readonly DependencyProperty FlowColorProperty =
            DependencyProperty.Register("FlowColor", typeof(Brush), typeof(HAWE_CircuitHorizontalLine), new FrameworkPropertyMetadata(Brushes.SteelBlue));

        #endregion

        #region IsFlowLeftToRight

        /// <summary>
        /// IsFlowBrushBlue
        /// </summary>
        public bool IsFlowBrushBlue
        {
            get { return (bool)GetValue(IsFlowBrushBlueProperty); }
            set { SetValue(IsFlowBrushBlueProperty, value); }
        }

        /// <summary>
        /// IsFlowBrushBlueProperty
        /// </summary>
        public static readonly DependencyProperty IsFlowBrushBlueProperty =
            DependencyProperty.Register("IsFlowBrushBlue", typeof(bool), typeof(HAWE_CircuitHorizontalLine), new FrameworkPropertyMetadata(true, IsFlowBrushBluePropertyChanged));

        /// <summary>
        /// IsFlowBrushBlue changed
        /// </summary>
        /// <param name="source">HAWE_Freigabe</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        protected static void IsFlowBrushBluePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            HAWE_CircuitHorizontalLine myself = source as HAWE_CircuitHorizontalLine;
            myself.OnApplyTemplate();
        }

        #endregion

        #region IsFlowActive

        /// <summary>
        /// IsFlowActive
        /// </summary>
        public bool IsFlowActive
        {
            get { return (bool)GetValue(IsFlowActiveProperty); }
            set { SetValue(IsFlowActiveProperty, value); }
        }

        /// <summary>
        /// IsFlowActiveProperty
        /// </summary>
        public static readonly DependencyProperty IsFlowActiveProperty =
            DependencyProperty.Register("IsFlowActive", typeof(bool), typeof(HAWE_CircuitHorizontalLine), new FrameworkPropertyMetadata(false, OnIsFlowActivePropertyChanged));

        /// <summary>
        /// IsFlowActive changed
        /// </summary>
        /// <param name="source">HAWE_Freigabe</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        protected static void OnIsFlowActivePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            HAWE_CircuitHorizontalLine myself = source as HAWE_CircuitHorizontalLine;
            myself.OnApplyTemplate();
        }

        #endregion

        #endregion

        #region Private methods

        // checks if double value is not NaN, +ve infinity, or -ve infinity
        private bool IsValidDoubleValue(double value)
        {
            return !Double.IsNaN(value) && !Double.IsInfinity(value) && !Double.IsNegativeInfinity(value);
        }

        #endregion
    }
}
