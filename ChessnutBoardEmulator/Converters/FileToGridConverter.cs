using System;
using System.Globalization;
using System.Windows.Data;

namespace ChessnutBoardEmulator.Converters;

public class FileToGridConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2)
            return null;

        if (values[0] is int file && values[1] is bool isReversed)
            return isReversed ? Math.Abs(file - 8) : file - 1;

        return null;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}