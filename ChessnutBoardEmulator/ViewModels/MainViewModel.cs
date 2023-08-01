namespace ChessnutBoardEmulator.ViewModels;

public class MainViewModel : BaseViewModel
{
    private BaseViewModel? _currentView;
    public BaseViewModel? CurrentView
    {
        get => _currentView;
        set => SetField(ref _currentView, value);
    }

    private EasyLinkViewModel? _easyLinkViewModel;

    public EasyLinkViewModel? EasyLinkViewModel
    {
        get => _easyLinkViewModel;
        set => SetField(ref _easyLinkViewModel, value);
    }

    public MainViewModel()
    {
        var boardViewModel = new BoardViewModel();
        CurrentView = boardViewModel;
        EasyLinkViewModel = new EasyLinkViewModel(boardViewModel);
        EasyLinkViewModel.StartMonitoring();
    }
}