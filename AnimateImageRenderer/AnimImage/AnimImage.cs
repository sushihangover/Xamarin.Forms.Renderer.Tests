using System;

using Xamarin.Forms;

namespace AnimImage
{
	public class App : Application
	{
		public App()
		{
			// The root page of your application
			var ai = new AnimatedImage
			{
				Source = "frame_0.png",
				WidthRequest = 250,
				HeightRequest = 250
			};

			var content = new ContentPage
			{
				Title = "AnimImage",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							HorizontalTextAlignment = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						},
						new AbsoluteLayout {
							VerticalOptions = LayoutOptions.CenterAndExpand,
							HorizontalOptions = LayoutOptions.CenterAndExpand,
							HeightRequest = 250,
							WidthRequest = 250,
							Children = {
								ai
							}
						},
						new Button {
							Text = "Start/Stop",
							Command = new Command(() => {
								ai.Animate = !ai.Animate;
						    })
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

