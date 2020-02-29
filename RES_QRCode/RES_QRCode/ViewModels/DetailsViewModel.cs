using RES_QRCode.Helper;
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
    class DetailsViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Location { get; set; }
        public string ID { get; set; }

        bool entryMarked;
        public bool EnrtyMarked
        {
            set
            {
                if (entryMarked != value)
                {
                    entryMarked = value;
                    OnPropertyChanged("EnrtyMarked");
                }
            }
            get
            {
                return entryMarked;
            }
        }

        ApiServices api = new ApiServices();

        public event PropertyChangedEventHandler PropertyChanged;

        public DetailsViewModel(EmployeeDetails empDetails)
        {
            this.ID = empDetails.ID;
            this.Name = empDetails.Name;
            this.Level = empDetails.Level;
            this.Location = empDetails.Location;
            this.EnrtyMarked = empDetails.EntryMarked;
        }

        public ICommand CheckInCommand
        {
            get
            {
                return new Command(async () =>
                {
                    ResponseMessageModel message = await api.CheckInAsync(Settings.AccessToken, this.ID, Settings.Username, false);

                    if (message.ResponseStatus)
                    {
                        //TODO: Display success message
                        DependencyService.Get<IMessage>().ShortMessage(message.ResponseMessage);
                        Application.Current.MainPage = new NavigationPage(new FeaturePage())
                        {
                            BarBackgroundColor = Color.IndianRed,
                            BarTextColor = Color.WhiteSmoke,

                        };
                    }
                    else
                    {
                        //TODO: Display error message
                        DependencyService.Get<IMessage>().ShortMessage(message.ResponseMessage);
                    }
                });
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                return new Command(async () =>
                {
                    ResponseMessageModel message = await api.CheckInAsync(Settings.AccessToken, this.ID, Settings.Username, true);

                    if (message.ResponseStatus)
                    {
                        //TODO: Display success message
                        DependencyService.Get<IMessage>().ShortMessage(message.ResponseMessage);
                        this.EnrtyMarked = false;
                        Application.Current.MainPage = new NavigationPage(new FeaturePage())
                        {
                            BarBackgroundColor = Color.IndianRed,
                            BarTextColor = Color.WhiteSmoke,

                        };
                    }
                    else
                    {
                        //TODO: Display error message
                        DependencyService.Get<IMessage>().ShortMessage(message.ResponseMessage);
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
