using ChessnutBoardEmulator.BoardModels.Interfaces;

namespace ChessnutBoardEmulator.BoardModels;

public class Rook : IChessPiece
{
    public string ImagePath => IsLightPiece ? "pack://application:,,,/ChessnutBoardEmulator;component/Images/wr.png" : "pack://application:,,,/ChessnutBoardEmulator;component/Images/br.png";
    public bool IsLightPiece { get; }
    public int Value { get; } = 5;

    public Rook(bool isLightPiece)
    {
        IsLightPiece = isLightPiece;
    }
}