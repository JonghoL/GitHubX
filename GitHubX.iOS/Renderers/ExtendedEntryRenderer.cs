using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using GitHubX.Controls;
using GitHubX.iOS.Renderers;
using System.ComponentModel;
using Foundation;
using UIKit;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace GitHubX.iOS.Renderers
{
	public class ExtendedEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			var view = (ExtendedEntry)Element;

			if (view != null)
			{
				SetPlaceholderTextColor (view);
			}
			if (e.OldElement == null) { }
			if (e.NewElement == null) { }
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (ExtendedEntry)Element;

			if (e.PropertyName == ExtendedEntry.PlaceholderTextColorProperty.PropertyName)
				SetPlaceholderTextColor(view);
		}

		void SetPlaceholderTextColor(ExtendedEntry view)
		{
			/*
UIColor *color = [UIColor lightTextColor];
YOURTEXTFIELD.attributedPlaceholder = [[NSAttributedString alloc] initWithString:@"PlaceHolder Text" attributes:@{NSForegroundColorAttributeName: color}];
			*/
			if(string.IsNullOrEmpty(view.Placeholder) == false && view.PlaceholderTextColor != Color.Default) {
				NSAttributedString placeholderString = new NSAttributedString(view.Placeholder, new UIStringAttributes(){ ForegroundColor = view.PlaceholderTextColor.ToUIColor() });
				Control.AttributedPlaceholder = placeholderString;
			}
		}
	}
}

