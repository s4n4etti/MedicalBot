using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalBot.InputProcessingEngine;

namespace MedicalBot.DialogManager
{
    class CDialogManager : IDialogManager
    {
        private readonly IBotClientManager _botClient;
        public CDialogManager(IBotClientManager botClient)
        {
            _botClient = botClient;
        }

        public async Task Connect(String proxy = null)
        {
            _botClient.Proxy = proxy;
            await _botClient.Start();
        }

        public void Disconnect()
        {
            _botClient.Stop();
        }

        public void StartMessageReceiving()
        {
            _botClient.Start();
        }

        public void StopMessageReceiving()
        {
            _botClient.Stop();
        }
    }
}
