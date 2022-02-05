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

        #region example of OnApplyTemplate --> gets control from xaml

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // How to get item from xaml
            Grid PART_DisplayGrid = GetTemplateChild("PART_DisplayGrid") as Grid;

            // Create columns
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();

            // Create Rows
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(1, GridUnitType.Star);

            PART_DisplayGrid.ColumnDefinitions.Add(gridCol1);
            PART_DisplayGrid.ColumnDefinitions.Add(gridCol2);
            PART_DisplayGrid.ColumnDefinitions.Add(gridCol3);
                       
            PART_DisplayGrid.RowDefinitions.Add(gridRow1);
            PART_DisplayGrid.RowDefinitions.Add(gridRow2);
            PART_DisplayGrid.RowDefinitions.Add(gridRow3);

        }

        #endregion
    }
}
