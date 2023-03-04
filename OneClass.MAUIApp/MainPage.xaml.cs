using Microsoft.Graph.Models;
using OneClass.MAUIApp.Services;

namespace OneClass.MAUIApp;

public partial class MainPage : ContentPage
{
	int _count = 0;
	private GraphService _graphService;
	private User _user;
	public MainPage()
	{
		InitializeComponent();
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
		if (_graphService == null)
		{
			_graphService = new GraphService();
		}
		_user = await _graphService.GetMyDetailsAsync();
		HelloLabel.Text = $"Hello, {_user.DisplayName}!";
	}
}
