using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RES_QRCode.Droid;
using Xamarin.Forms;

[assembly:Dependency(typeof(MessageDroid))]
namespace RES_QRCode.Droid
{
    public class MessageDroid : IMessage
    {
        public void LongMessage(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortMessage(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}