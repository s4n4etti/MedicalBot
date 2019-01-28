using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using MedicalBot.InputProcessingEngine;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace MedicalBot.DialogManager
{
    class CBotClientManager : IBotClientManager
    {
        private readonly ISession _session;
        private TelegramBotClient _bot;

        public String Proxy { get; set; }

        public CBotClientManager(ISession session)
        {
            _session = session;
        }

        Task IBotClientManager.Start()
        {
            return String.IsNullOrEmpty(Proxy) ? Start() : Start(Proxy);
        }

        private async Task Start(String proxy)
        {
            WebProxy wp = new WebProxy(proxy, true);
            String token = ConfigurationManager.AppSettings["TelegramApiToken"];
            _bot = new TelegramBotClient(token, wp);
            await _bot.SetWebhookAsync(String.Empty);
            _bot.OnUpdate += BotOnUpdate;
            _bot.StartReceiving();
        }

        private async void BotOnUpdate(object sender, UpdateEventArgs e)
        {
            if (e.Update.CallbackQuery != null || e.Update.InlineQuery != null)
                return; // inline and callback queries are handling separately

            Update update = e.Update;
            Message message = update.Message;
            if (message == null)
                return;

            switch (message.Type)
            {
                case MessageType.Text:
                    CInputProcessingEngine.Instance.ProccessMessage(_session, message);
                    CUser currentUser = _session.GetUser(message.Chat.Id);
                    Boolean skipUserInput = false;
                    do
                    {
                        String answer;
                        ReplyKeyboardMarkup keyboard;
                        skipUserInput = currentUser.CurrentState.SkipUserInput;
                        currentUser.GenerateAnswer(out answer, out keyboard);
                        if (answer == null)
                        {
                            await _bot.LeaveChatAsync(message.Chat.Id);
                            return;
                        }
                        if (keyboard != null)
                        {
                            await _bot.SendTextMessageAsync(currentUser.Id, answer, ParseMode.Default, false, false, 0, keyboard);
                        }
                        else
                        {
                            await _bot.SendTextMessageAsync(currentUser.Id, answer);
                        }
                    } while (skipUserInput);

                    break;
                default: 
                    throw new NotSupportedException();
            }
        }

        private async Task Start()
        {
            try
            {
                String token = ConfigurationManager.AppSettings["TelegramApiToken"];
                _bot = new TelegramBotClient(token);
                await _bot.SetWebhookAsync(String.Empty);
                _bot.OnUpdate += BotOnUpdate;
                _bot.StartReceiving();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Stop()
        {
            try
            {
                _bot.StopReceiving();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
