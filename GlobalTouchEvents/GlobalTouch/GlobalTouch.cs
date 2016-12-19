using System;

using Xamarin.Forms;

namespace GlobalTouch
{
	public class App : Application
	{
		public App()
		{
	DependencyService.Get<IGlobalTouch>().Subscribe((sender, e) =>
	{
		var point = (e as TouchEventArgs<Point>).EventData;
		System.Diagnostics.Debug.WriteLine($"{point.X}:{point.Y}");
	});

			// The root page of your application
			var content = new ContentPage
			{
				Title = "GlobalTouch",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							HorizontalTextAlignment = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
				}
			};

			MainPage = new NavigationPage(content);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
