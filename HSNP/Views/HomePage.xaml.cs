using System;
using System.Collections.Generic;
using HSNP.Services;
using HSNP.ViewModels;
using Xamarin.Forms;

namespace HSNP.Views
{	
	public partial class HomePage : ContentPage
	{	
		public HomePage ()
		{
			InitializeComponent ();
			BindingContext = new SearchViewModel(Navigation, ApiService.Instance);
		}
	}
}

