using System;

using Xamarin.Forms;

namespace PushPageFromNative
{
	public class App : Application
	{
		public App()
		{
			// The root page of your application
			var pushFormBtn = new Button
			{
				Text = "Push Form",
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
			};
			pushFormBtn.Clicked += (sender, e) =>
			{
					DependencyService.Get<IShowForm>().PushPage();
			};

			var content = new ContentPage
			{
				Title = "PushPageFromNative",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							HorizontalTextAlignment = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						},
						pushFormBtn
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
