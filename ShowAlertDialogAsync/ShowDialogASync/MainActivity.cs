using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Threading;
using System.Threading.Tasks;
using Android.Util;

namespace ShowDialogASync
{
	[Activity(Label = "ShowDialogASync", MainLauncher = true, Icon = "@mipmap/icon")]
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

			button.Click += async delegate 
			{				
				button.Text = string.Format("{0} clicks!", count++);

				var result = await MostrarMensaje("Stack", "Overflow");
				Log.Debug("respuesta", result.ToString());
			};
		}

		AlertDialog objDialog;
		public async Task<bool> MostrarMensaje(string p_titulo, string p_mensaje)
		{
			objDialog = new AlertDialog.Builder(this)
               .SetTitle(p_titulo)
			   .SetMessage(p_mensaje)
			   .SetCancelable(false)
               .Create();
			bool respuesta = false;
			await Task.Run(() =>
			{
				var waitHandle = new AutoResetEvent(false);
				objDialog.SetButton((int)(DialogButtonType.Positive), "yes", (sender, e) =>
				  {
				   respuesta = true;
				   waitHandle.Set();
			   });

				objDialog.SetButton((int)DialogButtonType.Negative, "no", (sender, e) =>
			   {
				   respuesta = false;
				   waitHandle.Set();
			   });
				RunOnUiThread(() =>
				{
					objDialog.Show();
				});
				waitHandle.WaitOne();
			});
			objDialog.Dispose();
			return respuesta;
		}
	}
}


