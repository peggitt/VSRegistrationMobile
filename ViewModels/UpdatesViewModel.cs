using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HSNP.Constants;
using HSNP.Interfaces;
using HSNP.Mobile;
using HSNP.Models;
using HSNP.Services;
using IntelliJ.Lang.Annotations;
using Java.Net;
using MvvmHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace HSNP.ViewModels
{
    public partial class UpdatesViewModel : Mobile.ViewModels.BaseViewModel
    {
        private readonly IApi _api;
        public UpdatesViewModel(bool complete = false)
        {
            GetItems(complete);

        }

        [ObservableProperty]
        private ObservableRangeCollection<HouseholdMember> householdMembers;

        [ObservableProperty]
        private ObservableRangeCollection<Household> households;

        [ObservableProperty]
        private int heightRequest;

        private ObservableRangeCollection<HouseholdMember> _sales;
        public ObservableRangeCollection<HouseholdMember> Sales
        {
            get => _sales;
            set { _sales = value; OnPropertyChanged(); }
        }

        public async void GetItems(bool complete)
        {
            try
            {


                var households = await App.db.Table<Household>().Where(i => i.IsComplete == complete && i.MarkForDownload == true).Take(10).ToListAsync();
                Households = new ObservableRangeCollection<Household>();
                Households.AddRange(households);

                var householdIds = households.Select(i => i.HouseholdId);

                HouseholdMembers = new ObservableRangeCollection<HouseholdMember>();
                HouseholdMembers.AddRange(await App.db.Table<HouseholdMember>().OrderByDescending(i => i.CreatedOn).Where(i => i.RelationshipId == 1 && householdIds.Contains(i.HouseholdId)).ToListAsync());
                HeightRequest = 100 + HouseholdMembers.Count() * 50;

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Sorry!", ex.ToString(), "Ok");
            }


        }
    }
}
