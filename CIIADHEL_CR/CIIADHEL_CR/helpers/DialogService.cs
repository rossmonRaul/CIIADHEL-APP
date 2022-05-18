using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIIADHEL_CR.helpers
{
    /// <summary>
    /// Made by Olman Sanchez Zuniga
    /// </summary>
    public class DialogService
    {
        public async static Task ShowErrorAsync(string message, string title, string buttonText)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, buttonText);
        }
        public async static Task ShowErrorAsync(string message, string title, string buttonText, Action CallBackAferHide)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, buttonText);
            CallBackAferHide?.Invoke();
        }
    }
}
