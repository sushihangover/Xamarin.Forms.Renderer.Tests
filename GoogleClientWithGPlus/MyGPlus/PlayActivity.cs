using System;
using Android.App;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Plus;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;

namespace MyGPlus
{
	[Activity(Label = "PlayActivity")]
	public class PlayActivity : FragmentActivity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
	{
		GoogleApiClient client;
		const string TAG = "MyGPlus";

		public void OnConnected(Bundle connectionHint)
		{
			var emailAddress = PlusClass.AccountApi.GetAccountName(client);
			Log.Debug(TAG, emailAddress);
		}

		public void OnConnectionFailed(ConnectionResult result)
		{
			result.StartResolutionForResult(this, 999);
		}

		public void OnConnectionSuspended(int cause)
		{
			throw new NotImplementedException();
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			client = new GoogleApiClient.Builder(Application.Context)
				.UseDefaultAccount()
				.EnableAutoManage(this, this)
				.AddConnectionCallbacks(this)
				.AddOnConnectionFailedListener(this)
				.AddApi(PlusClass.API)
				.AddScope(PlusClass.ScopePlusLogin)
				.Build();
			client.Connect();
		}
	}
}

