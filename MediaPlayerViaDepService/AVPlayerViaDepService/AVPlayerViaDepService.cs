using System;

using Xamarin.Forms;

namespace AVPlayerViaDepService
{
	public interface INativeMediaPlayer
	{
		void PlayMediaFile(string fileName);
	}

	public class App : Application
	{
		public App()
		{
			// The root page of your application
			var content = new ContentPage
			{
				Title = "AVPlayerViaDepService",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							HorizontalTextAlignment = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						},
						new Button {
							Text = "Play Audio",
							Command = new Command(() => {
								DependencyService.Get<INativeMediaPlayer>().PlayMediaFile("Audio.mp3");
							})
 					    },
						new Button {
							Text = "Play Video",
							Command = new Command(() => {
								DependencyService.Get<INativeMediaPlayer>().PlayMediaFile("Video.mp4");
							})
						 },
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

