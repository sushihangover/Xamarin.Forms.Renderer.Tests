using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace LocalSourceBinding
{
	public class FileObject
	{
		public string Name
		{
			get;
			set;
		}
	}
	public partial class UpdateFeedPage : ContentPage
	{
		public static readonly ObservableCollection<FileObject> CurrentSelectedFile = new ObservableCollection<FileObject>();
		//public static List<FileObject> CurrentSelectedFile = new List<FileObject>();
		public static string PostText = "";

		public UpdateFeedPage()
		{
			InitializeComponent();
			UpdateFeedEditor.Text = PostText;
		}
		protected override void OnAppearing()
		{
			// Original 
			//AttachedFilesListView.ItemsSource = CurrentSelectedFile;
			//UpdateFeedEditor.Focus();
			//base.OnAppearing();

			// Using a List collection
			//base.OnAppearing();
			//AttachedFilesListView.ItemsSource = null;
			//AttachedFilesListView.ItemsSource = CurrentSelectedFile;

			// Using a ObservableCollection
			base.OnAppearing();
			AttachedFilesListView.ItemsSource = CurrentSelectedFile;
		}
		async void onPostClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
		async void onAttachFileClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new FilePickerPage());
		}
	}
}

