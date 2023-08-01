using System.Collections.ObjectModel;
using System.Linq;
using ChessnutBoardEmulator.BoardModels;
using ChessnutBoardEmulator.BoardModels.Interfaces;

namespace ChessnutBoardEmulator.ViewModels;

public class ScoreViewModel : BaseViewModel
{
    public ObservableCollection<IChessPiece> WhiteCaptures { get; } = new();
    public ObservableCollection<IChessPiece> BlackCaptures { get; } = new();

    public int WhiteScore => WhiteCaptures.Sum(x => x.Value) - BlackCaptures.Sum(x => x.Value);
    public int BlackScore => BlackCaptures.Sum(x => x.Value) - WhiteCaptures.Sum(x => x.Value);

    public ScoreViewModel()
    {

    }

    public void AddWhiteCapture(IChessPiece piece)
    {
        WhiteCaptures.Add(piece);
        OnPropertyChanged(nameof(WhiteScore));
        OnPropertyChanged(nameof(BlackScore));
    }
    
    public void AddBlackCapture(IChessPiece piece)
    {
        BlackCaptures.Add(piece);
        OnPropertyChanged(nameof(WhiteScore));
        OnPropertyChanged(nameof(BlackScore));
    }

    public void ClearScore()
    {
        WhiteCaptures.Clear();
        BlackCaptures.Clear();
        
        OnPropertyChanged(nameof(WhiteScore));
        OnPropertyChanged(nameof(BlackScore));
    }
}