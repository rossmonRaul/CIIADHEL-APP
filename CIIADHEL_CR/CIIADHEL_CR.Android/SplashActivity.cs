using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Telephony;
using CIIADHEL_CR.models;
using Plugin.FirebasePushNotification;
using System;

namespace CIIADHEL_CR.Droid
{

    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, Label = "CIIADHEL CR")]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            System.Threading.Thread.Sleep(200);
            StartActivity(typeof(MainActivity));
            // Create your application here

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }
    }
}

