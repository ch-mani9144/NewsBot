using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NewsBot.Dialogs
{
    [Serializable]
    public class HelpDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait<LuisResult>(this.HelpProcessAsync);
        }

        private async Task HelpProcessAsync(IDialogContext context, IAwaitable<LuisResult> luisResult)
        {
            await context.PostAsync("Hi ! Right now I can help you with news.\nYou can try queries like :  \n• Todays Headlines  \n• Sports Headlines  \n• USA business news");
            context.Done(true);
        }

        /*public async Task StockAfterAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(true);
        }*/

    }
}