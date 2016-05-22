1st) Tag your broadcast receiver class with the `BroadcastReceiver` attribute so that the appropriate tag gets generated in your `AndroidManifest.xml` at compile time:

	[BroadcastReceiver]
	public class CurrencyUpdateService : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			Toast.MakeText(context, "Running", ToastLength.Short).Show();
		}
	}

Now your receiver will start toasting every 5 seconds.

**Note**: Xamarin auto-generates Android class name identifiers so if you do not hard-code a name for your class, there is no way to manually edit the manifest to add this item.



###AndroidManifest.xml from the above code generates (for me):

    <receiver android:name="md57db81284125fca721c48509d8e9cea0b.CurrencyUpdateService" />

Now if you manually add a `Name` to the `Attribute`, you can control what is generated. This is needed in cases where other Android applications need to call you and need a pre-defined (static) name to do so.

	[BroadcastReceiver(Name = "com.sushihangover.alarmso.Foobar")]
	public class CurrencyUpdateService : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			Toast.MakeText(context, "Running", ToastLength.Short).Show();
		}
	}

**Note**: [Do not use Android-style "dot name"][1], use the fully qualified name that includes your package name, see link for details

###AndroidManifest.xml now contains:

    <receiver android:name="com.sushihangover.alarmso.Foobar" />


2nd) As far as why you are trying to start a new process, remember that a `BroadcastReceiver` is not an Android `Service`... You can remove the `:process` tag in your manifest as it is not needed for what you're are doing.

If you really are looking to create and start a local or background service, you should checkout:

* https://developer.xamarin.com/guides/android/application_fundamentals/services/part_1_-_started_services/


  [1]: http://stackoverflow.com/a/36472280/4984832
