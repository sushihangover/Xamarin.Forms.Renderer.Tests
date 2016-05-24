using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LocalSourceBinding
{
	public partial class FilePickerPage : ContentPage
	{
		public FilePickerPage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			UpdateFeedPage.CurrentSelectedFile.Add(new FileObject { Name = "Second" });
		}

	}
}

