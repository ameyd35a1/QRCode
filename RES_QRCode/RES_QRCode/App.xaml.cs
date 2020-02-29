using RES_QRCode.Helper;
using RES_QRCode.Pages;
using RES_QRCode.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RES_QRCode
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        private void SetMainPage()
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                //TODO - Check logic for Access Token
                if (Settings.AccessTokenExpirationDate < DateTime.Now)
                {
                    var loginViewModel = new LoginViewModel();
                    loginViewModel.LoginCommand.Execute(null);
                }
                MainPage = new NavigationPage(new FeaturePage())
                {
                    BarBackgroundColor = Color.IndianRed,
                    BarTextColor = Color.WhiteSmoke,                    
                };
            }
            else 
            {
                MainPage = new NavigationPage(new Login())
                {
                    BarBackgroundColor = Color.IndianRed,
                    BarTextColor = Color.WhiteSmoke,

                };
            }            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
