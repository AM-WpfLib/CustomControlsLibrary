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

            //// How to get item from xaml
            //Canvas outerCanvas = GetTemplateChild("Template child Name") as Canvas;
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

    }
}
