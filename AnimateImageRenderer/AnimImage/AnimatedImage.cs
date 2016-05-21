using Xamarin.Forms;
namespace AnimImage
{
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
}

