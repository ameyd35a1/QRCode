using RES_QRCode.Helper;
using RES_QRCode.Pages;
using RES_QRCode.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace RES_QRCode.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        ApiServices _api = new ApiServices();

        public string Username { get; set; }

        public string Password { get; set; }

        public string Message { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    if (Settings.AccessToken != string.Empty)
                    {
                        var accesstoken = await _api.LoginAsync(Username, Password);
                        Settings.AccessToken = accesstoken;
                        IsBusy = false;
                    }
                    else
                    {
                        var accesstoken = await _api.LoginAsync(Username, Password);
                        IsBusy = false;
                        if (accesstoken != string.Empty)
                        {
                            Settings.AccessToken = accesstoken;
                            Application.Current.MainPage = new NavigationPage(new FeaturePage())
                            {
                                BarBackgroundColor = Color.IndianRed,
                                BarTextColor = Color.WhiteSmoke,

                            };

                        }
                        else
                        {
                            //Application.Current.MainPage = new NavigationPage(new Login())
                            //{
                            //    BarBackgroundColor = Color.IndianRed,
                            //    BarTextColor = Color.WhiteSmoke,

                            //};
                            //DependencyService.Get<IMessage>().ShortMessage("Login unsuccessful");
                            await Application.Current.MainPage.DisplayAlert("Login", "Login unsuccessful.", "OK");
                        }
                    }
                });
            }
        }

        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;
            IsBusy = false;
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
