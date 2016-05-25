using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using KeyboardDone.iOS;
using D = System.Diagnostics.Debug;
using System.ComponentModel;
using UIKit;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryExtRenderer_iOS))]
namespace KeyboardDone.iOS
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

	public static class EnumExtensions
	{
		public static UIReturnKeyType GetValueFromDescription(this ReturnKeyTypes value)
		{
			var type = typeof(UIReturnKeyType);
			if (!type.IsEnum) throw new InvalidOperationException();
			foreach (var field in type.GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field,
					typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attribute != null)
				{
					if (attribute.Description == value.ToString())
						return (UIReturnKeyType)field.GetValue(null);
				}
				else
				{
					if (field.Name == value.ToString())
						return (UIReturnKeyType)field.GetValue(null);
				}
			}
			throw new NotSupportedException($"Not supported on iOS: {value}");
		}
	}
}

