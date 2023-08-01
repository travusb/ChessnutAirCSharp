using System;
using System.Globalization;
using System.Windows.Data;

namespace ChessnutBoardEmulator.Converters;

public class RankToGridConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2)
            return null;

        if (values[0] is int rank && values[1] is bool isReversed)
            return isReversed ? rank - 1 :Math.Abs(rank - 8);

        return null;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}