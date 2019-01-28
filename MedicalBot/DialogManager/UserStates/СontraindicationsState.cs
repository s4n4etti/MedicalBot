using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    class CСontraindicationsState : IUserState
    {
        private readonly CUser _user;
        private String _context;

        public Boolean SkipUserInput => false;
        public ReplyKeyboardMarkup AnswerKeyboard => null;

        public CСontraindicationsState(CUser user)
        {
            _user = user;
        }

        public string Answer()
        {
            throw new NotImplementedException();
        }

        public void UpdateContext(String context)
        {
            _context = context;
        }
    }
}
