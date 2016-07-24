using System;
using CoreGraphics;
using CoreLocation;
using Foundation;
using Google.Maps;
using UIKit;

namespace GoogleMaps
{
	public partial class ViewController : UIViewController
	{
		MapServices mapServices;
		MapView mapView;
		CLLocationManager locationManager;

		protected ViewController(IntPtr handle) : base(handle) {}

		public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (mapView.MyLocation != null)
			{
				//One-shot location check
				//mapView.RemoveObserver(this, "myLocation", IntPtr.Zero);
				//locationManager.StopUpdatingLocation();
				var loc = new CLLocationCoordinate2D(mapView.MyLocation.Coordinate.Latitude, mapView.MyLocation.Coordinate.Longitude);
				mapView.Camera = new CameraPosition(loc, mapView.MaxZoom - 1, 0, 0);
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//Your Google Dev. Console API Key (with iOS Google Maps SDK enabled!)
			var success = MapServices.ProvideAPIKey(Your API Key Here);
			mapServices = MapServices.SharedServices;

			var button = new UIButton(UIButtonType.System);
			button.Frame = new CGRect(40, 40, 200, 40);
			button.SetTitle("Track My Movement", UIControlState.Normal);
			Add(button);
			button.TouchUpInside += (object sender, EventArgs e) =>
			{
				mapView = new MapView(UIScreen.MainScreen.Bounds);
				mapView.MapType = MapViewType.Hybrid;
				Add(mapView);
				mapView.AddObserver(this, new NSString("myLocation"), NSKeyValueObservingOptions.New, IntPtr.Zero);
				mapView.MyLocationEnabled = true;
				locationManager = new CLLocationManager();
				locationManager.DesiredAccuracy = CLLocation.AccuracyBest;
				locationManager.DistanceFilter = 0;
				locationManager.RequestWhenInUseAuthorization();
				locationManager.LocationsUpdated += (object sender2, CLLocationsUpdatedEventArgs e2) =>
				{
					Console.WriteLine(e2.Locations[e2.Locations.Length -1]);
				};
				locationManager.StartUpdatingLocation();
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

