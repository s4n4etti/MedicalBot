using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalBot.DialogManager;
using Telegram.Bot.Types;

namespace MedicalBot.InputProcessingEngine
{
    class CInputProcessingEngine
    {
        private static CInputProcessingEngine _instance;

        public static CInputProcessingEngine Instance => _instance ?? (_instance = new CInputProcessingEngine());

        public void ProccessMessage(ISession session, Message message)
        {
            if (message.Text.Contains("/"))
            {
                ProccessSpecialCommand(session, message);
                return;
            }

            CUser user = session.GetUser(message.Chat.Id);
            user.CurrentState.UpdateContext(message.Text);
        }

        private void ProccessSpecialCommand(ISession session, Message message)
        {
            switch (message.Text)
            {
                case "/start":
                    session.AddUser(new CUser(message.Chat.Id));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
