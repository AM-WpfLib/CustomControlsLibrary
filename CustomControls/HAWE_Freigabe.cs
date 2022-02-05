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
    public class HAWE_Freigabe : Control
    {
        static HAWE_Freigabe()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HAWE_Freigabe), new FrameworkPropertyMetadata(typeof(HAWE_Freigabe)));
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
            DependencyProperty.Register("Label", typeof(string), typeof(HAWE_Freigabe), new FrameworkPropertyMetadata(""));

        #endregion

        #region IsOpen

        /// <summary>
        /// IsOpen
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        /// <summary>
        /// IsOpenProperty
        /// </summary>
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(HAWE_Freigabe), new FrameworkPropertyMetadata(true, OnIsOpenPropertyChanged));

        /// <summary>
        /// IsOpenProperty changed
        /// </summary>
        /// <param name="source">HAWE_Freigabe</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        protected static void OnIsOpenPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            HAWE_Freigabe myself = source as HAWE_Freigabe;
            bool newIsOpenValue = (bool)e.NewValue;
            myself.OnIsOpenPropertyChanged(newIsOpenValue);
        }

        #endregion

        #region Angle

        /// <summary>
        /// Key for Angle
        /// </summary>
        public static DependencyPropertyKey AnglePropertyKey =
            DependencyProperty.RegisterReadOnly("Angle", typeof(double), typeof(HAWE_Freigabe), new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// AngleProperty - Gets angle of control.
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            AnglePropertyKey.DependencyProperty;

        /// <summary>
        /// Angle
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            private set { SetValue(AnglePropertyKey, value); }
        }

        #endregion

        #region Logic

        private void OnIsOpenPropertyChanged(bool newIsOpenValue)
        {
            if (newIsOpenValue)
                Angle = 0;
            else
                Angle = 90;
        }


        #endregion

    }
}
