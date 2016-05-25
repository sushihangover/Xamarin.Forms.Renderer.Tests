A custom `EntryRenderer` can handle changing the keyboard return key description.

* iOS : `UITextField` has a `ReturnKeyType` property that you can set to a **preassigned** list (see `UIReturnType` enum). 

* Android : `EntryEditText` has a `ImeOptions` property that controls what the "Action" button on the keyboard does and a `SetImeActionLabel` method that you can use to set any text string for it.

[![enter image description here][1]][1]

###A `Xamarin.Forms` custom `Entry` class:

    namespace YourNameSpaceHere
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

###iOS custom `EntryRenderer`:

    [assembly: ExportRenderer(typeof(Entry), typeof(EntryExtRenderer_iOS))]
    namespace YourNameSpaceHere.iOS
    {
    	public class EntryExtRenderer_iOS : EntryRenderer
    	{
    		public EntryExtRenderer_iOS() {	}
    
    		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    		{
    			base.OnElementChanged(e);
    			if ((Control != null) & (e.NewElement != null))
    				Control.ReturnKeyType = (e.NewElement as EntryExt).ReturnKeyType.GetValueFromDescription();
    		}
    
    		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    		{
    			base.OnElementPropertyChanged(sender, e);
    			if (e.PropertyName == EntryExt.ReturnKeyPropertyName)
    			{
    				D.WriteLine($"{(sender as EntryExt).ReturnKeyType.ToString()}");
    				Control.ReturnKeyType = (sender as EntryExt).ReturnKeyType.GetValueFromDescription();
    			}
    		}
    	}
    }

###Android custom `EntryRenderer`:

    [assembly: ExportRenderer(typeof(Entry), typeof(EntryExtRenderer_Droid))]
    namespace YourNameSpaceHere.Droid
    {
    	public class EntryExtRenderer_Droid : EntryRenderer
    	{
    		public EntryExtRenderer_Droid() { }
    
    		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    		{
    			base.OnElementChanged(e);
    			if ((Control != null) & (e.NewElement != null))
    			{
    				var entryExt = (e.NewElement as EntryExt);
    				Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();
    				// This is hackie ;-) / A Android-only bindable property should be added to the EntryExt class 
    				Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);
    			}
    		}
    		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    		{
    			base.OnElementPropertyChanged(sender, e);
    			if (e.PropertyName == EntryExt.ReturnKeyPropertyName)
    			{
    				var entryExt = (sender as EntryExt);
    				Control.ImeOptions = entryExt.ReturnKeyType.GetValueFromDescription();
    				// This is hackie ;-) / A Android-only bindable property should be added to the EntryExt class 
    				Control.SetImeActionLabel(entryExt.ReturnKeyType.ToString(), Control.ImeOptions);
    			}
    		}
    	}
    }

  [1]: http://i.stack.imgur.com/fTjM7.png
