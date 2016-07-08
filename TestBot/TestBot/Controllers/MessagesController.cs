﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;

namespace TestBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        /// 

        [Serializable]
        public class EchoDialog : IDialog<object>
        {
            public async Task StartAsync(IDialogContext context)
            {
                context.Wait(MessageReceivedAsync);
            }
            public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<Message> argument)
            {
                var message = await argument;
                await context.PostAsync("You said: " + message.Text);
                context.Wait(MessageReceivedAsync);
            }
        }


        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                return await Conversation.SendAsync(message, () => new EchoDialog());
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}