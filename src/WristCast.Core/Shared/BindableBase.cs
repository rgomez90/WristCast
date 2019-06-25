using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WristCast.Core.Shared
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        protected void SetProperty<T>(ref T value, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Equals(value, newValue)) return;
            value = newValue;
            OnPropertyChanged(propertyName);
        }

        protected void SetProperty<T>(ref T value, T newValue, params string[] propertyNames)
        {
            if (Equals(value, newValue)) return;
            value = newValue;
            foreach (var name in propertyNames)
            {
                OnPropertyChanged(name);
            }
        }

        protected void SetProperty<T>(ref T value, T newValue, Action action, params string[] propertyNames)
        {
            if (value.Equals(newValue)) return;
            value = newValue;
            foreach (var name in propertyNames)
            {
                OnPropertyChanged(name);
            }
            action?.Invoke();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}