using System;

using UIKit;
using CoreGraphics;
using System.Runtime.CompilerServices;

namespace PixelColor
{
	public partial class ViewController : UIViewController
	{
		UITextView text;
		UIImageView view;
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			view = new UIImageView(UIScreen.MainScreen.Bounds);
			view.Image = UIImage.FromFile("ja.jpg");
			text = new UITextView(new CGRect(0, 20, UIScreen.MainScreen.Bounds.Width, 40));
			view.Add(text);
			Add(view);
		}

		public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			GetColorUnderTouch(evt);
		}

		public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			GetColorUnderTouch(evt);
		}

		void GetColorUnderTouch(UIEvent evt)
		{
			var touch = evt.AllTouches.AnyObject as UITouch;
			var point = touch.LocationInView(view);
			var color = GetColorAtTouchPoint(point);
			text.Text = color.ToString();
		}

		byte[] alphaPixel = { 0, 0, 0, 0 };
		CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
		protected UIColor GetColorAtTouchPoint(CGPoint point)
		{
			var bitmapContext = new CGBitmapContext(alphaPixel, 1, 1, 8, 4, colorSpace, CGBitmapFlags.PremultipliedLast);
			bitmapContext.TranslateCTM(-point.X, -point.Y);
			View.Layer.RenderInContext(bitmapContext);
			return UIColor.FromRGBA(alphaPixel[0], alphaPixel[1], alphaPixel[2], alphaPixel[3]);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

