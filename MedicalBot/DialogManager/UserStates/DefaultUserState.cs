using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    class CDefaultUserState : IUserState
    {
        private readonly CUser _user;
        private String _context;

        public Boolean SkipUserInput => false;

        public ReplyKeyboardMarkup AnswerKeyboard => new ReplyKeyboardMarkup(new List<KeyboardButton>
        {
            new KeyboardButton
            {
                Text = "Боль в ушах"
            }
        }, oneTimeKeyboard: true);

        public void UpdateContext(String context)
        {
            _context = context;
        }

        public CDefaultUserState(CUser user)
        {
            _user = user;
        }

        public string Answer()
        {
            _user.ResetState();
            _user.CurrentState = new CDeseasedBodyPartDetectionState(_user);
            return "[TBD]Здравствуйте, вас приветсвует Ваш персональный медецинский помошник, что Вас беспокоит?";
        }
    }
}
