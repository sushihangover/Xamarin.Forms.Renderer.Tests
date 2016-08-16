using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PushPageFromNative.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(MainActivity))]
namespace PushPageFromNative.Droid
{
	[Activity(Label = "PushPageFromNative.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, PushPageFromNative.IShowForm
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}

		async public void PushPage()
		{
			await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new PushPageFromNative.MyPage());
		}
	}
}
