using Foundation;
using System;
using UIKit;
using ObjCRuntime;

namespace ImagePickerController
{
    public partial class myViewController : UIViewController
    {
		UIImagePickerController _imagePickerController;

		public myViewController (IntPtr handle) : base (handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		partial void myButtonTouch(UIButton sender)
		{
			ImagePickerController(UIImagePickerControllerSourceType.Camera);
		}

		public void ImagePickerController(UIImagePickerControllerSourceType sourceType)
		{
			if (_imagePickerController == null)
				_imagePickerController = new UIImagePickerController();
			if (Runtime.Arch == Arch.DEVICE) // No camara on Simulator
				_imagePickerController.SourceType = sourceType;
			else
				if (sourceType == UIImagePickerControllerSourceType.Camera)
					_imagePickerController.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) // Handle ipad correctly 
			{
				if (_imagePickerController.SourceType == UIImagePickerControllerSourceType.Camera)
					_imagePickerController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
				else
					_imagePickerController.ModalPresentationStyle = UIModalPresentationStyle.Popover;
			}
			else
			{
				_imagePickerController.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
			}

			_imagePickerController.Canceled += (object sender, EventArgs e) =>
			{
				Console.WriteLine("Picker Cancelled");
				_imagePickerController.DismissViewController(true, null);
			};
			_imagePickerController.FinishedPickingMedia += (object sender, UIImagePickerMediaPickedEventArgs e) =>
			{
				_imagePickerController.DismissViewController(true, null);
				Console.WriteLine(e.ReferenceUrl);
				if (_imagePickerController.SourceType == UIImagePickerControllerSourceType.Camera)
				{
					// Newly-captured media, save it to the Camera Roll on the device or ....
				}
				else
				{
					// Existing media seleted, do something with it....
				}
			};

			var mainWindow = UIApplication.SharedApplication.KeyWindow;
			var viewController = mainWindow?.RootViewController;
			while (viewController?.PresentedViewController != null)
			{
				viewController = viewController.PresentedViewController;
			}
			if (viewController == null)
				viewController = this;
			_imagePickerController.View.Frame = viewController.View.Frame;
			viewController.PresentViewController(_imagePickerController, true, () => { Console.WriteLine("Complete"); });
		}
    }
}