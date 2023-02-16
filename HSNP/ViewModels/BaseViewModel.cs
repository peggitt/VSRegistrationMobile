using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
//using Acr.UserDialogs;
using HSNP.Interface;
using HSNP.Models;
using HSNP.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using CommunityToolkit.Mvvm.Input;

namespace HSNP.ViewModels
{

    public partial class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel(INavigation navigation = null)
        {
            _permissionService = new PermissionService();

            // we handle the connectivity services
            _httpClient = new HttpClient();


            // Handle connectivity
            Connectivity.ConnectivityChanged += ConnectivityOnConnectivityChanged;
            IsNotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;

            //handle logged  in status
            IsNotLoggedIn = false; // Settings.Current.IsNotLoggedIn;
            Navigation = navigation;
        }

        ~BaseViewModel()
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
            IsNotConnected = e.NetworkAccess != NetworkAccess.Internet;
        }
        protected IToast Toast { get; } = DependencyService.Get<IToast>();

        public string SearchBarText
        {
            get => _searchBarText;
            set
            {
                _searchBarText = value;
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }
        public bool IsNotConnected
        {
            get => _isNotConnected;
            set
            {

                _isNotConnected = value;
                IsConnected = !IsNotConnected;
                OnPropertyChanged();
            }
        }

        public bool IsNotLoggedIn
        {
            get => _isNotLoggedIn;
            set
            {
                _isNotLoggedIn = value;
                OnPropertyChanged();
            }
        }

        private bool _isConnected { get; set; }
        private bool _isNotConnected { get; set; }
        private bool _isNotLoggedIn { get; set; }
        public string _searchBarText;
        private bool _isRefreshing = false;

        public PermissionService _permissionService;
        public readonly HttpClient _httpClient;
        // Navigation property inherited in view models
        public INavigation Navigation { get; set; }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        bool isOwner = false;
        public bool IsOwner
        {
            get { return isOwner; }
            set { SetProperty(ref isOwner, value); }
        }
        bool isIdle = false;
        public bool IsIdle
        {
            get { return isIdle; }
            set { SetProperty(ref isIdle, value); }
        }
        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        /// <summary>
        /// If <code>true</code>, signals the activity is not busy.
        /// </summary>
        /// <remarks>
        /// This is a useful property to bind a button enabled property to.
        /// </remarks>
        public bool IsNotBusy => !isBusy;


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }


}
