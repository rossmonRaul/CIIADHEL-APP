using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIIADHEL_CR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notams : PopupPage
    {
        private string rutaNotam;
        public Notams()
        {
            InitializeComponent();
        }
        public Notams(string rutaNotam) : this()
        {
            NetworkAccess currentNetwork = Connectivity.NetworkAccess;
            if (currentNetwork == NetworkAccess.Internet)//if you have internet
            {
                this.rutaNotam = rutaNotam;
                frameNotams.Source = rutaNotam;
                frameNo_internet.IsVisible = false;
                frameNotams.Navigated += (o, s) =>
                {
                    frameNotams.EvaluateJavaScriptAsync("document.getElementById('header-search').style.display = 'none';");
                };
                frameNotams.Navigated += (o, s) =>
                {
                    frameNotams.EvaluateJavaScriptAsync("document.querySelector('div.dropdown').style.display = 'none';");
                };
                frameNotams.Navigated += (o, s) =>
                {
                    frameNotams.EvaluateJavaScriptAsync("document.querySelector('div.pb-2').style.display = 'none';");
                };
                frameNotams.Navigated += (o, s) => {
                    frameNotams.EvaluateJavaScriptAsync("document.getElementById('donate').style.display = 'none';");
                };
                frameNotams.Navigated += (o, s) => {
                    frameNotams.EvaluateJavaScriptAsync("document.querySelector('aside.text-center').style.display = 'none';");
                };
                frameNotams.Navigated += (o, s) =>
                {
                    string hideContainerScript = "var containerElement = document.querySelector('.container.my-4.my-md-5'); if (containerElement) { containerElement.style.display = 'none'; }";
                    frameNotams.EvaluateJavaScriptAsync(hideContainerScript);
                };
                frameNotams.Navigated += (o, s) =>
                {
                    string hideAsideScript = "var asideElement = document.querySelector('aside.w-100.py-2.text-white.bg-primary.text-center.strong'); if (asideElement) { asideElement.style.display = 'none'; }";
                    frameNotams.EvaluateJavaScriptAsync(hideAsideScript);
                };
            }
            else
            {
                frameNotams.SetValue(IsVisibleProperty, false);
                frameNo_internet.SetValue(IsVisibleProperty, true);
            }

        }
        
        private void Button_Clicked(object sender, EventArgs e)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}
