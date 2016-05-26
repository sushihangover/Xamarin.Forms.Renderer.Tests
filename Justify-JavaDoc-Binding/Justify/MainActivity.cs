using Android.App;
using Android.Widget;
using Android.OS;
using Android.Text;
using Android.Test.Mock;
using Android.Test.Suitebuilder;
using Android.Graphics;
using Com.Bluejamesbond.Text;
using Com.Bluejamesbond.Text.Hyphen;
using Com.Bluejamesbond.Text.Style;

namespace Justify
{
	[Activity (Label = "Justify", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
				// Create span
//				var span = new SpannableString("In New York and New Jersey, governors Andrew Cuomo and Chris Christie have implemented controversial quarantines.");
//
//				// Set region to justify
//				span.SetSpan(new JustifiedSpan(), 0, span.Length(), SpanTypes.InclusiveExclusive);
//
//				// Create DocumentView and set span.
//				// Important: Use SpannedDocumentLayout.class
//				DocumentView documentView = new DocumentView(this, DocumentView.FormattedText);  // Support spanned text
//
//				// Set the fallback alignment if an alignment is not specified for a line
//				documentView.DocumentLayoutParams.TextAlignment = TextAlignment.Justified;
//
//				documentView.TextFormatted = span;
//
////				documentView.Text = span; 
//
//				//  Text = spa .setText(span, true); // Set to `true` to enable 
//
				DocumentView documentView = new DocumentView(this, DocumentView.PlainText);  // Support plain text
				documentView.DocumentLayoutParams.TextAlignment = TextAlignment.Justified;
				documentView.Text = "foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar foobar fofoobar foobar foobar foobar foobar foobar foobar fofoobar foobar foobar foobar foobar foobar foobar fofoobar foobar foobar foobar foobar foobar foobar fofoobar foobar foobar foobar foobar foobar foobar fofoobar foobar foobar foobar foobar foobar foobar fofoobar foobar foobar foobar foobar foobar foobar fofoobar foobar foobar foobar foobar foobar foobar fo ";
				documentView.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
				documentView.SetBackgroundColor(Color.LightGreen);
				var sv = FindViewById<ScrollView> (Resource.Id.scrollView1);
				sv.SetBackgroundColor(Color.LightYellow);
				sv.AddView(documentView);
			};
		}
	}
}


