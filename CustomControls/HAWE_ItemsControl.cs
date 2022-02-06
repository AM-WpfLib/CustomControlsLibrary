using CustomControls.Elements;
using CustomControls.Elements.Interfaces;
using CustomControls.Elements.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class HAWE_ItemsControl : ListBox
    {
        static HAWE_ItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HAWE_ItemsControl), new FrameworkPropertyMetadata(typeof(HAWE_ItemsControl)));
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
            Grid = GetTemplateChild("PART_DisplayGrid") as Grid;
            ItemsPresenter = GetTemplateChild("PART_ItemsPresenter") as ItemsPresenter;
        }

        private void SetControls()
        {
            SetGridMatrix();
        }

        #endregion

        #region Private fields

        private List<string> _matrixBorderNames = new List<string>();

        #endregion

        #region DependancyProperties

        /// <summary>
        /// MatrixSize
        /// </summary>
        public int MatrixSize
        {
            get { return (int)GetValue(MatrixSizeProperty); }
            set { SetValue(MatrixSizeProperty, value); }
        }

        /// <summary>
        /// MatrixSizeProperty
        /// </summary>
        public static readonly DependencyProperty MatrixSizeProperty =
            DependencyProperty.Register("MatrixSize", typeof(int), typeof(HAWE_ItemsControl), new FrameworkPropertyMetadata(3));

        #endregion

        #region PART_DisplayGrid

        // Notes:
        // must get controls defensively as follows to ensure that the program does not crash... because the GetTemplateChild() can return null.
        // must ensure that the event is unregistered before an event is subscribed because the OnApplyTemplate() can be called multiple times.

        private Grid _grid;
        private Grid Grid
        {
            get { return _grid; }
            set
            {
                if (_grid != null)
                {
                    _grid.MouseMove -= PART_DisplayGrid_MouseMove;
                    _grid.DragOver -= PART_DisplayGrid_DragOver;
                    _grid.Drop -= PART_DisplayGrid_Drop;
                }

                _grid = value;

                if(_grid != null)
                {
                    _grid.AllowDrop = true;
                    _grid.MouseMove += PART_DisplayGrid_MouseMove;
                    _grid.DragOver += PART_DisplayGrid_DragOver;
                    _grid.Drop += PART_DisplayGrid_Drop;
                }   
            }
        }

        private void SetGridMatrix()
        {
            _matrixBorderNames = new List<string>();

            for (int i = 0; i < MatrixSize; i++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(1, GridUnitType.Star);
                Grid.RowDefinitions.Add(gridRow);
                Grid.ColumnDefinitions.Add(gridCol);
            }

            for (int rowNumber = 0; rowNumber < MatrixSize; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < MatrixSize; columnNumber++)
                {
                    Border border = new Border();
                    border.Background = Brushes.Transparent;
                    border.BorderBrush = Brushes.LightGray;
                    border.BorderThickness = new Thickness(1);
                    border.Margin = new Thickness(0);
                    border.Name = $"MatrixElementBorder_{rowNumber}_{columnNumber}";
                    _matrixBorderNames.Add(border.Name);
                    Grid.SetRow(border, rowNumber);
                    Grid.SetColumn(border, columnNumber);
                    Grid.Children.Add(border);
                }
            }
        }

        private void PART_DisplayGrid_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //// Package the data.
                //DataObject data = new DataObject();
                //data.SetData(DataFormats.StringFormat, circleUI.Fill.ToString());
                //data.SetData("Double", circleUI.Height);
                //data.SetData("Object", this);

                //// Initiate the drag-and-drop operation.
                //DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void PART_DisplayGrid_DragOver(object sender, DragEventArgs e)
        {
            foreach(UIElement uIElement in Grid.Children)
            {
                Border border = uIElement as Border;
                if (border == null)
                    continue;
                if (!_matrixBorderNames.Contains(border.Name))
                    continue;

                Point mousePosition = e.GetPosition(Grid);
                Point borderPosition = border.TransformToAncestor(Grid).Transform(new Point(0, 0));
                if (IsMouseOverElement(mousePosition, borderPosition, border.ActualWidth, border.ActualHeight))
                {
                    border.Background = Brushes.LightGray;
                }
                else
                {
                    border.Background = Brushes.Transparent;
                }
            }
        }

        private void PART_DisplayGrid_Drop(object sender, DragEventArgs e)
        {
            base.OnDrop(e);

            Point mousePosition = e.GetPosition(Grid);

            int? gridRow = GetGridRow(mousePosition.Y, MatrixSize, Grid.ActualHeight);
            if (gridRow == null)
                return;

            int? gridColumn = GetGridColumn(mousePosition.X, MatrixSize, Grid.ActualWidth);
            if (gridColumn == null)
                return;

            // If the DataObject contains string data, extract it.
            if (e.Data.GetDataPresent(typeof(ItemsPresenterModel)))
            {
                ItemsPresenterModel dataString = e.Data.GetData(typeof(ItemsPresenterModel)) as ItemsPresenterModel;

                // map data into required control
                StackPanel stackPanel = new StackPanel();
                TextBlock idTextBlock = new TextBlock();
                idTextBlock.Text = dataString.SampleId.ToString();                
                TextBlock textTextBlock = new TextBlock();
                textTextBlock.Text = dataString.SampleText.ToString();                
                TextBlock valueTextBlock = new TextBlock();
                valueTextBlock.Text = dataString.SampleValue.ToString();
                stackPanel.Children.Add(idTextBlock);
                stackPanel.Children.Add(textTextBlock);
                stackPanel.Children.Add(valueTextBlock);

                if (stackPanel.Children.Count > 0)
                {
                    Grid.SetRow(stackPanel, (int)gridRow);
                    Grid.SetColumn(stackPanel, (int)gridColumn);
                    _grid.Children.Add(stackPanel);

                    e.Effects = DragDropEffects.Copy;
                }
            }
            e.Handled = true;
        }

        #endregion

        #region PART_ItemsPresenter

        private ItemsPresenter _itemsPresenter;
        private ItemsPresenter ItemsPresenter
        {
            get { return _itemsPresenter; }
            set
            {
                if (_itemsPresenter != null)
                    _itemsPresenter.MouseMove -= PART_ItemsPresenter_MouseMove;

                _itemsPresenter = value;

                if(_itemsPresenter != null)
                {
                    _itemsPresenter.MouseMove += PART_ItemsPresenter_MouseMove;
                }
            }
        }

        private void PART_ItemsPresenter_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //getting all items
                //IList<ItemsPresenterModel> items = this.ItemsSource as IList<ItemsPresenterModel>;

                //Package the data
                IItemsPresenterModel selectedItem = this.SelectedItem as IItemsPresenterModel;

                // Initiate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, selectedItem, DragDropEffects.Copy);
            }
        }

        #endregion

        #region Private methods

        private bool IsMouseOverElement(Point elementParentRelativeMousePosition, Point elementPosition, double elementActualWidth, double elementActualHeight)
        {
            if (!IsValidDoubleValue(elementParentRelativeMousePosition.X) || Double.IsNaN(elementParentRelativeMousePosition.Y))
                return false;

            if (!IsValidDoubleValue(elementPosition.X) || Double.IsNaN(elementPosition.Y))
                return false;

            if (!IsValidDoubleValue(elementActualWidth))
                return false;

            if (!IsValidDoubleValue(elementActualHeight))
                return false;

            int mousePositionIntX = Convert.ToInt32(elementParentRelativeMousePosition.X);
            int mousePositionIntY = Convert.ToInt32(elementParentRelativeMousePosition.Y);
            System.Drawing.Point mouseIntPosition = new System.Drawing.Point(mousePositionIntX, mousePositionIntY);

            int borderIntX = Convert.ToInt32(elementPosition.X);
            int borderIntY = Convert.ToInt32(elementPosition.Y);
            int borderIntActualWidth = Convert.ToInt32(elementActualWidth);
            int borderIntActualHeight = Convert.ToInt32(elementActualHeight);
            System.Drawing.Rectangle borderRectangle = new System.Drawing.Rectangle(borderIntX, borderIntY, borderIntActualWidth, borderIntActualHeight);

            return borderRectangle.Contains(mouseIntPosition);
        }

        private int? GetGridRow(double gridRelativeMouseY, int matrixSize, double gridActualHeight)
        {
            if (!IsValidDoubleValue(gridRelativeMouseY))
                return null;

            if (!IsValidDoubleValue(gridActualHeight))
                return null;

            if (matrixSize < 1 || gridActualHeight <= 0)
                return null;

            int gridRow = Convert.ToInt32(Math.Floor(gridRelativeMouseY * matrixSize / gridActualHeight));

            return gridRow;
        }

        private int? GetGridColumn(double gridRelativeMouseX, int matrixSize, double gridActualWidth)
        {
            if (!IsValidDoubleValue(gridRelativeMouseX))
                return null;

            if (!IsValidDoubleValue(gridActualWidth))
                return null;

            if (matrixSize < 1 || gridActualWidth <= 0)
                return null;

            int gridColumn = Convert.ToInt32(Math.Floor(gridRelativeMouseX * matrixSize / gridActualWidth));

            return gridColumn;
        }

        // checks if double value is not NaN, +ve infinity, or -ve infinity
        private bool IsValidDoubleValue(double value)
        {
            return !Double.IsNaN(value) && !Double.IsInfinity(value) && !Double.IsNegativeInfinity(value);
        }

        #endregion
    }
}
