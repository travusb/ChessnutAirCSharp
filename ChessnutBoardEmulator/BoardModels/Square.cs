using ChessnutBoardEmulator.BoardModels.Interfaces;
using ChessnutBoardEmulator.ViewModels;

namespace ChessnutBoardEmulator.BoardModels;

public class Square : BaseViewModel
{
    private bool _isReversedOrder;
    public bool IsReverseOrder
    {
        get => _isReversedOrder;
        set => SetField(ref _isReversedOrder, value);
    }
    public int Rank { get; }
    public int File { get; }
    public bool IsLightSquare { get; private set; }

    private IChessPiece? _currentChessPiece;
    public IChessPiece? CurrentChessPiece
    {
        get => _currentChessPiece;
        private set => SetField(ref _currentChessPiece, value);
    }

    public Square(int rank, int file, bool isReversedOrder)
    {
        Rank = rank;
        File = file;
        IsReverseOrder = isReversedOrder;
        
        IsLightSquare = File % 2 == 0 ? Rank % 2 != 0 : Rank % 2 == 0;
    }

    public void AddPieceToSquare(IChessPiece? currentChessPiece)
    {
        CurrentChessPiece = currentChessPiece;
    }
}