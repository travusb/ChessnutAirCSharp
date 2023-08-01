using System.Windows;

namespace ChessnutBoardEmulator.Converters;

public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
{
    public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed) {}
}