using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Builder.Luis;
using NewsBot.Constants;

namespace NewsBot.Dialogs
{
    [Serializable]
    public class GreetDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait<LuisResult>(this.GreetProcessAsync);

        }

        private async Task GreetProcessAsync(IDialogContext context, IAwaitable<LuisResult> luisResult)
        {
            var result = await luisResult;
            EntityRecommendation _greet;


            if (result.TryFindEntity(luisEntities.greet.ToString(), out _greet))
            {


                //await context.PostAsync("Hi There! I can Help you with Stock Prices of NASDAQ and NYSE listed companies,latest News and Word meanings from Oxford.Type **help** anytime for more information.");
                await context.PostAsync("How can I help You?");
                context.Done(true);
            }
            else
            {
                //await context.Forward(new NoneDialog(), this.StockAfterAsync, result, System.Threading.CancellationToken.None);
                //message to send when entity not found
                //await context.PostAsync("This part not implemented");
                await context.PostAsync("Sorry,I didn't get that.");
                context.Done(true);
            }
        }
    }
}