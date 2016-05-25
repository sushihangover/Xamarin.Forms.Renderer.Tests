using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using KeyboardDone.Droid;
using System.ComponentModel;
using Android.Views.InputMethods;
using System;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryExtRenderer_Droid))]
namespace KeyboardDone.Droid
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
	public static class EnumExtensions
	{
		public static ImeAction GetValueFromDescription(this ReturnKeyTypes value)
		{
			var type = typeof(ImeAction);
			if (!type.IsEnum) throw new InvalidOperationException();
			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field,
					typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attribute != null)
				{
					if (attribute.Description == value.ToString())
						return (ImeAction)field.GetValue(null);
				}
				else
				{
					if (field.Name == value.ToString())
						return (ImeAction)field.GetValue(null);
				}
			}
			throw new NotSupportedException($"Not supported on Android: {value}");
		}
	}
}

