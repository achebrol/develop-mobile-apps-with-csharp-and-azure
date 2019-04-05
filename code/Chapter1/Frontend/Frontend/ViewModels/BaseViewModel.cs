using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Frontend.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string propTitle = string.Empty;
        private bool propIsBusy = false;

        public string Title
        {
            get { return propTitle; }
            set { SetProperty(ref propTitle, value, "Title"); }
        }

        public bool IsBusy
        {
            get { return propIsBusy; }
            set { SetProperty(ref propIsBusy, value, "IsBusy"); }
        }

        protected void SetProperty<T>(ref T store, T value, string propName, Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(store, value)) return;
            store = value;
            if (onChanged != null)
                onChanged();
            OnPropertyChanged(propName);
        }

        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
