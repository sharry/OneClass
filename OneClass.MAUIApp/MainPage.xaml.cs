using OneClass.Domain.DbModels;
using OneClass.Domain.GraphModels;
using OneClass.MAUIApp.Services;
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
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		if (_authService is null)
		{
			_authService = new AuthService();
			Token = await _authService.GetAuthenticationToken();
		}
	}
	private void OnCounterClicked(object sender, EventArgs e)
	{
		_count++;

		if (_count == 1)
			CounterBtn.Text = $"Clicked {_count} time";
		else
			CounterBtn.Text = $"Clicked {_count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
	private async void GetUserInfoBtn_Clicked(object sender, EventArgs e)
	{
		using var httpClient = new HttpClient();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token); ;
		var response = await httpClient.GetAsync("https://localhost:5001/my-data");
		if (response.IsSuccessStatusCode)
		{
			HelloLabel.Text = "Loading...";
			var me = await response.Content.ReadFromJsonAsync<UserData>();
			HelloLabel.Text = $"Hello, {me.DisplayName}!";
		}
	}
}
