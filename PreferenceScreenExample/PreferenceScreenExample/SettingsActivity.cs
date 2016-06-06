
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PreferenceScreenExample
{
	[Activity(Label = "SettingsActivity")]
	public class SettingsActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.PreferencesLayout);
			FragmentManager.BeginTransaction().Add(Resource.Id.PreferencesContainer, new SettingsFragment()).Commit();
		}
	}
}

