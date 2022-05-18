using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using CIIADHEL_CR.Droid;
using CIIADHEL_CR.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ImeiService))]
namespace CIIADHEL_CR.Droid
{
    public class ImeiService : IImeiService
    {
        [Obsolete]
        public string GetImei()
        {
            TelephonyManager manager = (TelephonyManager)Forms.Context.GetSystemService(Android.Content.Context.TelephonyService);

            return manager.Imei;
        }
    }
}