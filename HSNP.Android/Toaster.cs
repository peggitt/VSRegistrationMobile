using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HSNP.Droid;
using HSNP.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(Toaster))]
namespace HSNP.Droid
{
    public class Toaster : IToast
    {
        public void SendToast(string message)
        {
            var context = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context;
            Device.BeginInvokeOnMainThread(() =>
            {
                Toast.MakeText(context, message, ToastLength.Long).Show();
            });
        }
    }
}