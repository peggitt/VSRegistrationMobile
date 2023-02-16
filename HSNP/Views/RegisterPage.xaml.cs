using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSNP.Services;
using HSNP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HSNP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(string option="new")
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(Navigation, ApiService.Instance,option);
        }
    }
}