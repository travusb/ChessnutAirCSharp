using ChessnutBoardEmulator.BoardModels.Interfaces;

namespace ChessnutBoardEmulator.BoardModels;

public class Queen : IChessPiece
{
    public string ImagePath => IsLightPiece ? "pack://application:,,,/ChessnutBoardEmulator;component/Images/wq.png" : "pack://application:,,,/ChessnutBoardEmulator;component/Images/bq.png";
    public bool IsLightPiece { get; }
    public int Value { get; } = 9;

    public Queen(bool isLightPiece)
    {
        IsLightPiece = isLightPiece;
    }
}