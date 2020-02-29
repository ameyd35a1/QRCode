using RES_QRCode.Models;
using RES_QRCode.Pages;
using RES_QRCode.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RES_QRCode.ViewModels
{
    class AddUserViewModel
    {
        ApiServices _api = new ApiServices();

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Role
        {
            get
            {
                if (IsAdmin)
                {
                    return "admin";
                }
                else
                {
                    return "user";
                }
            }
        }

        private bool _isAdmin = false;
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))
                    {
                        if (Password.Equals(ConfirmPassword))
                        {
                            ResponseMessageModel message = await _api.AddUserAsync(Username, Password, Role);

                            if (message.ResponseStatus)
                            {
                                DependencyService.Get<IMessage>().LongMessage(message.ResponseMessage);
                                Application.Current.MainPage = new NavigationPage(new FeaturePage())
                                {
                                    BarBackgroundColor = Color.IndianRed,
                                    BarTextColor = Color.WhiteSmoke,
                                };
                            }
                            else
                            {
                                DependencyService.Get<IMessage>().LongMessage(message.ResponseMessage);
                            }
                        }
                        else
                        {
                            DependencyService.Get<IMessage>().LongMessage("Passwords did not match");
                        }
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongMessage("Please enter all the details");
                    }
                });
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
