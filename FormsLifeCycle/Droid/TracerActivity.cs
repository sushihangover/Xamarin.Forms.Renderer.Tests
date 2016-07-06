using Android.App;
using Android.Content;
using Android.OS;
using Java.Lang;

namespace FormsLifeCycle.Droid
{
	public class FormsLifeCycleActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		const string TAG = "FormsLifeCycleActivity";

		public FormsLifeCycleActivity() { }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			notify("onCreate");
		}

		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);
			notify("onCreate PersistableBundle");
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			notify("OnSaveInstanceState");
		}

		protected override void OnRestoreInstanceState(Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState(savedInstanceState);
			notify("OnSaveInstanceState");
		}

		public override void OnRestoreInstanceState(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnRestoreInstanceState(savedInstanceState, persistentState);
			notify("OnRestoreInstanceState PersistableBundle");
		}
		protected override void OnStop()
		{
			base.OnStop();
			notify("OnStop");
		}

		protected override void OnPause()
		{
			base.OnPause();
			notify("OnPause");
		}

		protected override void OnResume()
		{
			base.OnResume();
			notify("OnResume");
		}

		protected override void OnRestart()
		{
			base.OnRestart();
			notify("OnResume");
		}

		protected override void OnStart()
		{
			base.OnStart();
			notify("OnStart");
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			notify("OnDestroy");
		}

		public override void OnLowMemory()
		{
			base.OnLowMemory();
			notify("OnLowMemory");
		}

		public override void OnTrimMemory(TrimMemory level)
		{
			base.OnTrimMemory(level);
			notify($"OnTrimMemory {level.ToString()}");
		}

		private void notify(string name)
		{
			#if DEBUG 
			string[] strings = Class.Name.Split('.');
			Notification notification = new Notification.Builder(this)
				.SetContentTitle(name + " " + strings[strings.Length - 1]).SetAutoCancel(true)
				.SetSmallIcon(Resource.Drawable.icon)
				.SetContentText(name).Build();
			var notificationManager = (NotificationManager)GetSystemService(NotificationService);
			notificationManager.Notify((int)JavaSystem.CurrentTimeMillis(), notification);
			#else
			Log.Debug(TAG, name);
			#endif
		}
	}
}

