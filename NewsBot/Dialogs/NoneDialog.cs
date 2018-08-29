using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis.Models;

namespace NewsBot.Dialogs
{
    [Serializable]
    public class NoneDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait<LuisResult>(this.NoneDialogProcessAsync);
        }

        public async Task NoneDialogProcessAsync(IDialogContext context, IAwaitable<LuisResult> luisResult)
        {
            await context.PostAsync("Sometimes, I may not have the information you need.\n Right Now I can only help with News. :)");
            context.Done(true);
        }
    }
}