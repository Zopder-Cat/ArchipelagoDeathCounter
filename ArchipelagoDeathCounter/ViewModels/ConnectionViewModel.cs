using ArchipelagoDeathCounter.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ArchipelagoDeathCounter.ViewModels
{
    internal partial class ConnectionViewModel : ViewModelBase, IPrimaryAction
    {
        private Connector connector;
        private CountViewModel nextPage;

        private readonly Action<ViewModelBase>? _navigate;
        
        [ObservableProperty]
        private int ?_port;
        [ObservableProperty]
        private string _slotname;
        [ObservableProperty]
        private string _gamename;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _adress;

        public string Title { get; } = "Connect to Server";

        public ConnectionViewModel(Action<ViewModelBase>? navigate = null)
        {
          _navigate = navigate;
           this.nextPage = new CountViewModel(); 
        }
        public override bool executePrimaryAction()
        {
            connector = new Connector(Port ?? default(int), Gamename, Slotname, Password);
            connector.DeathLinkReceived += nextPage.getCounter().ReceiveDeath;
            if (connector.Connect())
            {
                return true;
            }
            return false;
        }

        public override string getActionName()
        {
            return "Connect";
        }
        public override ViewModelBase getNextPage()
        {
            return nextPage; 
        }

   
    }
}
