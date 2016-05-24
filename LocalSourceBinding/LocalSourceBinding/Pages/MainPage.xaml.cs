using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LocalSourceBinding
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			UpdateFeedPage.CurrentSelectedFile.Add(new FileObject { Name="First" });
		}
		async void onPageClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new UpdateFeedPage());
		}
	}
}

