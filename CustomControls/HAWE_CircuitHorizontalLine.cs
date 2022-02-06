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
            SetFlowArrows();
        }

        #endregion

        #region Private fields

        private double _arrowWidthToHeightRatio = 1.5;
        private double _arrowStartOffset = 0;

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

        private void SetFlowArrows()
        {
            if (MainCanvas == null)
                return;

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
                Canvas.SetTop(hAWE_FlowArrow, 0);
                Canvas.SetLeft(hAWE_FlowArrow, (correctedArrowWidth * currentArrowCount) + _arrowStartOffset);
                MainCanvas.Children.Add(hAWE_FlowArrow);

                var initialDuration = (canvasWidth - (_arrowStartOffset + (currentArrowCount * correctedArrowWidth))) * 2 / canvasWidth;
                TimeSpan finalMovementBeginTime = TimeSpan.FromSeconds(initialDuration);

                var initialMovement = new DoubleAnimation()
                {
                    From = (correctedArrowWidth * currentArrowCount) + _arrowStartOffset,
                    To = canvasWidth - correctedArrowWidth,
                    BeginTime = new TimeSpan(0, 0, 0, 0),
                    Duration = TimeSpan.FromSeconds(initialDuration),
                    RepeatBehavior = new RepeatBehavior(1),
                };

                var finalRepeatMovement = new DoubleAnimation()
                {
                    From = _arrowStartOffset,
                    To = canvasWidth - correctedArrowWidth,
                    BeginTime = finalMovementBeginTime,
                    Duration = TimeSpan.FromSeconds(2),
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
            leftBorder.BorderThickness = new Thickness(1,1,0,1);
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
        }

        #endregion

        #region Dependancy Properties

        #region Flow Color

        /// <summary>
        /// Label
        /// </summary>
        public Brush FlowColor
        {
            get { return (Brush)GetValue(FlowColorProperty); }
            set { SetValue(FlowColorProperty, value); }
        }

        /// <summary>
        /// LabelProperty
        /// </summary>
        public static readonly DependencyProperty FlowColorProperty =
            DependencyProperty.Register("FlowColor", typeof(Brush), typeof(HAWE_CircuitHorizontalLine), new FrameworkPropertyMetadata(Brushes.SteelBlue));

        #endregion

        #region IsFlowLeftToRight

        /// <summary>
        /// IsFlowLeftToRight
        /// </summary>
        public bool? IsFlowLeftToRight
        {
            get { return (bool?)GetValue(IsFlowLeftToRightProperty); }
            set { SetValue(IsFlowLeftToRightProperty, value); }
        }

        /// <summary>
        /// IsFlowLeftToRightProperty
        /// </summary>
        public static readonly DependencyProperty IsFlowLeftToRightProperty =
            DependencyProperty.Register("IsFlowLeftToRight", typeof(bool?), typeof(HAWE_CircuitHorizontalLine), new FrameworkPropertyMetadata(null));

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
