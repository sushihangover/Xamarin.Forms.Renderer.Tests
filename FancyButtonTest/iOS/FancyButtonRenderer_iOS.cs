using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FancyButtonTest;
using FancyButtonTest.iOS;
using UIKit;

[assembly: ExportRenderer (typeof(FancyButton), typeof(FancyButtonRenderer_iOS))]
namespace FancyButtonTest.iOS
{
	public class FancyButtonRenderer_iOS : ViewRenderer<FancyButton, UIButton>
	{
		UIButton _button;

		public FancyButtonRenderer_iOS () { }

		protected override void OnElementChanged (ElementChangedEventArgs<FancyButton> e)
		{
			base.OnElementChanged (e);

			if (Control == null) {
				_button = new UIButton (UIButtonType.RoundedRect);
				_button.BackgroundColor = UIColor.LightGray;
				SetNativeControl (_button);
			}
			if (e.OldElement != null) {
				_button.TouchUpInside -= OnButtonTapped;
			}
			if (e.NewElement != null) {
				_button.TouchUpInside += OnButtonTapped;
			}
		}

		void OnButtonTapped (object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine ("FancyButton TouchUpInside");
		}
	}
}

