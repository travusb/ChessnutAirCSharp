using ChessnutBoardEmulator.BoardModels.Interfaces;

namespace ChessnutBoardEmulator.BoardModels;

public class Knight : IChessPiece
{
    public string ImagePath => IsLightPiece ? "pack://application:,,,/ChessnutBoardEmulator;component/Images/wn.png" : "pack://application:,,,/ChessnutBoardEmulator;component/Images/bn.png";
    public bool IsLightPiece { get; }
    public int Value { get; } = 3;

    public Knight(bool isLightPiece)
    {
        IsLightPiece = isLightPiece;
    }
}