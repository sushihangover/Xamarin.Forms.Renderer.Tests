using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ModalPageNavigation
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			loginDone.Clicked += OnLoginClick;
		}
		async void OnLoginClick(object sender, EventArgs e)
		{
			// If Login is complete/succesful - set new root page
			Application.Current.MainPage = new MainApplicationPage();
			// Pops all but the root Page off the navigation stack, with optional animation.
			await Navigation.PopToRootAsync(true);


			//You are looking for PopToRootAsync.So your user enters required info and they tap a login button, you perform your login verification and if success you set a new MainPage and then PopToRootAsync which pops all but the root Page off the navigation stack.
		}
	}
}

