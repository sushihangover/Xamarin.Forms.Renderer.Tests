using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AVPlayerViaDepService.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(NativePlayer))]
namespace AVPlayerViaDepService.Droid
{
	[Activity(Label = "AVPlayerViaDepService.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}
	}
	public class NativePlayer : INativeMediaPlayer
	{
		public NativePlayer() { }

		public void PlayMediaFile(string mediaFile)
		{
		}

		//public void DonePlaying(NSNotiication notification)
		//{
		//}
	}
}

