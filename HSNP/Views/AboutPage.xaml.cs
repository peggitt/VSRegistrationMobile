using System;
using System.ComponentModel;
using HSNP.Services;
using HSNP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HSNP.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel(Navigation, ApiService.Instance);
        }
    }
}