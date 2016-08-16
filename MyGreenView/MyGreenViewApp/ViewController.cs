using System;

using UIKit;
using GreenViewBinding;
using Foundation;
using System.Runtime.InteropServices;

namespace MyGreenViewApp
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var view = new MyView();
			view.Frame = new CoreGraphics.CGRect(40, 40, 100, 100);
			Add(view);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
