using System;
using System.Collections.Generic;
using HSNP.ViewModels;
using HSNP.Views;
using Xamarin.Forms;

namespace HSNP
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new LogoutPage());
        }
    }
}
