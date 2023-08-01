using System.Linq;
using System.Threading;
using System.Windows.Input;
using ChessnutBoardEmulator.BoardModels;
using ChessnutBoardEmulator.Common;
using EasyLinkSDKCSharp;

namespace ChessnutBoardEmulator.ViewModels;

public class EasyLinkViewModel : BaseViewModel
{
    private readonly ChessnutAirAPI _api = new ChessnutAirAPI();
    private readonly BoardViewModel _boardViewModel;

    private bool _isConnected;
    public bool IsConnected
    {
        get => _isConnected;
        set => SetField(ref _isConnected, value);
    }

    private bool _isWhiteTurn;

    public bool IsWhiteTurn
    {
        get => _isWhiteTurn;
        set => SetField(ref _isWhiteTurn, value);
    }

    private bool _isRunning;

    public bool IsRunning
    {
        get => _isRunning;
        set => SetField(ref _isRunning, value);
    }

    private string _lastKnownFen = "8/8/8/8/8/8/8/8";

    public string LastKnownFen
    {
        get => _lastKnownFen;
        set => SetField(ref _lastKnownFen, value);
    }
    
    public EasyLinkViewModel(BoardViewModel boardViewModel)
    {
        _boardViewModel = boardViewModel;
        
        /*_boardViewModel.StartNewGameEvent += BoardViewModelOnStartNewGameEvent;
        _boardViewModel.ClearBoardEvent += BoardViewModelOnClearBoardEvent;*/
        
        StartMonitoringCommand = new RelayCommand(StartMonitoring);
    }

    private void BoardViewModelOnStartNewGameEvent()
    {
        if (!IsConnected)
            return;
        
        _api.SetLeds(new[]
        {
            "1111111",
            "1111111",
            "00000000",
            "00000000",
            "00000000",
            "00000000",
            "1111111",
            "1111111"
        });
        
        IsWhiteTurn = true;
        LastKnownFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
    }
    
    private void BoardViewModelOnClearBoardEvent()
    {
        if (!IsConnected)
            return;
        
        _api.SetLeds(new[]
        {
            "00000000",
            "00000000",
            "00000000",
            "00000000",
            "00000000",
            "00000000",
            "00000000",
            "00000000"
        });
    }

    public ICommand StartMonitoringCommand { get; }

    public void StartMonitoring()
    {
        Monitor();
        //new Thread(Monitor).Start();
    }

    private void Monitor()
    {
        IsConnected = _api.Connect();

        if (!_isConnected)
            return;
        
        _api.SetRealtimeCallback(RealtimeCallback);
        _api.SetRealtimeMode();
        IsRunning = true;
    }
    
    private void RealtimeCallback(string fen, int length)
    {
        if(LastKnownFen == fen)
                return;
            
        var ranks = fen.Split('/').Reverse().ToArray();

        for (int i = 0; i < ranks.Length; i++)
        {
            int fenBlanks = 0;
            var currentRank = i + 1;
            
            for (int j = 0; j < ranks[i].Length; j++)
            {
                var value = ranks[i][j];
                if (char.IsDigit(value))
                {
                    var num = (int)char.GetNumericValue(value);

                    for (int k = 0; k < num; k++)
                    {
                        _boardViewModel.Squares.FirstOrDefault(x => x.Rank == currentRank && x.File == j + fenBlanks + k + 1)
                            ?.AddPieceToSquare(null);
                    }

                    fenBlanks += num - 1;
                    continue;
                }

                switch (value)
                {
                    case 'P':
                    case 'p':
                        _boardViewModel.Squares.FirstOrDefault(x => x.Rank == currentRank && x.File == j + fenBlanks + 1)
                            ?.AddPieceToSquare(new Pawn(value == 'P'));
                        break;
                    case 'R':
                    case 'r':
                        _boardViewModel.Squares.FirstOrDefault(x => x.Rank == currentRank && x.File == j + fenBlanks + 1)
                            ?.AddPieceToSquare(new Rook(value == 'R'));
                        break;
                    case 'N':
                    case 'n':
                        _boardViewModel.Squares.FirstOrDefault(x => x.Rank == currentRank && x.File == j + fenBlanks + 1)
                            ?.AddPieceToSquare(new Knight(value == 'N'));
                        break;
                    case 'B':
                    case 'b':
                        _boardViewModel.Squares.FirstOrDefault(x => x.Rank == currentRank && x.File == j + fenBlanks + 1)
                            ?.AddPieceToSquare(new Bishop(value == 'B'));
                        break;
                    case 'Q':
                    case 'q':
                        _boardViewModel.Squares.FirstOrDefault(x => x.Rank == currentRank && x.File == j + fenBlanks + 1)
                            ?.AddPieceToSquare(new Queen(value == 'Q'));
                        break;
                    case 'K':
                    case 'k':
                        _boardViewModel.Squares.FirstOrDefault(x => x.Rank == currentRank && x.File == j + fenBlanks + 1)
                            ?.AddPieceToSquare(new King(value == 'K'));
                        break;
                }
            }
        }

        LastKnownFen = fen;
    }
}