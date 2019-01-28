using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalBot.DialogManager;

namespace MedicalBot.InputProcessingEngine
{
    interface ISession
    {
        void AddUser(CUser user);
        CUser GetUser(Int64 id);
    }
}
