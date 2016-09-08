using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using CoreAnimation;

namespace TabBadges
{
	public static class Extensions
	{
		public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
		{
			var i = 0;
			foreach (var e in ie) action(e, i++);
		}
	}
	public partial class MainTabBarController : UITabBarController
	{

		public MainTabBarController(IntPtr handle) : base(handle)
		{
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.Black;
			mainTabBar.Items.Each((UITabBarItem item, int index) =>
			{
				item.BadgeValue = "Normal";
				foreach (var badgeView in mainTabBar.Subviews[index].Subviews)
				{
					if (index > 0)
						if (badgeView.Class.Name.Contains("_UIBadgeView"))
						{
							badgeView.Layer.Transform = new CATransform3D();
							badgeView.Layer.Transform = CATransform3D.MakeTranslation(-100, -20, 1);
							item.BadgeValue = "Custom";
							item.BadgeColor = UIColor.Green;
							item.SetBadgeTextAttributes(new UIStringAttributes() { ForegroundColor = UIColor.Black }, UIControlState.Normal);
							return;
						}
				}
			});
		}
	}
}