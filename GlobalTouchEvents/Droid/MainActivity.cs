using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GlobalTouch;
using Xamarin.Forms;

namespace GlobalTouch.Droid
{
	[Activity(Label = "GlobalTouch.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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

		public EventHandler globalTouchHandler;
		public override bool DispatchTouchEvent(MotionEvent ev)
		{
			globalTouchHandler?.Invoke(null, new TouchEventArgs<Point>(new Point(ev.GetX(), ev.GetY())));
			return base.DispatchTouchEvent(ev);
		}

	}
}
