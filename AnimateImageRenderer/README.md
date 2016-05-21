#Looping Image

**iOS**: This can be done via applying an array of `UIImage`s to a `UIImageView.AnimationImage` property.

**Android**: *One way*, is to set a "animation-list" drawable to the background of `ImageView`.

[![enter image description here][1]][1]

(the gif is glitchy, but these two techniques run smooth on devices (and most emulators ;-)

Note: This example code uses 10 images (`frame_X.png`) that are linked under iOS **Resources** and Android **Resources/drawable**.

###`Xamarin.Forms` Custom `Image` Control w/ bindable `Animate` property:

	public class AnimatedImage : Image
	{
		public static readonly BindableProperty AnimateProperty = BindableProperty.Create(
			propertyName: "Animate",
			returnType: typeof(bool),
			declaringType: typeof(AnimatedImage),
			defaultValue: false);

		public bool Animate
		{
			get { return (bool)GetValue(AnimateProperty); }
			set { SetValue(AnimateProperty, value); }
		}
	}

###iOS Custom `ImageRenderer`:

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

###Android Custom `ImageRenderer`:

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

###Android `animation-list` Drawable:

    <?xml version="1.0" encoding="UTF-8" ?>
    <animation-list xmlns:android="http://schemas.android.com/apk/res/android"
        android:oneshot="false">
        <item android:drawable="@drawable/frame_0" android:duration="100" />
        <item android:drawable="@drawable/frame_1" android:duration="100" />
        <item android:drawable="@drawable/frame_2" android:duration="100" />
        <item android:drawable="@drawable/frame_3" android:duration="100" />
        <item android:drawable="@drawable/frame_4" android:duration="100" />
        <item android:drawable="@drawable/frame_5" android:duration="100" />
        <item android:drawable="@drawable/frame_6" android:duration="100" />
        <item android:drawable="@drawable/frame_7" android:duration="100" />
        <item android:drawable="@drawable/frame_8" android:duration="100" />
        <item android:drawable="@drawable/frame_9" android:duration="100" />
    </animation-list>

  [1]: http://i.stack.imgur.com/8yvV6.gif