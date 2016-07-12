using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace PCLStorageTest.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			Console.WriteLine(documents);

			return base.FinishedLaunching(app, options);
		}
	}
}

