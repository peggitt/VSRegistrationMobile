using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HSNP.Views
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            await lblTitle.ScaleTo(2, 1000);
            await Task.Delay(100);
            await lblTitle.ScaleTo(1.6, 1000);
            // lblStallMaster.RotateTo(360, 1000);
        }
    }
}

