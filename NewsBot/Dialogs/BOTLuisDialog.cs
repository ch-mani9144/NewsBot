using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace NewsBot.Dialogs
{
    [Serializable]
    [LuisModel("654dc938-21a1-42f4-a894-7efbff51a966", "1e68e2b1b56b48728528ed7ad92e42cd", LuisApiVersion.V2, "westus.api.cognitive.microsoft.com")]
    public class BOTLuisDialog : LuisDialog<object>
    {
        [LuisIntent("Greetings")]
        public async Task GreetingAsync(IDialogContext context, LuisResult result)
        {
            try
            {
                await context.Forward(new GreetDialog(), OnCompeletionAsync, result, System.Threading.CancellationToken.None);
            }
            catch (Exception e)
            {
                await context.PostAsync("Error Occured . " + e.Message);
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("NewsSearch")]
        public async Task NewsSearchAsync(IDialogContext context, LuisResult result)
        {
            try
            {

                await context.Forward(new NewsDialog(), OnCompeletionAsync, result, System.Threading.CancellationToken.None);
            }
            catch (Exception e)
            {
                await context.PostAsync("Error Occured . " + e.Message);
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("Help")]
        public async Task HelpAsync(IDialogContext context, LuisResult result)
        {
            try
            {
                await context.Forward(new HelpDialog(), OnCompeletionAsync, result, System.Threading.CancellationToken.None);
            }
            catch (Exception e)
            {
                await context.PostAsync("Error Occured . " + e.Message);
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("StockPrice")]
        public async Task StockPricesAsync(IDialogContext context, LuisResult result)
        {
            try
            {
                //await context.Forward(new StockDialog(), OnCompeletionAsync, result, System.Threading.CancellationToken.None);
                await context.PostAsync("How can I help You?");
            }
            catch (Exception e)
            {
                await context.PostAsync("Error Occured . " + e.Message);
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("DictonarySearch")]
        public async Task DictonarySearchAsync(IDialogContext context, LuisResult result)
        {
            //IMessageActivity msg = await message as IMessageActivity;
            //var cnt =  msg.Entities.Count;
            //var reply=context.MakeMessage();
            //reply.Text = cnt.ToString();
            //await context.PostAsync(reply);


            try
            {

                //await context.Forward(new DictonaryDialog(), OnCompeletionAsync, result, System.Threading.CancellationToken.None);
                await context.PostAsync("How can I help You?");
            }
            catch (Exception e)
            {
                await context.PostAsync("Error Occured . " + e.Message);
                context.Wait(MessageReceived);
            }
        }

        [LuisIntent("None")]
        public async Task NoneAsync(IDialogContext context, LuisResult result)
        {
            await context.Forward(new NoneDialog(), OnCompeletionAsync, result, System.Threading.CancellationToken.None);
        }

        private async Task OnCompeletionAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
    }
}