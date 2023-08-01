using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ChessnutBoardEmulator.BoardModels;
using ChessnutBoardEmulator.Common;

namespace ChessnutBoardEmulator.ViewModels;

public class BoardViewModel : BaseViewModel
{
    private bool _isBoardReversed;

    public bool IsBoardReversed
    {
        get => _isBoardReversed;
        set
        {
            if (SetField(ref _isBoardReversed, value))
            {
                foreach (var square in Squares)
                {
                    square.IsReverseOrder = IsBoardReversed;
                }
            }
        }
    }
    public ObservableCollection<Square> Squares { get; } = new();

    public ScoreViewModel ScoreViewModel { get; } = new();

    public event Action? StartNewGameEvent;
    public event Action? ClearBoardEvent;
    
    public BoardViewModel()
    {
        BuildSquares();
        SetupBoardCommand = new RelayCommand(SetupBoard);
        ClearBoardCommand = new RelayCommand(ClearBoard);
    }
    
    public ICommand SetupBoardCommand { get; }
    public ICommand ClearBoardCommand { get; }

    private void BuildSquares()
    {
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                Squares.Add(new Square(i, j, IsBoardReversed));
            }
        }
    }

    private void SetupBoard()
    {
        ClearBoard();

        foreach (var square in Squares.Where(x => x.Rank is 1 or 8 || x.Rank is 2 or 7))
        {
            if (square.Rank is 2 or 7)
            {
                square.AddPieceToSquare(new Pawn(square.Rank == 2));
                continue;
            }

            switch (square.File)
            {
                case 1:
                case 8:
                    square.AddPieceToSquare(new Rook(square.Rank == 1));
                    break;
                case 2:
                case 7:
                    square.AddPieceToSquare(new Knight(square.Rank == 1));
                    break;
                case 3:
                case 6:
                    square.AddPieceToSquare(new Bishop(square.Rank == 1));
                    break;
                case 4:
                    square.AddPieceToSquare(new Queen(square.Rank == 1));
                    break;
                case 5:
                    square.AddPieceToSquare(new King(square.Rank == 1));
                    break;
            }
        }
        
        StartNewGameEvent?.Invoke();
    }
    
    private void ClearBoard()
    {
        ScoreViewModel.ClearScore();
        
        foreach (var square in Squares)
        {
            square.AddPieceToSquare(null);
        }
        
        ClearBoardEvent?.Invoke();
    }
}