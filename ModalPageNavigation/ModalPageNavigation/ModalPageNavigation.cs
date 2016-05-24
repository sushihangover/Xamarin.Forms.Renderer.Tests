using System;

using Xamarin.Forms;

namespace ModalPageNavigation
{
	public class App : Application
	{
		public App()
		{
			//PopToRootAsync is not supported globally on iOS, please use a NavigationPage.
			//PopToRootAsync is not supported globally on Android, please use a NavigationPage.
			var navPage = new NavigationPage(new LoginPage());
			NavigationPage.SetHasNavigationBar(navPage.CurrentPage, false);
			MainPage = navPage;
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

