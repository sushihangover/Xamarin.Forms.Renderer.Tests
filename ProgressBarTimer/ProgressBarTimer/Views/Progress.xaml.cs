using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProgressBarTimer
{
	public partial class Progress : ContentPage
	{
		public Progress()
		{
			InitializeComponent();

		}

		public void OnButtonClicked(object sender, EventArgs e)
		{
			overlay.IsVisible = true;

			TimeSpan duration = TimeSpan.FromSeconds(5);
			DateTime startTime = DateTime.Now;

			Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
			{
				double progress = (DateTime.Now - startTime).TotalMilliseconds / duration.TotalMilliseconds;
				ProgressBar.Progress = progress;
				bool continueTimer = progress < 1;
				if (!continueTimer)
				{
					// Hide overlay. 
					overlay.IsVisible = false;
				}
				return continueTimer;
			});
		}
	}
}

