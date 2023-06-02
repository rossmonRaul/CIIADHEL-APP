using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CIIADHEL_CR.models
{
    public class Country : INotifyPropertyChanged
    {
        #region Fields

        private string _flag;
        private string _countryName;
        private string _countryCode;
        private string _countryMask;
        private string _countrySampleNumber;

        #endregion Fields

        #region Properties
        public Country(string nombre, string codigo, string mascara, string bandera, string sampleNumber)
        {
            this._countryName = nombre;
            this._countryCode = codigo;
            this._countryMask = mascara;
            this._flag = bandera;
            this._countrySampleNumber = sampleNumber;   
        }
        public string CountryFlag
        {
            get => _flag;
            set => SetProperty(ref _flag, value);
        }

        public string CountryName
        {
            get => _countryName;
            set => SetProperty(ref _countryName, value);
        }

        public string CountryCode
        {
            get => _countryCode;
            set => SetProperty(ref _countryCode, value);
        }

        public string CountryMask
        {
            get => _countryMask;
            set => SetProperty(ref _countryMask, value);
        }

        public string CountrySampleNumber
        {
            get => _countrySampleNumber;
            set => SetProperty(ref _countrySampleNumber, value);
        }

        #endregion Properties

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
