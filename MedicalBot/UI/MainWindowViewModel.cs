using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MedicalBot.Common;
using MedicalBot.DialogManager;

namespace MedicalBot.UI
{
    enum EOnlineStatus
    {
        Offline = 1,
        Connecting = 2,
        Online = 3,
        Disconnecting = 4,
    }

    class CMainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IDialogManager _dialogManager;
        private String _selectedProxy;
        private EOnlineStatus _onlineStatus;

        public ActionCommand ChangeOnlineStatusCommand { get; private set; }

        public ObservableCollection<String> Proxies { get; private set; }

        public String StatusLabelText
        {
            get
            {
                switch (OnlineStatus)
                {
                    case EOnlineStatus.Online:
                        return "ONLINE";
                    case EOnlineStatus.Offline:
                        return "OFFLINE";
                    case EOnlineStatus.Connecting:
                        return "CONNECTING...";
                    case EOnlineStatus.Disconnecting:
                        return "DISCONNECTING...";
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public String ConnectButtonContent
        {
            get
            {
                switch (OnlineStatus)
                {
                    case EOnlineStatus.Online:
                        return "GO TO OFFLINE";
                    case EOnlineStatus.Offline:
                        return "GO TO ONLINE";
                    case EOnlineStatus.Connecting:
                        return "Connecting...";
                    case EOnlineStatus.Disconnecting:
                        return "Disconnecting...";
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public Boolean IsConnectButtonEnabled
        {
            get
            {
                switch (OnlineStatus)
                {
                    case EOnlineStatus.Online:
                    case EOnlineStatus.Offline:
                        return true;
                    case EOnlineStatus.Connecting:
                    case EOnlineStatus.Disconnecting:
                        return false;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        public String SelectedProxy
        {
            get { return _selectedProxy; }
            set
            {
                _selectedProxy = value;
                OnPropertyChanged();
            }
        }

        public EOnlineStatus OnlineStatus
        {
            get { return _onlineStatus; }
            set
            {
                _onlineStatus = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsConnectButtonEnabled));
                OnPropertyChanged(nameof(ConnectButtonContent));
                OnPropertyChanged(nameof(StatusLabelText));
            }
        }

        public CMainWindowViewModel(IDialogManager dialogManager)
        {
            _dialogManager = dialogManager;
            ChangeOnlineStatusCommand = new ActionCommand(ChangeOnlineStatus);
            Proxies = new ObservableCollection<string>
            {
                "109.105.51.18:53281",
                "185.81.98.9",
                "109.105.54.74:57657",
                "104.248.30.172:80",
                "103.243.81.234:8080",
                "108.61.162.183:1080",
                "109.163.195.226:34725",
                "185.65.160.98:31487",
                "104.244.72.171:57480",
                "104.248.82.81:3128",
                "109.106.137.56:30242",
                "109.167.113.9:52867",
                "179.43.174.146:3128"
            };
            SelectedProxy = Proxies[0];
            OnlineStatus = EOnlineStatus.Offline;
        }

        private void ChangeOnlineStatus()
        {
            switch (OnlineStatus)
            {
                case EOnlineStatus.Offline:                   
                    Connect();
                    break;
                case EOnlineStatus.Online :
                    Disconnect();
                    break;
                default:
                    throw new InvalidEnumArgumentException(@"Online status is changing now!");
            }
        }

        private async Task Connect()
        {
            try
            {
                OnlineStatus = EOnlineStatus.Connecting;               
                await _dialogManager.Connect(SelectedProxy);
                OnlineStatus = EOnlineStatus.Online;
            }
            catch (Exception e)
            {
                OnlineStatus = EOnlineStatus.Offline;
                MessageBox.Show(e.Message);
            }
        }

        private void Disconnect()
        {
            try
            {
                OnlineStatus = EOnlineStatus.Disconnecting;
                _dialogManager.Disconnect();
                OnlineStatus = EOnlineStatus.Offline;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
