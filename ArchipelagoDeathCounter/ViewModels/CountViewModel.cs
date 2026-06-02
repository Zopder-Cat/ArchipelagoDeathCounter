using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ArchipelagoDeathCounter.ViewModels
{
    internal partial class CountViewModel : ViewModelBase, IPrimaryAction
    {
        private readonly Action<ViewModelBase>? _navigate;
        private Counter counter;
  
        [ObservableProperty]
        //private Dictionary<string, int> _deaths = new Dictionary<string, int>();
        private string _deaths = "No deaths so far";

        public CountViewModel(Action<ViewModelBase>? navigate = null)
        {
            _navigate = navigate;
            counter = new Counter();
            counter.DeathsUpdated += _ => this.updateDeathText();
        }
         public override bool executePrimaryAction()
         {
            return true;
         }

        public override string getActionName()
        {
            return "Cancel";
        }
        public override ViewModelBase getNextPage()
        {
            return new ConnectionViewModel();
        }

        public Counter getCounter()
        {
            return this.counter;
        }
        public void updateDeathText()
        {
            Deaths = counter.getDeathsFormatted();
        }

    }
}
