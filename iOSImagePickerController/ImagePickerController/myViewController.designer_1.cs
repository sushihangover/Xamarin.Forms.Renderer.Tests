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

namespace ImagePickerController
{
    [Register ("myViewController")]
    partial class myViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton myButton { get; set; }

        [Action ("myButtonTouch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void myButtonTouch (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (myButton != null) {
                myButton.Dispose ();
                myButton = null;
            }
        }
    }
}