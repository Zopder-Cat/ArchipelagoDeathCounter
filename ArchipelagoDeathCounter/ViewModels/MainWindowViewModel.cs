using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;



namespace ArchipelagoDeathCounter.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public string Greeting { get; set; } = "Death Counter for Archipelago";
        
        [ObservableProperty]
        private string _buttonText = "Connect";
        [ObservableProperty]
        private ViewModelBase _currentPage;

        public MainWindowViewModel()
        {
            _currentPage = new ConnectionViewModel(vm => CurrentPage = vm);
        }

        [RelayCommand]
        public void ButtonClick()
        {
            if (CurrentPage.executePrimaryAction())
            {

                CurrentPage = CurrentPage.getNextPage();
                ButtonText = CurrentPage.getActionName();
            }  
        }      
    }
}
