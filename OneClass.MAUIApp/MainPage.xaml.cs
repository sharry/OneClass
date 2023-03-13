using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;
using OneClass.MAUIApp.Services;
using OneClass.MAUIApp.View;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace OneClass.MAUIApp;

public partial class MainPage : ContentPage
{
	private AuthService _authService;
	int _count = 0;
	string Token { get; set; }
	public MainPage()
	{
		InitializeComponent();
	}
	
	private async void LoginBtn_Clicked(object sender, EventArgs e)
	{
        if (_authService is null)
        {
            _authService = new AuthService();
            Token = await _authService.GetAuthenticationToken();
            if(Token != null)
            {
                await Navigation.PushAsync(new ClassesPage());

            }
        }
       
    }

	private async void loadUserData()
	{
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token); ;
        var response = await httpClient.GetAsync("https://onelass-backend.azurewebsites.net/my-data");
        if (response.IsSuccessStatusCode)
        {
            HelloLabel.Text = "Loading...";
            var me = await response.Content.ReadFromJsonAsync<UserData>();
            HelloLabel.Text = $"Hello, {me.DisplayName}!";
        }
    }
	
	private async void LoginBtn_Clicked(object sender, EventArgs e)
        if (_authService is null)
        {
            _authService = new AuthService();
            Token = await _authService.GetAuthenticationToken();
            if(Token != null)
            {
                loadUserData();
                // hide the login  button

            }
        }
       
    }

	private async void loadUserData()
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token); ;
        var response = await httpClient.GetAsync("https://onelass-backend.azurewebsites.net/my-data");
        if (response.IsSuccessStatusCode)
        {
            HelloLabel.Text = "Loading...";
            var me = await response.Content.ReadFromJsonAsync<UserData>();
            HelloLabel.Text = $"Hello, {me.DisplayName}!";
        }
    }
}
