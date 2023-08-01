using System.Windows;
using System.Windows.Controls;

namespace ChessnutBoardEmulator.Views;

public partial class ScoreView : UserControl
{
    public static readonly DependencyProperty IsLightScoreProperty = DependencyProperty.Register("IsLightScore", typeof(bool), typeof(ScoreView));

    public bool IsLightScore
    {
        get => (bool)GetValue(IsLightScoreProperty);
        set => SetValue(IsLightScoreProperty, value);
    }
    
    public ScoreView()
    {
        InitializeComponent();
    }
}