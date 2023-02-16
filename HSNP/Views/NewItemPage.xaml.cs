using System;
using System.Collections.Generic;
using System.ComponentModel;
using HSNP.Models;
using HSNP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HSNP.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
          //  BindingContext = new NewItemViewModel();
        }
    }
}