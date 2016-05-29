using System.IO;
using Foundation;
using UIKit;
using AVFoundation;
using AVKit;
using AVPlayerViaDepService.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(NativePlayer))]
namespace AVPlayerViaDepService.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}

	public class NativePlayer : INativeMediaPlayer
	{
		public NativePlayer() { }

		AVPlayerViewController playerVC;
		AVPlayer player;
		public void PlayMediaFile(string mediaFile)
		{
			var multiMediaFile = NSBundle.MainBundle.GetUrlForResource(Path.GetFileNameWithoutExtension(mediaFile), Path.GetExtension(mediaFile));
			var playerItem = new AVPlayerItem(multiMediaFile);
			NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, DonePlaying);

			player = new AVPlayer(playerItem);
			playerVC = new AVPlayerViewController();
			playerVC.Player = player;
			playerVC.AllowsPictureInPicturePlayback = true;
			var mainWindow = UIApplication.SharedApplication.KeyWindow;
			var viewController = mainWindow.RootViewController;
			while (viewController.PresentedViewController != null)
			{
				viewController = viewController.PresentedViewController;
			}
			playerVC.View.Frame =  viewController.View.Frame;
			viewController.PresentViewController(playerVC, true, () => { player.Play(); });
		}

		public void DonePlaying(NSNotification notification)
		{
			playerVC.DismissModalViewController(true);
			NSNotificationCenter.DefaultCenter.RemoveObserver(AVPlayerItem.DidPlayToEndTimeNotification);
			playerVC.Dispose();
			player.Dispose();
		}	
	}
}

