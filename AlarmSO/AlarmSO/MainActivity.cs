using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Net;
using Android.Media;
using Java.Lang;

namespace AlarmSO
{
	[Activity(Label = "AlarmSO", Name="com.sushihangover.alarmso.FoobarName", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

			var lvarManager = (AlarmManager)GetSystemService(Context.AlarmService);

			Intent lvarCurrencyIntent = new Intent(this, typeof(CurrencyUpdateService));

			var lvarPendingIntent = PendingIntent.GetBroadcast(this, 0, lvarCurrencyIntent, PendingIntentFlags.CancelCurrent);
			lvarManager.SetInexactRepeating(AlarmType.ElapsedRealtime, 1000, 5000, lvarPendingIntent);
		}
	}

	[BroadcastReceiver(Name = "com.sushihangover.alarmso.Foobar")]
	public class CurrencyUpdateService : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			Toast.MakeText(context, "Running", ToastLength.Short).Show();
			System.Diagnostics.Debug.WriteLine("Running...");
			try
			{
				Uri notification = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
				Ringtone tone = RingtoneManager.GetRingtone(Application.Context, notification);
				tone.Play();
			}
			catch (Error e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}
	}
}


