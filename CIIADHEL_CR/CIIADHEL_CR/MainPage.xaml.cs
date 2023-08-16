using System;
using Xamarin.Forms;

namespace CIIADHEL_CR
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            flyout.listview.ItemSelected += OnSelectedItem;
        }

        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as models.FlyoutItem;

            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                flyout.listview.SelectedItem = null;
                IsPresented = false;
            }
        }




        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    try
        //    {
        //        // Call endpoint for get all airports
        //        List<Airport_Principal> airports = await AirportServices.getAllAirports();

        //        // Call endpoint for get airports by name
        //        //List<Airport> airportName = await AirportServices.getAllAirportsByName("Batan");

        //        // Call endpoint for get an airport by id
        //        //Airport_Detail airportId = await AirportServices.getAnAirportById(2);
        //        //List<Airport_Detail> test = new List<Airport_Detail>();
        //        //test.Add(airportId);

        //        lstAirposts.ItemsSource = airports;

        //    }
        //    catch( Exception ex)
        //    {
        //        Console.WriteLine(ex); //This is temporal
        //        // Show message error in screen
        //    }

        //}

        //private async void lstAirposts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    await Navigation.PushAsync(new AirportPage(e.SelectedItem as Airport_Principal));
        //}

    }
}
