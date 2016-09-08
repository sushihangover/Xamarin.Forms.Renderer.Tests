// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace TabBadges
{
    [Register ("MainTabBarController")]
    partial class MainTabBarController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        TabBadges.AppMainTabBar mainTabBar { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mainTabBar != null) {
                mainTabBar.Dispose ();
                mainTabBar = null;
            }
        }
    }
}