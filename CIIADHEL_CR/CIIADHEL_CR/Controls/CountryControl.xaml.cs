using System;
using Xamarin.Forms;
using CIIADHEL_CR.models;

namespace CIIADHEL_CR.Controls
{
    public partial class CountryControl : StackLayout
    {
        public static readonly BindableProperty CountryProperty = BindableProperty.Create(
            nameof(Country),
            typeof(Country),
            typeof(CountryControl),
            default,
            BindingMode.TwoWay,//
          
          propertyChanged: (bindable, value, newValue) => (bindable as CountryControl)?.UpdateCountry(newValue as Country));

        public Country Country
        {
            get => (Country)GetValue(CountryProperty);
            set => SetValue(CountryProperty, value);
        }

        public CountryControl()
        {
            InitializeComponent();
        }

        private void UpdateCountry(Country model)
        {
            CountryCodeLabel.Text = $"+{model?.CountryCode}";
            CountryNameLabel.Text = model?.CountryName;
            FlagImage.Source = model?.CountryFlag;


        }
    }
}
