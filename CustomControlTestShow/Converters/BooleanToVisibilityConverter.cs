using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CustomControlTestShow.Converters
{
    [Localizability(LocalizationCategory.NeverLocalize)]
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public static BooleanToVisibilityConverter FalseOrNullToCollapsed { get; } = new BooleanToVisibilityConverter(Visibility.Collapsed, false, false);
        public static BooleanToVisibilityConverter TrueOrNullToCollapsed { get; } = new BooleanToVisibilityConverter(Visibility.Collapsed, true, false);

        public static BooleanToVisibilityConverter TrueOrNullToVisible { get; } = new BooleanToVisibilityConverter(Visibility.Collapsed, false, true);
        public static BooleanToVisibilityConverter FalseOrNullToVisible { get; } = new BooleanToVisibilityConverter(Visibility.Collapsed, true, true);

        public static BooleanToVisibilityConverter FalseOrNullToHidden { get; } = new BooleanToVisibilityConverter(Visibility.Hidden, false, false);


        private Visibility _nonVisibleState;
        public Visibility NonVisibleState
        {
            get { return _nonVisibleState; }
            set
            {
                if (IsStatic)
                    throw new InvalidOperationException("Der Properties dürfen beim static Instanz nur einmal festgelegt werden");

                _nonVisibleState = value;
            }
        }

        private bool _falseToVisibile;
        public bool FalseToVisibile
        {
            get { return _falseToVisibile; }
            set
            {
                if (IsStatic)
                    throw new InvalidOperationException("Der Properties dürfen beim static Instanz nur einmal festgelegt werden");

                _falseToVisibile = value;
            }
        }

        private bool _nullToVisibile;
        public bool NullToVisibile
        {
            get { return _nullToVisibile; }
            set
            {
                if (IsStatic)
                    throw new InvalidOperationException("Der Properties dürfen beim static Instanz nur einmal festgelegt werden");

                _nullToVisibile = value;
            }
        }

        private bool IsStatic { get; set; }

        private BooleanToVisibilityConverter(Visibility nonVisibleState, bool falseToVisibile, bool nullToVisible)
        {
            _nonVisibleState = nonVisibleState;
            _falseToVisibile = falseToVisibile;
            _nullToVisibile = nullToVisible;
            IsStatic = true;
        }
        public BooleanToVisibilityConverter()
        { }

        #region IValueConverter Members

        public object Convert(object inputBool, Type targetType, object parameter, CultureInfo culture)
        {
            //wenn der inputBool boolean ist
            if (inputBool is bool)
                return SetBoolToVisibility((bool)inputBool);

            //wenn der inputBool ein nullable boolean ist
            if (inputBool is bool?)
            {
                if ((bool?)inputBool == null)
                {
                    if (NullToVisibile)
                        return Visibility.Visible;

                    return NonVisibleState;
                }

                return SetBoolToVisibility((bool)inputBool);
            }

            //wenn der inputBool weder boolean noch nullable boolean ist --> NullToVisibile wird dann benutzt um die Visibility zu entscheiden
            if (NullToVisibile)
                return Visibility.Visible;

            return NonVisibleState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                return (Visibility)value == Visibility.Visible;
            }
            return false;
        }

        private Visibility SetBoolToVisibility(bool inputBool)
        {
            if (inputBool)
            {
                if (FalseToVisibile)
                    return NonVisibleState;
                else
                    return Visibility.Visible;
            }
            if (FalseToVisibile)
                return Visibility.Visible;

            return NonVisibleState;
        }


        #endregion

    }
}
