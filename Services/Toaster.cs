
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using HSNP.Mobile.Droid;
using HSNP.Mobile.Interfaces;

[assembly: Microsoft.Maui.Controls.Dependency(typeof(Toaster))]
namespace HSNP.Mobile.Droid
{
    public class Toaster : IToaster
    {
        public async Task SendToast(string message)
        {
            var toast = Toast.Make(message);
            await toast.Show();
        }
        public async Task SendToastAsync(string message)
        {
            var toast = Toast.Make(message);
            await toast.Show();
        }
        public async Task SendSnackbarAsync(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {

                CornerRadius = new CornerRadius(10),
                Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),

            };
            string actionButtonText = "Ok";
            TimeSpan duration = TimeSpan.FromSeconds(10);

            var snackbar = Snackbar.Make(message, null, actionButtonText, duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);

        }
        public async Task SendSnackbar(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                //  BackgroundColor = Colors.Red,
                //  TextColor = Colors.Green,
                //   ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(10),
                Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
                //  CharacterSpacing = 0.5
            };
            string actionButtonText = "Ok";
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = Snackbar.Make(message, null, actionButtonText, duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);

        }
    }
}