using UIKit;
using WeeklyXamarin.Framework.UI;
using WeeklyXamarin.iOS.Dependencies;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInfo_iOS))]
namespace WeeklyXamarin.iOS.Dependencies
{
    public class DeviceInfo_iOS : IDeviceInfo
    {
        public float StatusbarHeight => (float)UIApplication.SharedApplication.StatusBarFrame.Size.Height;
    }
}
