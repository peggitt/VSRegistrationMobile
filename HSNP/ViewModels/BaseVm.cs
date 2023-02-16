using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Refit;
using Rg.Plugins.Popup.Extensions;
using HSNP.Interface;
using HSNP.Models;
using HSNP.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using HSNP.Services;
// using Acr.UserDialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HSNP.ViewModels
{
    
    public partial class BaseVm : ObservableObject
    {
        [ObservableProperty]
        private string idNumber;
        [ObservableProperty]
        private string phoneNo;
        [ObservableProperty]
        private bool isGate;

        public BaseVm(INavigation navigation = null)
        {
            _permissionService = new PermissionService();

            // we handle the connectivity services
            _httpClient = new HttpClient();         
            // Handle connectivity
            Connectivity.ConnectivityChanged += ConnectivityOnConnectivityChanged;
          //  IsNotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;

            //handle logged  in status
         //   IsNotLoggedIn = false; // Settings.Current.IsNotLoggedIn;
            Navigation = navigation;
        }


        ~BaseVm()
        {
            Connectivity.ConnectivityChanged -= ConnectivityOnConnectivityChanged;
        }



        public async Task<bool> PreNetworkCheck()
        {
            try
            {
                var connected = _permissionService.CheckNetworkConnectivity();
                if (connected != NetworkAccess.Internet)
                {
                  //  UserDialogs.Instance.Alert("No internet connection available.", "Oops", "Ok");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

              //  await UserDialogs.Instance.AlertAsync("Something went wrong, please try again later.", "Error", "Ok");

                return true;
            }
        }
        private void ConnectivityOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
           // IsNotConnected = e.NetworkAccess != NetworkAccess.Internet;
        }
        protected IToast Toast { get; } = DependencyService.Get<IToast>();

       

      

       
        [ObservableProperty]
        public string searchBarText;
        [ObservableProperty]
        private bool isRefreshing = false;

        [ObservableProperty]
        private bool isNotConnected;

        [ObservableProperty]
        private bool isNotLoggedIn;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private bool isOwner;

        [ObservableProperty]
        private bool isIdle;

        [ObservableProperty]
        private string title;

        public PermissionService _permissionService;
        public readonly HttpClient _httpClient;
        // Navigation property inherited in view models
        public INavigation Navigation { get; set; }


        /// <summary>
        /// If <code>true</code>, signals the activity is not busy.
        /// </summary>
        /// <remarks>
        /// This is a useful property to bind a button enabled property to.
        /// </remarks>
        public bool IsNotBusy => !isBusy;

    }
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> lista, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                lista.Add(item);
            }
        }
    }
}