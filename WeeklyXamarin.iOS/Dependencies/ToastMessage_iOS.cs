
using GlobalToast;
using GlobalToast.Animation;
using UIKit;
using WeeklyXamarin.Framework.Interfaces;
using WeeklyXamarin.iOS.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastMessage_iOS))]
namespace WeeklyXamarin.iOS.Dependencies
{
    public class ToastMessage_iOS : IToastMessage
    {
        public void ShortAlert(string message)
        {
            Toast.MakeToast(message)
                .SetAppearance(new ToastAppearance
                {
                    Color = UIColor.Black,
                    CornerRadius = 8,
                    MessageTextAlignment = UITextAlignment.Center
                })
                .SetLayout(new ToastLayout
                {
                    PaddingLeading = 12,
                    PaddingTrailing = 12,
                    Spacing = 6
                })
                .SetAnimator(new ScaleAnimator())
                .SetDuration(ToastDuration.Regular) // Default is Regular
                .Show();
        }
    }
}
