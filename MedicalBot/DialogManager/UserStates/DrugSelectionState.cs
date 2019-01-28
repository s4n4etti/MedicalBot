using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalBot.Common;
using Telegram.Bot.Types.ReplyMarkups;

namespace MedicalBot.DialogManager
{
    class CDrugSelectionState : IUserState
    {
        private readonly CUser _user;
        private String _context;

        public Boolean SkipUserInput => false;
        public ReplyKeyboardMarkup AnswerKeyboard => new ReplyKeyboardMarkup(new List<KeyboardButton>
        {
            new KeyboardButton
            {
                Text = "[TBD]В начало"
            }
        }, oneTimeKeyboard: true);

        public CDrugSelectionState(CUser user)
        {
            _user = user;
        }

        public string Answer()
        {
            IEnumerable<Drug> suitableDrugs = Drugs.All.Where(drug => (drug.Contraindications & _user.Contraindications) == Contraindications.None);
            _user.CurrentState = new CDefaultUserState(_user);
            if (!suitableDrugs.Any())
            {
                return "[TBD]К сожалению на основании указанных Вами симптомов и противопоказаний нам не удалось подобрать для Вас лекарства. Рекомендуем обратиться в ближайшую клиннику.";
            }

            StringBuilder drugNames = new StringBuilder();
            foreach (var drug in suitableDrugs)
            {
                drugNames.AppendFormat("{0}, ", drug.Name);
            }

            drugNames.Remove(drugNames.Length - 2, 2);
            drugNames.Append('.');
            return $"[TBD]На основании указанных вами симптомов и противопоказаний были подобраны следующие лекарства для лечения отита: {drugNames}";
        }

        public void UpdateContext(String context)
        {
            _context = context;
        }
    }
}
