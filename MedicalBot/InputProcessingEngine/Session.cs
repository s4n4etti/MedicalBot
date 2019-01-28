using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalBot.DialogManager;

namespace MedicalBot.InputProcessingEngine
{
    class CSession : ISession
    {
        private readonly List<CUser> _users;

        public CSession()
        {
            _users = new List<CUser>();
        }

        public void AddUser(CUser user)
        {
            _users.Add(user);
        }

        public CUser GetUser(Int64 id)
        {
            return _users.FirstOrDefault(user => user.Id == id);
        }
    }
}
