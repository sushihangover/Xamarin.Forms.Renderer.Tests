using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ServiceModel.Channels;
using System.Diagnostics.Contracts;
using D = System.Diagnostics.Debug;

namespace MessagingCenterAsync
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LibraryChoicePage : ContentPage
	{
		public LibraryChoicePage()
		{
			InitializeComponent();

			var semaphone = new SemaphoreSlim(1);
			MessagingCenter.Subscribe<object>(this, "ClearStackLayout", async (sender) =>
			{
			   await semaphone.WaitAsync();
			   Device.BeginInvokeOnMainThread(() =>
			   {
				   _choices.Children.Clear();
			   });
			   semaphone.Release();
			});

			MessagingCenter.Subscribe<object, View>(this, "AddToStackLayout", async (sender, arg) =>
			{
				await semaphone.WaitAsync();
				Device.BeginInvokeOnMainThread(() =>
				{
					_choices.Children.Add(arg);
				});
				semaphone.Release();
			});

			addToStackLayout.Clicked += (object sender, EventArgs e) =>
			{
				Random random = new Random();
				var tasks = new List<Task>();
				for (int i = 0; i < 1000; i++)
				{
					if (random.NextDouble() > .1)
						tasks.Add(Task.Factory.StartNew(() => { AddLayout(); }));
					else
						tasks.Add(Task.Factory.StartNew(() => { ClearLayout(); }));
				}
				var completed = Task.Factory.ContinueWhenAll(tasks.ToArray(), (messagecenterTasks) =>
				{
					foreach (var task in messagecenterTasks)
					{
						if (task.Status == TaskStatus.Faulted)
						{
							D.WriteLine("Faulted:");
							D.WriteLine($"  {task.Exception.Message}");
						}
					}
				}).Wait(1000);
				if (!completed)
					D.WriteLine("Some tasks did not complete in time allocated");
			};

			clearStackLayput.Clicked += async (object sender, EventArgs e) =>
			{
				await Task.Run(() => ClearLayout());
			};
		}

		int itemNo = 0;
		protected void AddLayout()
		{
			itemNo += 1;
			MessagingCenter.Send<object, View>(this, "AddToStackLayout", new Button() { Text = itemNo.ToString() });
		}

		protected void ClearLayout()
		{
			itemNo += 1;
			MessagingCenter.Send<object>(this, "ClearStackLayout");
		}
	}
}


