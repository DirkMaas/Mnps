using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using CollectABull.Core.Services.DataStore;
using CollectABull.Core.Services.Collections;
using System.Collections.Generic;

namespace CollectABull.Core.Converters
{
	public class ColorsVValueConverter : MvxValueConverter<int, string>
	// TODO Is one instance of this class created and used for the life of the program,
	// or is one created each time one is needed?  Hopefully the former.  Should test this.
    {
		private readonly Dictionary<int, string> _convertDict;
		private readonly Dictionary<string, int> _convertBackDict;

		ColorsVValueConverter(ICollectionService cs)
		{
			_convertDict = new Dictionary<int, string>();
			_convertBackDict = new Dictionary<string, int>();

			List<colorsV> cV = cs.GetAllColorsV();
			foreach (colorsV c in cV) 
			{
				_convertDict.Add(c.key, c.descrip);
				_convertBackDict.Add(c.descrip, c.key);
			}
		}
		// Convert goes from ViewModel to the View
		// the <type> is the type from the ViewModel
		protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
		{
			return _convertDict[value];
		}
		protected override int ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
		{
			return _convertBackDict[value];
		}

    }
}