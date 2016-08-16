using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using Android.Util;

namespace AndroidService
{
	[Service(Name="com.sushihangover.androidservice.MyMostAmazingService", Exported = true)]
	[IntentFilter(new String[] { "myservice" }, Categories = new[] { Intent.CategoryDefault })]
	public class PsiServiceHost : Service
	{
		public override IBinder OnBind(Intent intent)
		{
			return null;
		}
		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			Log.Debug("SO", "MyMostAmazingService Has Been Started");
			return base.OnStartCommand(intent, flags, startId);
		}
	}

	[Activity(Label = "AndroidService", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		bool started;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);
			button.Click += delegate {
				if (started)
					StartService(new Intent("my.very.special.service"));
				else
					StopService(new Intent("my.very.special.service"));
				started = !started;
			};
		}
	}
}

