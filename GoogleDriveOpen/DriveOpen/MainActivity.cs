using System;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace DriveOpen
{
	[Activity(Label = "DriveOpen", MainLauncher = true, Icon = "@mipmap/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
	public class MainActivity : Activity, GoogleApiClient.IConnectionCallbacks, IResultCallback, IDriveApiDriveContentsResult
	{
		int count = 1;
		const string TAG = "GDrive";
		const int REQUEST_CODE_RESOLUTION = 3;
		const int REQUEST_CODE_OPENER = 10;

		GoogleApiClient _googleApiClient;

		public IDriveContents DriveContents
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Statuses Status
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate
			{
				button.Text = string.Format("{0} clicks!", count++);
				if (_googleApiClient == null)
				{
					_googleApiClient = new GoogleApiClient.Builder(this)
					  .AddApi(DriveClass.API)
					  .AddScope(DriveClass.ScopeFile)
					  .AddConnectionCallbacks(this)
					  .AddOnConnectionFailedListener(onConnectionFailed)
					  .Build();
				}
				if (!_googleApiClient.IsConnected)
					_googleApiClient.Connect();
			};
		}

		protected void onConnectionFailed(ConnectionResult result)
		{
			// Called whenever the API client fails to connect.
			Log.Info(TAG, "GoogleApiClient connection failed: " + result);
			if (!result.HasResolution)
			{
				// show the localized error dialog.
				GoogleApiAvailability.Instance.GetErrorDialog(this, result.ErrorCode, 0).Show();
				return;
			}
			// The failure has a resolution. Resolve it.
			// Called typically when the app is not yet authorized, and an authorization dialog is displayed to the user.
			try
			{
				result.StartResolutionForResult(this, REQUEST_CODE_RESOLUTION);
			}
			catch (IntentSender.SendIntentException e)
			{
				Log.Error(TAG, "Exception while starting resolution activity", e);
			}
		}

		public void OnConnected(Bundle connectionHint)
		{
			Log.Info(TAG, "API client connected.");
			IntentSender intentSender = DriveClass.DriveApi
				.NewOpenFileActivityBuilder()
				//.SetMimeType(new string[] { "text/pdf" })
				.Build(_googleApiClient);
			try
			{
				StartIntentSenderForResult(intentSender, REQUEST_CODE_OPENER, null, 0, 0, 0);
			}
			catch (IntentSender.SendIntentException e)
			{
				Log.Warn(TAG, "Unable to send intent", e);
			}
		}

		public void OnConnectionSuspended(int cause)
		{
			throw new NotImplementedException();
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (requestCode == REQUEST_CODE_RESOLUTION)
			{
				switch (resultCode)
				{
					case Result.Ok:
						_googleApiClient.Connect();
						break;
					case Result.Canceled:
						Log.Error(TAG, "Unable to sign in, is app registered for Drive access");
						break;
					case Result.FirstUser:
						Log.Error(TAG, "Unable to sign in: RESULT_FIRST_USER");
						break;
					default:
						Log.Error(TAG, "Should never be here: " + resultCode);
						return;
				}
			}
			if (requestCode == REQUEST_CODE_OPENER)
			{
				switch (resultCode)
				{
					case Result.Ok:
						var driveId = (DriveId)data.GetParcelableExtra(OpenFileActivityBuilder.ExtraResponseDriveId);
						Toast.MakeText(this, "Selected folder's ID: " + driveId, ToastLength.Long).Show();
						var driveFile = driveId.AsDriveFile();
						var driveContentResult = driveFile.OpenAsync(_googleApiClient, DriveFile.ModeReadOnly, null).ContinueWith((resultTask) =>
						{
						var driveContentResults = resultTask.Result;
						var driveContent = driveContentResults.DriveContents;
							Log.Info(TAG, $"{driveContent.DriveId}");
						});
						break;
					case Result.Canceled:
						Log.Error(TAG, "Google Drive Picker Cancelled, no file selected");
						break;
					default:
						Log.Error(TAG, "Result code", resultCode);
						return;
				}
			}
		}

		void IResultCallback.OnResult(Java.Lang.Object result)
		{
			var contentResults = (result).JavaCast<IDriveApiDriveContentsResult>();
			var driveContents = contentResults.DriveContents;
		}
	}
}


