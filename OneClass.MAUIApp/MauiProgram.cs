using Microsoft.Identity.Client;
using Microsoft.Maui.LifecycleEvents;

namespace OneClass.MAUIApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureLifecycleEvents(events =>
			{
#if ANDROID
				events.AddAndroid(platform =>
				{
					platform.OnActivityResult((activity, rc, result, data) =>
					{
						AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(rc, result, data);
					});
				});
#endif
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
