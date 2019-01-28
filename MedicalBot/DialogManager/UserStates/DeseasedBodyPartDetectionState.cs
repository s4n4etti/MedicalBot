using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    static class BodyParts
    {
        public static String Ears = "Боль в ушах";
    }

    class CDeseasedBodyPartDetectionState : IUserState
    {
        private CUser _user;
        private String _context;

        public Boolean SkipUserInput => false;
        public ReplyKeyboardMarkup AnswerKeyboard => null;

        public CDeseasedBodyPartDetectionState(CUser user)
        {
            _user = user;
        }
        public String Answer()
        {
            _user.CurrentState = new CSymptomsDiagnosisState(_user);
            return "[TBD]Какие симптомы проявляются?";
        }

        public void UpdateContext(String context)
        {
            _context = context;
        }
    }
}
