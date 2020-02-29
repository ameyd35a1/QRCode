using RES_QRCode.Helper;
using RES_QRCode.Models;
using RES_QRCode.Pages;
using RES_QRCode.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace RES_QRCode.ViewModels
{
    public class FeatureViewModel : INotifyPropertyChanged
    {
        ApiServices api = new ApiServices();

        public string EmployeeID { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public string GreetingMessage
        {
            get
            {
                return "Hello, " + Settings.Username;
            }
        }

        public FeatureViewModel()
        {
            IsBusy = false;
        }

        public ICommand FindCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    var accessToken = Settings.AccessToken;
                    EmployeeDetails EmpDetails = await api.GetDetailsAsync(accessToken, EmployeeID);
                    IsBusy = false;
                    if (EmpDetails != null)
                    {

                        await Application.Current.MainPage.Navigation.PushAsync(new DetailsPage(EmpDetails));
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Find User", "User not found.", "OK");
                    }
                });
            }
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
