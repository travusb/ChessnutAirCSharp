using ChessnutBoardEmulator.BoardModels.Interfaces;

namespace ChessnutBoardEmulator.BoardModels;

public class Bishop : IChessPiece
{
    public string ImagePath => IsLightPiece ? "pack://application:,,,/ChessnutBoardEmulator;component/Images/wb.png" : "pack://application:,,,/ChessnutBoardEmulator;component/Images/bb.png";
    public bool IsLightPiece { get; }
    public int Value { get; } = 3;

    public Bishop(bool isLightPiece)
    {
        IsLightPiece = isLightPiece;
    }
}