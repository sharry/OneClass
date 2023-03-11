using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace OneClass.MAUIApp.Platforms.Android;
[Activity(Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
		Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
		DataHost = "auth",
		DataScheme = "msal896a45d1-095f-416d-baf3-ff19493fc2c1")]
public class MsalActivity : BrowserTabActivity
{
}
