using ChessnutBoardEmulator.BoardModels.Interfaces;

namespace ChessnutBoardEmulator.BoardModels;

public class King : IChessPiece
{
    public string ImagePath => IsLightPiece ? "pack://application:,,,/ChessnutBoardEmulator;component/Images/wk.png" : "pack://application:,,,/ChessnutBoardEmulator;component/Images/bk.png";
    public bool IsLightPiece { get; }
    public int Value { get; } = 0;

    public King(bool isLightPiece)
    {
        IsLightPiece = isLightPiece;
    }
}