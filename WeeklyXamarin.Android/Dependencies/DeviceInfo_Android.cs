
using WeeklyXamarin.Droid.Dependencies;
using WeeklyXamarin.Framework.UI;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInfo_Android))]
namespace WeeklyXamarin.Droid.Dependencies
{
    public class DeviceInfo_Android : IDeviceInfo
    {
        public float StatusbarHeight => 24;
    }
}
