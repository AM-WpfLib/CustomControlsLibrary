using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace CustomControlTestShow.Common
{
    public abstract class PropertyChangedNotifier : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(params string[] names)
        {
            OnPropertyChanged((IEnumerable<string>)names);
        }

        protected virtual void OnPropertyChanged(IEnumerable<string> names)
        {
            PropertyChangedEventHandler eh = PropertyChanged;

            if (eh != null)
            {
                foreach (string name in names)
                {
                    eh(this, new PropertyChangedEventArgs(name));
                }
            }
        }

        #endregion
    }
}

