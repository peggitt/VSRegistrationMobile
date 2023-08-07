using Android.Icu.Number;
using HSNP.Interfaces;
using HSNP.Models;
using MvvmHelpers;
namespace HSNP.Mobile.ViewModels;

public partial class RegistrationViewModel : BaseViewModel
{

    private readonly IApi _api;
    public RegistrationViewModel(bool complete=false) 
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
    public  ObservableRangeCollection<HouseholdMember> Sales
    {
        get => _sales;
        set { _sales = value; OnPropertyChanged(); }
    }

    public async void GetItems(bool complete)
    {
        try {

           
            var households = await App.db.Table<Household>().Where(i => i.IsComplete == complete).ToListAsync();
            Households = new ObservableRangeCollection<Household>();
            Households.AddRange(households);

            //var householdIds = households.Select(i => i.HouseholdId);

            //HouseholdMembers = new ObservableRangeCollection<HouseholdMember>();
            //HouseholdMembers.AddRange(await App.db.Table<HouseholdMember>().OrderByDescending(i => i.CreatedOn).Where(i => i.SerialNo == "1" && householdIds.Contains(i.HouseholdId)).ToListAsync());
            //HeightRequest = 100 + HouseholdMembers.Count() * 50;

        }
        catch (Exception ex) {
           await Application.Current.MainPage.DisplayAlert("Sorry!", ex.ToString(), "Ok");
        }


    }
}
