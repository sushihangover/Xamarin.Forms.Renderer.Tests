using Android.App;
using Android.Widget;
using Android.OS;
using System.Reflection;
using Android;
using Android.Util;
using System.Linq;
using System.Collections.Generic;

namespace RunTimePermissions
{
	[Activity(Label = "RunTimePermissions", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		const int PermissionSMSRequestCode = 99;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);
			button.Click += delegate { 
				if ((int)Build.VERSION.SdkInt < 23) // Permissions defined during install
					DoSomeWork();

				var permission = BaseContext.CheckSelfPermission(Manifest.Permission.ReadSms);
				if (permission == Android.Content.PM.Permission.Granted) // Did the user already grant permission?
					DoSomeWork();
				else // Ask the user to allow/deny permission request
					RequestPermissions(new string[] { Manifest.Permission.ReadSms }, PermissionSMSRequestCode);
			};
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
		{
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			if (requestCode == PermissionSMSRequestCode)
			{
				if ((grantResults.Count() > 0) && (grantResults[0] == Android.Content.PM.Permission.Granted))
					DoSomeWork();
				else
					Log.Debug("PERM", "The user denied access!");
			}
		}

		protected void DoSomeWork()
		{
			Log.Debug("PERM", "We have permission, so do something with it");
		}
	}
}


