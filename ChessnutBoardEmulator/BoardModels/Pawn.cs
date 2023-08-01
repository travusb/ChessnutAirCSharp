using ChessnutBoardEmulator.BoardModels.Interfaces;

namespace ChessnutBoardEmulator.BoardModels;

public class Pawn : IChessPiece
{
    public string ImagePath => IsLightPiece ? "pack://application:,,,/ChessnutBoardEmulator;component/Images/wp.png" : "pack://application:,,,/ChessnutBoardEmulator;component/Images/bp.png";
    public bool IsLightPiece { get; }
    public int Value { get; } = 1;

    public Pawn(bool isLightPiece)
    {
        IsLightPiece = isLightPiece;
    }
}