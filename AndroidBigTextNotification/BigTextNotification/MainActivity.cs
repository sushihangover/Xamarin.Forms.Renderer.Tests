using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Media;
using Android.Content;
using Java.Lang;
using Java.Util;

namespace BigTextNotification
{
	[Activity(Label = "BigTextNotification", MainLauncher = true, Icon = "@mipmap/icon")]
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

			button.Click += delegate
			{
				var notification = new Notification.Builder(Application.Context)
					.SetSmallIcon(Resource.Mipmap.Icon)
					.SetLargeIcon(BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Mipmap.Icon))
					.SetAutoCancel(true)
					.SetStyle(new Notification
							  .BigTextStyle()
				              .SetSummaryText("Summary Text")
				              .SetBigContentTitle("Content Title")
							  .BigText("Big Text Area")
				             )
					.Build();
				var notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
				notificationManager.Notify(1, notification);
			};
		}
	}
}

