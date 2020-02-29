using RES_QRCode.Models;
using RES_QRCode.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RES_QRCode.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailsPage : ContentPage
	{
		public DetailsPage (EmployeeDetails EmpDetails)
		{
			InitializeComponent ();

            BindingContext = new DetailsViewModel(EmpDetails);
		}

        public DetailsPage()
        {
            InitializeComponent();
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}