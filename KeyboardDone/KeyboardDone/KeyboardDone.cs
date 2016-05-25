using System;

using Xamarin.Forms;

namespace KeyboardDone
{
	public class App : Application
	{
		public App()
		{
			// The root page of your application
			var content = new ContentPage
			{
				Title = "EntryExt",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							HorizontalTextAlignment = TextAlignment.Center,
							Text = "EntryExtRenderer Example"
						},
						new EntryExt {
							Text = "Next Key",
							ReturnKeyType = ReturnKeyTypes.Next
						},
						new EntryExt {
							Text = "Done Key",
							ReturnKeyType = ReturnKeyTypes.Done
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

