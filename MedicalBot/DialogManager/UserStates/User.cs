using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    [Flags]
    enum Symptoms
    {
        None = 0,
        Sneeze = 1,
        Rhinitis = 2,
        EarsPain = 4,
        Temperature = 8,
        Pus = 16,
        Inflammation = 32
    }

    [Flags]
    enum Contraindications
    {
        None = 0,
        Pregnancy = 1,
        OneYearsOld = 2,
        TympanicMembranePerforation = 4,
        Lactation = 8,
        Hypersensitivity = 16
    }

    class CUser
    {
        public Int64 Id { get; private set; }
        public IUserState CurrentState { get; set; }
        public Symptoms Symptoms { get; set; }
        public Contraindications Contraindications { get; set; }
        public CUser(Int64 id)
        {
            Id = id;
            CurrentState = new CDefaultUserState(this);
            Symptoms = Symptoms.None;
            Contraindications = Contraindications.None;
        }

        public void GenerateAnswer(out String answer, out ReplyKeyboardMarkup keyboardMarkup)
        {
            keyboardMarkup = CurrentState.AnswerKeyboard;
            answer = CurrentState.Answer();
        }

        public void ResetState()
        {
            Contraindications = Contraindications.None;
            Symptoms = Symptoms.None;
        }
    }
}
