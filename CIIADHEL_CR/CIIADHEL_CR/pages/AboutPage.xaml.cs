
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIIADHEL_CR.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            Application.Current.Properties["ultimaPantalla"] = "about";
        }
    }
}