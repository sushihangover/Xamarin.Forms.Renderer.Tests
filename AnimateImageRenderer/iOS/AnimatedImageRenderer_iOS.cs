using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AnimImage;
using AnimImage.iOS;
using Foundation;
using UIKit;

[assembly: ExportRenderer(typeof(AnimatedImage), typeof(AnimatedImageRenderer_iOS))]
namespace AnimImage.iOS
{
	public class AnimatedImageRenderer_iOS : ImageRenderer
	{
		const int imageCount = 10;
		NSMutableArray imageArray;
		public AnimatedImageRenderer_iOS() {
			imageArray = new NSMutableArray(imageCount);
			for (int i = 0; i < imageCount; i++)
				imageArray.Add(UIImage.FromFile(new NSString($"frame_{i}.png")));
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.AnimationImages = NSArray.FromArray<UIImage>(imageArray);
				Control.AnimationDuration = 1;
				Control.AnimationRepeatCount = 0;
				if (e.NewElement != null)
				{
					if ((e.NewElement as AnimatedImage).Animate)
						Control.StartAnimating();
					else
						Control.StopAnimating();
				}
			}
		}
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == "Animate")
			{
				if ((sender as AnimatedImage).Animate)
					Control?.StartAnimating();
				else
					Control?.StopAnimating();
			}
		}

	}
}

