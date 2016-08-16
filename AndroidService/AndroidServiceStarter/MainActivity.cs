using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace AndroidServiceStarter
{
	[Activity(Label = "AndroidServiceStarter", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		bool started = false;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);
			button.Click += delegate
			{
				var intent = new Intent();
				intent.SetClassName("com.sushihangover.androidservice", "com.sushihangover.androidservice.MyMostAmazingService");
				if (started)
					StartService(intent);
				else
					StopService(new Intent(intent));
				started = !started;
			};
		}
	}
}

