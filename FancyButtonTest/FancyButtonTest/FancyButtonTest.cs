using System;

using Xamarin.Forms;

namespace FancyButtonTest
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			var fancyButton = new FancyButton();
			fancyButton.Title = "Stack";
			fancyButton.SubTitle = "Overflow";
			fancyButton.Icon.Source = ImageSource.FromResource("FancyButtonTest.Resources.icon.png");

			MainPage = new ContentPage {
				Content = new StackLayout {
					Padding = new Thickness(10),
					VerticalOptions = LayoutOptions.Center,
					Children = 
					{
						new Frame {
							OutlineColor = Color.Black,
							BackgroundColor = Color.Teal,
							HorizontalOptions = LayoutOptions.Center,
							VerticalOptions = LayoutOptions.Center,
							WidthRequest = 200,
							HeightRequest = 100,
							Content =  new StackLayout {
								Children = {
									new Label {
										XAlign = TextAlignment.Center,
										Text = "Welcome to Xamarin Forms!"
									},
									new Button {
										Text = "Xamarin.Forms Button",
										HorizontalOptions = LayoutOptions.Center,
										VerticalOptions = LayoutOptions.Center											
									}
								}
							}
						},
						new Frame {
							OutlineColor = Color.Black,
							BackgroundColor = Color.Teal,
							HorizontalOptions = LayoutOptions.Center,
							VerticalOptions = LayoutOptions.Center,
							WidthRequest = 200,
							HeightRequest = 100,
							Content = new FancyButton {
								Title = "Stack",
								SubTitle = "OverFlow",
								IconSource = ImageSource.FromResource("FancyButtonTest.Resources.icon.png")
							}
						},
					}
				}
			};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

