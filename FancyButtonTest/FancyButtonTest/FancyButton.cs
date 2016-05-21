using System;
using Xamarin.Forms;

namespace FancyButtonTest
{
	public class FancyButton : View
	{
		string _Title = "Title";
		string _SubTitle = "SubTitle";
		Image _Icon = new Image();

		public FancyButton () { }

		public string Title {
			get { return _Title; }
			set { _Title = value; }
		}
		public string SubTitle {
			get { return _SubTitle; }
			set { _SubTitle = value; }
		}
		public Image Icon {
			get { return _Icon; }
		}
		public ImageSource IconSource {
			set { _Icon.Source = value; }
		}
	}
}
	
