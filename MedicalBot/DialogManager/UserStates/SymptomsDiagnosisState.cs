using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    static class SymptomsTraits
    {
        public static List<String> Sneeze = new List<String> { "чихать", "чих", "чиханье", "чихота", "чох" };
        public static List<String> Rhinitis = new List<String> { "насморк", "сопли", "возгря" };
        public static List<String> EarsPain = new List<String> { "стреляет ухо", "трещит ухо", "болит ухо", "закладывает ухо" };
        public static List<String> Temperature = new List<String> { "высокая температура", "повышенная температура", "жар" };
        public static List<String> Pus = new List<String> { "гной в ухе", "ухо гноит", "жидкость в ухе", "выделения из уха" };
        public static List<String> Inflamination = new List<String> { "воспаление", "болезнь" };
    }

    class CSymptomsDiagnosisState : IUserState
    {
        private readonly CUser _user;
        private String[] _context;

        public Boolean SkipUserInput { get; private set; }

        public ReplyKeyboardMarkup AnswerKeyboard { get; private set; }

        public CSymptomsDiagnosisState(CUser user)
        {
            SkipUserInput = true;
            AnswerKeyboard = null;
            _user = user;
        }

        public String Answer()
        {
            if (_user.Symptoms == Symptoms.None)
            {
                SkipUserInput = false;
                _user.CurrentState = new CDefaultUserState(_user);
                return "[TBD] К сожалению, на основании описанных Вами симптомов, не удалось определить заболевание. Рекомендуем обратиться в ближайшую клиннику.";
            }
            _user.CurrentState = new CGenderSelectionState(_user);
            return "[TBD]На основании указанных Вами симптомов можно сделать вывод, что у Вас отит. Для правильного подбора лекартсва необходимо уточнить, имеются ли у Вас противопоказания к применению тех или иных препаратов. Для этого, пожалуйста ответьте на несколько вопросов.";               
        }

        public void UpdateContext(String context)
        {
            _context = context.Split();
            for(Int32 i = 0; i < _context.Length; i++)
            {
                _context[i] = _context[i].ToLower();
                _context[i] = _context[i].Trim(' ', ',');
            }
            ProccessContext();
            PrepareKeyboard();
        }

        private void PrepareKeyboard()
        {
            if (_user.Symptoms == Symptoms.None)
            {
                AnswerKeyboard = new ReplyKeyboardMarkup(new List<KeyboardButton>
                {
                    new KeyboardButton
                    {
                        Text = "[TBD]В начало"
                    }
                }, oneTimeKeyboard: true);              
            }
        }

        private void ProccessContext()
        {
            if ((_context.Intersect(SymptomsTraits.Sneeze)).Any())
            {
                _user.Symptoms = _user.Symptoms | Symptoms.Sneeze;
            }
            if ((_context.Intersect(SymptomsTraits.EarsPain)).Any())
            {
                _user.Symptoms = _user.Symptoms | Symptoms.EarsPain;
            }
            if ((_context.Intersect(SymptomsTraits.Rhinitis)).Any())
            {
                _user.Symptoms = _user.Symptoms | Symptoms.Rhinitis;
            }
            if ((_context.Intersect(SymptomsTraits.Temperature)).Any())
            {
                _user.Symptoms = _user.Symptoms | Symptoms.Temperature;
            }
            if ((_context.Intersect(SymptomsTraits.Pus)).Any())
            {
                _user.Symptoms = _user.Symptoms | Symptoms.Pus;
            }
            if ((_context.Intersect(SymptomsTraits.Inflamination)).Any())
            {
                _user.Symptoms = _user.Symptoms | Symptoms.Inflammation;
            }
        }
    }
}
