using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    interface IUserState
    {
        String Answer();
        ReplyKeyboardMarkup AnswerKeyboard { get; }
        void UpdateContext(String context);
        Boolean SkipUserInput { get; }
    }
}
