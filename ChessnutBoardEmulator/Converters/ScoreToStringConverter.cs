using System;
using System.Globalization;
using System.Windows.Data;

namespace ChessnutBoardEmulator.Converters;

public class ScoreToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int score)
        {
            if (score <= 0)
                return string.Empty;

            return $"+{score}";
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}