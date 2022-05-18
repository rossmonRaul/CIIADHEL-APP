using CIIADHEL_CR.pages;
using Xamarin.Forms;
namespace CIIADHEL_CR.models
{
    public class LoginModalPage : CarouselPage
    {
        ContentPage login;
        public LoginModalPage(ILoginManager ilm)
        {
            login = new LoginPage(ilm);

            this.Children.Add(login);
            MessagingCenter.Subscribe<ContentPage>(this, "Login", (sender) =>
            {
                this.SelectedItem = login;
            });
        }
    }
}
