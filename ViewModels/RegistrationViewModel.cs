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
        _ = GetItems(complete);
       
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

    public async Task GetItems(bool complete)
    {
        try {

           
            var households = await App.db.Table<Household>().Where(i => i.IsComplete == complete && i.MarkForDownload==false).ToListAsync();
            Households = new ObservableRangeCollection<Household>();
            Households.AddRange(households);

            var householdIds = households.Select(i => i.HouseholdId);

            HouseholdMembers = new ObservableRangeCollection<HouseholdMember>();
            HouseholdMembers.AddRange(await App.db.Table<HouseholdMember>().OrderByDescending(i => i.CreatedOn)
                .Where(i =>  householdIds.Contains(i.HouseholdId) && (i.RelationshipId == 1 || (i.RelationshipId!=1 && i.SerialNo=="1"))).ToListAsync() );
            HeightRequest = 100 + HouseholdMembers.Count() * 50;

        }
        catch (Exception ex) {
           await Application.Current.MainPage.DisplayAlert("Sorry!", ex.Message, "Ok");
        }


    }
}
