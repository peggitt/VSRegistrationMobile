using HSNP.Interfaces;
using HSNP.Models;

namespace HSNP.Mobile.ViewModels;


public partial class MembersViewModel : BaseViewModel
{

    private readonly IApi _api;
    [ObservableProperty]
    private int heightRequest;
    [ObservableProperty]
    private string householdId;

    [ObservableProperty]
    private string membersCount;

    [ObservableProperty]
    private bool isComplete;

    [ObservableProperty]
    private bool hideCompleteButton;

    public MembersViewModel() 
    {
        HouseholdId = App.HouseholdId;
        GetItems();
    }

    [ObservableProperty]
    private List<HouseholdMember> householdMembers;

    public async void GetItems()
    {
        try{

            HouseholdMembers = await App.db.Table<HouseholdMember>().Where(i => i.HouseholdId == HouseholdId).ToListAsync();
            HeightRequest = 100 + HouseholdMembers.Count() * 50;

            var current = HouseholdMembers.Where(i => i.IsComplete).Count();
            var total = (await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == HouseholdId)).HouseholdMembers;
            IsComplete = current == total;
            MembersCount = $"{current} / {total}";

            HideCompleteButton = (bool)(await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == App.HouseholdId)).IsComplete;
        }catch(Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Sorry", ex.Message, "OK");
        }
    }
    [RelayCommand]
    async void Save()
    {
        if (!HouseholdMembers.Any(i => i.RelationshipId == 1))
        {
            await Application.Current.MainPage.DisplayAlert("Main Provider Missing", $"Please add a member with relationship set as Main Provider", "OK");
        }
        else
        {


            var current = HouseholdMembers.Where(i => i.IsComplete).Count();
            var total = (await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == HouseholdId)).HouseholdMembers;

            if (current == total)
            {
                var household = await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == App.HouseholdId);
                household.IsComplete = true;
                await App.db.UpdateAsync(household);
                HideCompleteButton = true;
                await Toast.SendToast("Household marked as ready for upload");
                Application.Current.MainPage = new AppShell();
                // await Shell.Current.GoToAsync($"/{nameof(RegistrationPage)}");
            }
            else
            {

                await Application.Current.MainPage.DisplayAlert("Please Add all Members", $"Added members: {current} / {total}", "OK");
            }

        }
    }
}
