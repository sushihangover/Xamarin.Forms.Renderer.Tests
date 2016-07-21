using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Text.RegularExpressions;
using Android.Runtime;
using System;
using Android.Util;
using Android.Text;
using Java.Lang;

namespace zipcodeval
{
	[Activity(Label = "zipcodeval", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, View.IOnKeyListener, IInputFilter
	{


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);

			var button = FindViewById<Button>(Resource.Id.btnSearch);
			var zipCodeEntry = FindViewById<TextView>(Resource.Id.zipCodeEntry);
			//zipCodeEntry.SetOnKeyListener(this);
			zipCodeEntry.SetFilters(new IInputFilter[] { this });

			button.Click += delegate
			{
				if (!ValidateZipCode(zipCodeEntry.Text))
				{
					zipCodeEntry.Error = "Enter Valid USA Zip Code";
					return;
				}
				DoSubmit();
			};
		}

		public bool OnKey(View view, [GeneratedEnum] Keycode keyCode, KeyEvent e)
		{
			if (view.Id == Resource.Id.zipCodeEntry)
			{
				Log.Debug("V", keyCode.ToString()); // Validate key by key
			}
			return false;
		}

		protected bool ValidateZipCode(string zipCode)
		{
			string pattern = @"^\d{5}(\-\d{4})?$";
			var regex = new Regex(pattern);
			Log.Debug("V", regex.IsMatch(zipCode).ToString());
			return regex.IsMatch(zipCode);
		}

		protected void DoSubmit()
		{
			// yadda yadda yadda
		}

		public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = start; i < end; i++)
			{
				if ((Character.IsDigit(source.CharAt(i)) || source.CharAt(i) == '-'))
					sb.Append(source.CharAt(i));
			}
			return (sb.Length() == (end - start)) ? null : new Java.Lang.String(sb.ToString());
		}
	}
}


