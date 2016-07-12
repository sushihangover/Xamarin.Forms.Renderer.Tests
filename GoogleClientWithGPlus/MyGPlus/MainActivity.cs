using System;
using Android;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Plus;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace MyGPlus
{
	[Activity(Label = "MyGPlus", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
	{
		GoogleApiClient client;
		bool _resolvingError;
		const string TAG = "MyGPlus";
		const int REQUEST_RESOLVE_ERROR = 999;

		public async void OnConnected(Bundle connectionHint)
		{
			Log.Debug(TAG, "OnConnected");
			if (client.IsConnected)
			{
				var emailAddress = PlusClass.AccountApi.GetAccountName(client);
				Log.Debug(TAG, emailAddress);

				var peopleResult = await PlusClass.PeopleApi.LoadAsync(client, new string[] { "me" });
				if (peopleResult.Status.StatusCode == CommonStatusCodes.Success)
				{
					if (peopleResult.PersonBuffer.Count > 0)
					{
						try // try/catch is needed as emumerator will always(?) fault in move next, broken api
						{
							foreach (var person in peopleResult.PersonBuffer)
							{
								Log.Debug(TAG, person.DisplayName);
								Log.Debug(TAG, person.Id);
								Log.Debug(TAG, person.Url);
								if (person.HasImage)
									Log.Debug(TAG, person.Image.Url);
								Log.Debug(TAG, person.AboutMe);
								person.Dispose();
							}
						}
						catch (Exception e)
						{
							Log.Debug(TAG, $"{e.Message}");
						}
						finally
						{
							peopleResult.Release(); // prevent memory leak
						}
					}
				}
				else
				{
					Log.Debug(TAG, $"{peopleResult.Status.StatusMessage}");
				}
			}
		}

		public void OnConnectionFailed(ConnectionResult result)
		{
			Log.Debug(TAG, "OnConnectionFailed");
			if (_resolvingError)
				return;
			if (result.HasResolution)
			{
				try
				{
					_resolvingError = true;
					result.StartResolutionForResult(this, REQUEST_RESOLVE_ERROR);
				}
				catch (IntentSender.SendIntentException e)
				{
					Log.Debug(TAG, e.Message);
					client.Connect();
				}
			}
			else
			{
				ShowErrorDialog(result.ErrorCode);
			}
		}

		void ShowErrorDialog(int errorCode)
		{
			var dialogFragment = new ErrorDialogFragment();
			var args = new Bundle();
			args.PutInt("dialog_error", errorCode);
			dialogFragment.Arguments = args;
			dialogFragment.Show(FragmentManager, "errordialog");
		}

		public void OnConnectionSuspended(int cause)
		{
			Log.Debug(TAG, "OnConnectionSuspended");
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (requestCode == REQUEST_RESOLVE_ERROR)
			{
				if (resultCode == Result.Ok)
				{
					if (!client.IsConnecting && !client.IsConnected)
					{
						client.Connect();
					}
				}
				else if (resultCode == Result.Canceled)
				{
					Log.Debug(TAG, "Result.Canceled; Did you register the app?");
					// https://console.developers.google.com/apis/credentials

				}
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.GPlusLogin);
			Button loginButton = FindViewById<Button>(Resource.Id.myLogin);
			Button logoutButton = FindViewById<Button>(Resource.Id.myLogout);
			loginButton.Click += delegate
			{
				//StartActivity(typeof(PlayActivity)); // AutoManaged Way
				client = new GoogleApiClient.Builder(Application.Context)
					.UseDefaultAccount()
					.AddConnectionCallbacks(this)
					.AddOnConnectionFailedListener(this)
					.AddApi(PlusClass.API)
					.AddScope(PlusClass.ScopePlusLogin)
					.AddScope(PlusClass.ScopePlusProfile)
					.Build();
				_resolvingError = false;
				client.Connect();
			};
			logoutButton.Click += delegate
			{
				client?.Disconnect();
			};
		}
	}
}


