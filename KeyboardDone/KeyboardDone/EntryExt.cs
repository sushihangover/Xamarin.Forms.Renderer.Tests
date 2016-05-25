using Xamarin.Forms;

namespace KeyboardDone
{
	public class EntryExt : Entry
	{
		public const string ReturnKeyPropertyName = "ReturnKeyType";

		public EntryExt() { }

		public static readonly BindableProperty ReturnKeyTypeProperty = BindableProperty.Create(
			propertyName: ReturnKeyPropertyName,
			returnType: typeof(ReturnKeyTypes),
			declaringType: typeof(EntryExt),
			defaultValue: ReturnKeyTypes.Done);

		public ReturnKeyTypes ReturnKeyType
		{
			get { return (ReturnKeyTypes)GetValue(ReturnKeyTypeProperty); }
			set { SetValue(ReturnKeyTypeProperty, value); }
		}
	}

	// Not all of these are support on Android, consult EntryEditText.ImeOptions
	public enum ReturnKeyTypes : int
	{
		Default,
		Go,
		Google,
		Join,
		Next,
		Route,
		Search,
		Send,
		Yahoo,
		Done,
		EmergencyCall,
		Continue
	}
}

//Android ImeOptions:
	//ImeAction.Done
	//ImeAction.Go
	//ImeAction.ImeMaskAction
	//ImeAction.ImeNull
	//ImeAction.Next
	//ImeAction.None
	//ImeAction.Previous
	//ImeAction.Search
	//ImeAction.Send
	//ImeAction.Unspecified
