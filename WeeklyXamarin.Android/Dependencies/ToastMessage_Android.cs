using Android.Widget;
using WeeklyXamarin.Droid.Dependencies;
using WeeklyXamarin.Framework.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastMessage_Android))]
namespace WeeklyXamarin.Droid.Dependencies
{
    public class ToastMessage_Android : IToastMessage
    {
        public void ShortAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}
