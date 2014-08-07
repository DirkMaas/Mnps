using System;
using Cirrious.CrossCore.Converters;
using MonoTouch.UIKit;
using System.Collections.Generic;
using CollectABull.Core.Services.Collections;
using CollectABull.Core.Services.DataStore;
using System.Globalization;

namespace CollectABull.Touch
{
	public class TextToColorValueConverter : MvxValueConverter<string, UIColor>
	{
		private readonly Dictionary<string, UIColor> _convertDict;
		private readonly Dictionary<UIColor, string> _convertBackDict;
		public TextToColorValueConverter(ICollectionService cs)
		{
			_convertDict = new Dictionary<string, UIColor>();
			_convertBackDict = new Dictionary<UIColor, string>();

			List<colorsV> cV = cs.GetAllColorsV();
			foreach (colorsV c in cV) 
			{
//				UIColor color = UIColor.FromRGB(c.red, c.green, c.blue);
//				_convertDict.Add(c.color, color);
//				_convertBackDict.Add(color, c.color);
			}
		}
		// Convert goes from ViewModel to the View
		// the <type> is the type from the ViewModel
		protected override UIColor Convert(string value, Type targetType, object parameter, CultureInfo culture)
		{
			return _convertDict[value];
		}
		protected override string ConvertBack(UIColor value, Type targetType, object parameter, CultureInfo culture)
		{
			return _convertBackDict[value];
		}

	}
}

