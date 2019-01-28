using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    class CHypersensitivityDetectionState : IUserState
    {
        private readonly CUser _user;

        private String _context;
        public Boolean SkipUserInput => true;

        public ReplyKeyboardMarkup AnswerKeyboard => null;

        public void UpdateContext(String context)
        {
            _context = context;
        }

        public CHypersensitivityDetectionState(CUser user)
        {
            _user = user;
        }

        public string Answer()
        {
            if (_context.ToLower() != "нет")
            {
                _user.Contraindications = _user.Contraindications | Contraindications.Hypersensitivity;
            }

            _user.CurrentState = new CDrugSelectionState(_user);
            return "[TBD]Идёт подбор лекарства, пожалуйста подождите...";
        }
    }
}
