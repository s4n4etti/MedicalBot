using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    class CAgeSelectionState : IUserState
    {
        private readonly CUser _user;

        private String _context;
        public Boolean SkipUserInput => false;

        public ReplyKeyboardMarkup AnswerKeyboard => new ReplyKeyboardMarkup(new List<KeyboardButton>
        {
            new KeyboardButton
            {
                Text = "Да"
            },

            new KeyboardButton
            {
                Text = "Нет"
            }
        }, oneTimeKeyboard: true);

        public void UpdateContext(String context)
        {
            _context = context;
        }

        public CAgeSelectionState(CUser user)
        {
            _user = user;
        }

        public string Answer()
        {
            // TODO : handle wrong age input
            Int32 userAge = Int32.Parse(_context);

            if (userAge <= 1)
            {
                _user.Contraindications = _user.Contraindications | Contraindications.OneYearsOld;
            }

            _user.CurrentState = new CEarsOperationInformationState(_user);
            return "[TBD] У Вас были операции на ушах или других частях ухо-горло-носа?";
        }
    }
}
