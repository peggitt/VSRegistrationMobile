using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSNP.Services;
using HSNP.ViewModels;
using HSNP.Views.Account;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HSNP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel(ApiService.Instance, Navigation);
        }
        void ForgotPinTapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ForgotPinPage());
        }
    }
}