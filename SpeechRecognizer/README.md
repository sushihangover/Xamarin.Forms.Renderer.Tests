###Android Google Speech Recognizer results

Ref: [http://stackoverflow.com/questions/40614131/android-speech-recognition-pass-data-back-to-xamarin-forms](http://stackoverflow.com/questions/40614131/android-speech-recognition-pass-data-back-to-xamarin-forms)

###My Answer:

I would use an `AutoResetEvent` to pause the return until the `OnActivityResult` is called, which would be until the user either records something, cancels, or you timeout their action in the AutoResetEvent.

###Return a `Task<string>` from your `SpeechToTextAsync` method:

	public interface ISpeechToText
	{
		Task<string> SpeechToTextAsync();
	}

###Add an `AutoResetEvent` to *pause* execution:

Note: Wrapping the `AutoResetEvent.WaitOne` to prevent hanging the application looper

	public class SpeechToText_Android : Listener.ISpeechToText
	{
		public static AutoResetEvent autoEvent = new AutoResetEvent(false);
		public static string SpeechText;
		const int VOICE = 10;

		public async Task<string> SpeechToTextAsync()
		{
			var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
			voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
			voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Sprechen Sie jetzt");
			voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
			voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
			voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
			voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
			voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);

			SpeechText = "";
			autoEvent.Reset();
			((Activity)Forms.Context).StartActivityForResult(voiceIntent, VOICE);
			await Task.Run(() => { autoEvent.WaitOne(new TimeSpan(0, 2, 0)); });
			return SpeechText;
		}
	}

###MainActivity OnActivityResult:

		const int VOICE = 10;
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (requestCode == VOICE)
			{
				if (resultCode == Result.Ok)
				{
					var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
					if (matches.Count != 0)
					{
						var textInput = matches[0];
						if (textInput.Length > 500)
							textInput = textInput.Substring(0, 500);
						SpeechToText_Android.SpeechText = textInput;
					}
				}
				SpeechToText_Android.autoEvent.Set();
			}
		}

Note: This is making use of a couple of static vars to simplify the implementation of this example... Some developers would say this is a code smell, I semi-agree, but you can not ever have more then one Google Speech Recognizer running at a time.... 

###Hello World Example:

	public class App : Application
	{
		public App()
		{
			var speechTextLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				Text = "Waiting for text"
			};

			var speechButton = new Button();
			speechButton.Text = "Fetch Speech To Text Results";
			speechButton.Clicked += async (object sender, EventArgs e) =>
			{
				var speechText = await WaitForSpeechToText();
				speechTextLabel.Text = string.IsNullOrEmpty(speechText) ? "Nothing Recorded" : speechText;
			};

			var content = new ContentPage
			{
				Title = "Speech",
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							HorizontalTextAlignment = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						},
						speechButton,
						speechTextLabel
					}
				}
			};
			MainPage = new NavigationPage(content);
		}

		async Task<string> WaitForSpeechToText()
		{
			return await DependencyService.Get<Listener.ISpeechToText>().SpeechToTextAsync();
		}
	}

[![enter image description here][1]][1]


  [1]: https://i.stack.imgur.com/3FULp.jpg