using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    class CEarsOperationInformationState : IUserState
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
            },

            new KeyboardButton
            {
                Text = "Не знаю"
            }
        }, oneTimeKeyboard: true);

        public void UpdateContext(String context)
        {
            _context = context;
        }

        public CEarsOperationInformationState(CUser user)
        {
            _user = user;
        }

        public string Answer()
        {
            if (_context.ToLower() == "да")
            {
                _user.Contraindications = _user.Contraindications | Contraindications.TympanicMembranePerforation;
            }

            _user.CurrentState = new CHypersensitivityDetectionState(_user);
            return "[TBD]Страдаете ли Вы гиперчувствительностью?";
        }
    }
}
