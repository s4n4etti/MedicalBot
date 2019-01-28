using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalBot.DialogManager
{
    interface IBotClientManager
    {
        String Proxy { get; set; }
        Task Start();
        void Stop();
    }
}
