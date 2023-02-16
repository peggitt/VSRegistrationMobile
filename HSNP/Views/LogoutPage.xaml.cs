using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HSNP.Models;
using HSNP.Services;
using HSNP.ViewModels;
using Xamarin.Forms;

namespace HSNP.Views
{
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();
         
        }

        async void Logout_Clicked(System.Object sender, System.EventArgs e)
        {
            await App.Database._database.DeleteAllAsync<User>();
            //await App.Database._database.DeleteAllAsync<Visitor>();
            await Shell.Current.GoToAsync("//LoginPage");
        }
        async void Cancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        
    }
}

