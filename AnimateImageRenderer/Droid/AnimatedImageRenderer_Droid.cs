using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AnimImage;
using AnimImage.Droid;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(AnimatedImage), typeof(AnimatedImageRenderer_Droid))]
namespace AnimImage.Droid
{
	public class AnimatedImageRenderer_Droid : ImageRenderer
	{
		public AnimatedImageRenderer_Droid() { }

		AnimationDrawable anim;
		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);
			if (Control != null)
			{
				Control.SetBackgroundResource(Resource.Drawable.animatedlogo);
				if (e.NewElement != null)
				{
					if ((e.NewElement as AnimatedImage).Animate)
					{
						(Control.Background as AnimationDrawable)?.Start();
						Control.ImageAlpha = 0;
					}
				}
			}
		}
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == "Animate")
			{
				if ((sender as AnimatedImage).Animate)
				{
					(Control.Background as AnimationDrawable)?.Start();
					Control.ImageAlpha = 0;
				}
				else
				{
					Control.ImageAlpha = 255;
					(Control.Background as AnimationDrawable)?.Stop();
				}
			}
		}
	}
}
