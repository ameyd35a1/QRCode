using RES_QRCode.Helper;
using RES_QRCode.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace RES_QRCode.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeaturePage : ContentPage
    {
        public FeaturePage()
        {
            InitializeComponent();

            //TODO: Populate the Settings.Role from the Login Response from the service and then remove the below block
            //Remove for other users than admin
            if (Settings.Role.ToLower() != "admin")
            {
                ToolbarItem AddUserToolbarItem = ToolbarItems.First(i => i.Name.Equals("AddUserToolbarItem"));
                ToolbarItems.Remove(AddUserToolbarItem);
            }
        }

        private async void BtnScan_Clicked(object sender, EventArgs e)
        {
            ZXingScannerPage scanPage = new ZXingScannerPage
            {
                DefaultOverlayBottomText = "Scan QR code"
            };
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();

                    FeatureViewModel featureVM = new FeatureViewModel();
                    featureVM.EmployeeID = result.Text;
                    featureVM.FindCommand.Execute(null);
                });
            };
            await Navigation.PushModalAsync(scanPage);
        }

        private async void Settings_Activated(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SettingsPage());
        }

        private async void AddUser_Activated(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddUserPage());
        }

        private void Logout_Activated(object sender, EventArgs e)
        {
            Settings.AccessToken = string.Empty;
            Settings.Username = string.Empty;
            Application.Current.MainPage = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.IndianRed,
                BarTextColor = Color.WhiteSmoke,
            };
        }
    }
}