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
            Canvas = GetTemplateChild("PART_MainCanvas") as Canvas;
        }

        private void SetControls()
        {
            SetFlowArrows();
        }

        #endregion

        #region Private fields

        private const double _arrowWidthToHeightRatio = 21 / 14;

        #endregion

        #region PART_OuterBorder

        private Canvas _canvas;
        private Canvas Canvas
        {
            get { return _canvas; }
            set
            {
                //if (_canvas != null)
                //{
                //    _border.MouseMove -= PART_DisplayGrid_MouseMove;
                //    _border.DragOver -= PART_DisplayGrid_DragOver;
                //    _border.Drop -= PART_DisplayGrid_Drop;
                //}

                _canvas = value;

                if (_canvas != null)
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
            if (Canvas == null)
                return;

            double canvasActualWidth = Canvas.ActualWidth;
            double canvasActualHeight = Canvas.ActualHeight;

            if ()
                return;

            double arrowWidth = canvasActualHeight * _arrowWidthToHeightRatio;


            //_matrixBorderNames = new List<string>();

            //for (int i = 0; i < MatrixSize; i++)
            //{
            //    ColumnDefinition gridCol = new ColumnDefinition();
            //    RowDefinition gridRow = new RowDefinition();
            //    gridRow.Height = new GridLength(1, GridUnitType.Star);
            //    Grid.RowDefinitions.Add(gridRow);
            //    Grid.ColumnDefinitions.Add(gridCol);
            //}

            //for (int rowNumber = 0; rowNumber < MatrixSize; rowNumber++)
            //{
            //    for (int columnNumber = 0; columnNumber < MatrixSize; columnNumber++)
            //    {
            //        Border border = new Border();
            //        border.Background = Brushes.Transparent;
            //        border.BorderBrush = Brushes.LightGray;
            //        border.BorderThickness = new Thickness(1);
            //        border.Margin = new Thickness(0);
            //        border.Name = $"MatrixElementBorder_{rowNumber}_{columnNumber}";
            //        _matrixBorderNames.Add(border.Name);
            //        Grid.SetRow(border, rowNumber);
            //        Grid.SetColumn(border, columnNumber);
            //        Grid.Children.Add(border);
            //    }
            //}
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



        #endregion
    }
}
