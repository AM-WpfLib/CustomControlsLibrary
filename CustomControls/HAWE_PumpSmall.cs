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
    public class HAWE_PumpSmall : Control
    {
        static HAWE_PumpSmall()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HAWE_PumpSmall), new FrameworkPropertyMetadata(typeof(HAWE_PumpSmall)));

            CommandManager.RegisterClassCommandBinding(typeof(HAWE_PumpSmall), new CommandBinding(ChangeFlowDirectionCommand, OnChangeFlowDirection));
        }

        #region Label

        /// <summary>
        /// Label
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// LabelProperty
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(HAWE_PumpSmall), new FrameworkPropertyMetadata(""));

        #endregion

        #region SwitchFlowDirection

        /// <summary>
        /// SwitchFlowDirection
        /// </summary>
        public bool? SwitchFlowDirection
        {
            get { return (bool?)GetValue(SwitchFlowDirectionProperty); }
            set { SetValue(SwitchFlowDirectionProperty, value); }
        }

        /// <summary>
        /// SwitchFlowDirectionProperty
        /// </summary>
        public static readonly DependencyProperty SwitchFlowDirectionProperty =
            DependencyProperty.Register("SwitchFlowDirection", typeof(bool?), typeof(HAWE_PumpSmall), new FrameworkPropertyMetadata(null));

        #endregion

        #region ChangeFlowDirectionCommand

        public static readonly RoutedCommand ChangeFlowDirectionCommand = new RoutedCommand("ChangeFlowDirectionCommand", typeof(HAWE_PumpSmall));

        private static void OnChangeFlowDirection(object sender, ExecutedRoutedEventArgs e)
        {
            HAWE_PumpSmall myself = sender as HAWE_PumpSmall;
            myself.ChangeFlowDirection();
        }

        #endregion

        #region Logic

        public void ChangeFlowDirection()
        {
            if (SwitchFlowDirection == null)
                SwitchFlowDirection = true;
            else if (SwitchFlowDirection == true)
                SwitchFlowDirection = false;
            else
                SwitchFlowDirection = null;
        }

        #endregion
    }
}
