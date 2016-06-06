using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace PreferenceScreenExample
{
	[Activity(Label = "PreferenceScreenExample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { 
				button.Text = string.Format("{0} clicks!", count++);
				Intent intent = new Intent(this, typeof(SettingsActivity));
				StartActivity(intent);
			};
		}
	}
}


