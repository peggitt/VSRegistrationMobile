using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using HSNP.Database;
using HSNP.Models;
using HSNP.Services;
using HSNP.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HSNP
{
    public partial class App : Application
    {
        public static User User;
        public static Visitor Visitor;
        public static List<Visit> Visits;
        public static SQLiteAsyncConnection db;
        public static DataStore Database { get; private set; }
        public App()
        {
            InitializeComponent();
            Database = new DataStore();
            db = Database._database;
            DependencyService.Register<MockDataStore>();
            User = new User();
            Visitor = new Visitor();
            Visits = new List<Visit>();
            MainPage = new LoadingPage();
        }

        protected override async void OnStart()
        {
            await Task.Delay(1000);
            var status = await CheckStatusAsync();
            if (status.Equals("home"))
                MainPage = new AppShell();
              // MainPage = new NavigationPage(new HomePage());
            else if (status.Equals("login"))
                MainPage = new NavigationPage(new LoginPage());
           /* else if (status.Equals("payment"))
                MainPage = new NavigationPage(new PaymentPage());
            else if (status.Equals("renew"))
                MainPage = new NavigationPage(new SubscriptionPage());
           */
        }
        public async Task<string> CheckStatusAsync()
        {
            var str = "";
            User = await db.Table<User>().FirstOrDefaultAsync();
            if (User == null || User.IsLoggedIn == false)
                str = "login";
            else
            {
               
                /* var storeSubscription = await App.Database.GetStoreSubscription();
                if (storeSubscription == null)
                    str = "renew";
                else if (storeSubscription.EndDate != null &&
                         ((DateTime)storeSubscription.EndDate) < DateTime.Now)
                    str = "renew";
                else if (storeSubscription.StatusId == 1)
                    str = "payment";

                else
                */
                    str = "home";
            }

            return str;

        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
