using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalBot.DialogManager
{
    interface IDialogManager
    {
        Task Connect(String proxy = null);
        void Disconnect();
        void StartMessageReceiving();
        void StopMessageReceiving();
    }
}
