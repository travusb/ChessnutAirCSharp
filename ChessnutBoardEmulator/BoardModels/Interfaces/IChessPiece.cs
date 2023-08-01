namespace ChessnutBoardEmulator.BoardModels.Interfaces;

public interface IChessPiece
{
    public string ImagePath { get; }
    public bool IsLightPiece { get; }
    public int Value { get; }
}