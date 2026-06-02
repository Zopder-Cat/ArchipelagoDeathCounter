using CommunityToolkit.Mvvm.ComponentModel;

namespace ArchipelagoDeathCounter.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, IPrimaryAction
    {
        private ViewModelBase _nextPage;
        public abstract bool executePrimaryAction();
        public abstract string getActionName();
        public abstract ViewModelBase getNextPage();
    }
}
