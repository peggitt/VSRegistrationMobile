using System.Net.Http.Headers;
using HSNP.Models;
using Xamarin.KotlinX.Coroutines.Channels;

namespace HSNP.Mobile.Views;

public partial class LogoutPage : ContentPage
{
    public string AppVersion { get; set; }

    public LogoutPage()
	{
		InitializeComponent();
        AppVersion = $"App Version {AppInfo.Current.VersionString}";
        BindingContext = this;
    }
    async void LogoutClicked(object sender, EventArgs args)
    {

        // string url = "http://197.254.7.126:7300/getallcombocodes";

        // // JSON payload to send to the API
        // string jsonPayload = "{\"UserName\":\"admin@hsnp.or.ke\"}";
        // // Authentication token
        // string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJBY2Nlc3NUeXBlIjoiMSIsInVzZXJfaWQiOiJhZG1pbkBoc25wLm9yLmtlIiwiUm9sZV9JRCI6OTQwODA4NTI0LCJSYW5kS2V5IjoxLCJleHAiOjE2Nzg5OTM4NjB9.ntoaV2L8K20OpNmVLD05MDM_MJ-6rvg_8uvEzdJStjc";
        // // Create an HttpClient object with default settings
        // HttpClient httpClient = new
        //HttpClient(new Xamarin.Android.Net.AndroidMessageHandler());
        // // Set the authorization header with the authentication token
        // httpClient.DefaultRequestHeaders.Authorization =
        //     new AuthenticationHeaderValue("Bearer", token);
        // // Build the request message with the JSON payload and the GET method
        // HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
        // request.Content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
        // // Send the request to the API and wait for the response
        // HttpResponseMessage response = await httpClient.SendAsync(request);
        // // Read the response content as a string
        // string responseContent = await response.Content.ReadAsStringAsync();
        // // Display the response content in the console
        // Console.WriteLine(responseContent);

        var user = App.User;
        user.IsLoggedIn = false;
        await App.db.UpdateAsync(user);
        await Navigation.PopToRootAsync();
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}
