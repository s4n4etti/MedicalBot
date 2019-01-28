using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalBot.DialogManager;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    class CGenderSelectionState : IUserState
    {
        private readonly CUser _user;
        private String _context;

        public Boolean SkipUserInput => false;

        public ReplyKeyboardMarkup AnswerKeyboard { get; private set; }

        public CGenderSelectionState(CUser user)
        {
            _user = user;
            AnswerKeyboard = new ReplyKeyboardMarkup(new List<KeyboardButton>
            {
                new KeyboardButton
                {
                    Text = "М"
                },
                new KeyboardButton
                {
                    Text = "Ж"
                }
            }, oneTimeKeyboard: true);
        }

        public string Answer()
        {
            if (_context == "М")
            {
                _user.CurrentState = new CAgeSelectionState(_user);
                return "Укажите Ваш возраст:";
            }

            if (_context == "Ж")
            {
                // TODO : implement
                throw new NotImplementedException();
            }
            return "Укажите Ваш пол:";
        }

        public void UpdateContext(String context)
        {
            _context = context;
            if (_context == "М")
            {
                AnswerKeyboard = null;
            }
        }
    }
}
