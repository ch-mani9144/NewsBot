using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using NewsBot.Dialogs;
using NewsBot.Utilites;

namespace NewsBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        [ResponseType(typeof(void))]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.GetActivityType() == ActivityTypes.Message)
            {
                LocationClass.EntitiesFromActivity = activity.Entities;
                if (activity.GetMentions().Count() > 0)
                {
                    Mention[] m = activity.GetMentions();
                    var messageText = activity.Text;

                    for (int i = 0; i < m.Length; i++)
                    {
                        if (m[i].Mentioned.Id == activity.Recipient.Id)
                        {
                            if (m[i].Text != null)
                                activity.Text = messageText.Replace(m[i].Text, "");
                            await Conversation.SendAsync(activity, () => new BOTLuisDialog());
                        }
                    }
                }
                else
                {
                    await Conversation.SendAsync(activity, () => new BOTLuisDialog());
                }

            }
            else
            {
                await HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task<Activity> HandleSystemMessage(Activity message)
        {
            string messageType = message.GetActivityType();
            if (messageType == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (messageType == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
                var client = new ConnectorClient(new Uri(message.ServiceUrl));

                if (message.MembersAdded.Any())
                {
                    foreach (var members in message.MembersAdded)
                    {
                        var reply = message.CreateReply();

                        if (members.Id == message.Recipient.Id)
                        {
                            reply.Text = $"Hi There! I can Help you with latest News.\nType **help** anytime for more information.";

                        }
                        else
                        {
                            break;
                        }

                        await client.Conversations.ReplyToActivityAsync(reply);
                    }
                }
            }
            else if (messageType == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (messageType == ActivityTypes.Typing)
            {
                // Handle knowing that the user is typing
            }
            else if (messageType == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}