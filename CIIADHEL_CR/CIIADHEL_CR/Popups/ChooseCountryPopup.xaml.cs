using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using CIIADHEL_CR.Controls;
using CIIADHEL_CR.models;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace CIIADHEL_CR.Popups
{
    public partial class ChooseCountryPopup : PopupPage
    {
        #region Fields

        private static List<Country> countries;
        private Country selectedCountry;

        #endregion Fields

        #region Constructors

        public ChooseCountryPopup(Country selectedCountry)
        {
            InitializeComponent();
            if (countries == null || !countries.Any())
            {
                countries= new List<Country>();
                //countries.Add(new Country("Costa Rica","506", "XXXX-XXXX","cr.png","8312-3456"));
                //countries.Add(new Country("United States","1", "(201) XXX-XXXX","us.png", "(201) 555-0123"));
                LoadCountries();
            }
            //VisibleCountries = countries;
            VisibleCountries = new ObservableCollection<Country>(countries);

            SelectedCountry = selectedCountry;
            CommonCountriesList.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(VisibleCountries), source: this));
            CurrentCountryControl.SetBinding(CountryControl.CountryProperty, new Binding(nameof(SelectedCountry), source: this));
        }

        #endregion Constructors

        #region Properties

        public ICommand CountrySelectedCommand { get; set; }

         public ObservableCollection<Country> VisibleCountries { get; }
        //public List<Country> VisibleCountries { get; }

        public Country SelectedCountry
        {
            get => selectedCountry;
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        #endregion Properties

        #region Private Methods

        private void CloseBtn_Clicked(object sender, EventArgs e)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private void ConfirmBtn_Clicked(object sender, EventArgs e)
        {
            CountrySelectedCommand?.Execute(SelectedCountry);
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        public void LoadCountries()
        {
            string jsonFileName = "CIIADHEL_CR.resources.countries.countries.json";
            Assembly assembly = typeof(ChooseCountryPopup).GetTypeInfo().Assembly;

            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(jsonFileName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string jsonString = reader.ReadToEnd(); 
                        countries = JsonConvert.DeserializeObject<List<Country>>(jsonString); 
                    }
                }
            }
            catch (Exception ex)
            {             
                Console.WriteLine($"Error al cargar el archivo JSON: {ex.Message}");
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;

            VisibleCountries.Clear();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                countries.ForEach(country => VisibleCountries.Add(country));
            }
            else
            {
                var filteredCountries = countries.Where(country => country.CountryName.ToLower().Contains(searchText.ToLower())).ToList();
                filteredCountries.ForEach(country => VisibleCountries.Add(country));
            }
        }

        private void CommonCountriesList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedCountry = e.SelectedItem as Country;
        }

        #endregion Private Methods
    }
}
