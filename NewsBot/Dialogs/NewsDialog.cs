using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using NewsBot.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NewsBot.Dialogs
{
    [Serializable]
    public class NewsDialog : IDialog<object>
    {
        NewsApiManager newsapi = new NewsApiManager();

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait<LuisResult>(this.NewsApiProcessAsync);
        }

        public async Task NewsApiProcessAsync(IDialogContext context, IAwaitable<LuisResult> luisResult)
        {

            var result = await luisResult;
            EntityRecommendation _category;
            EntityRecommendation _country;
            EntityRecommendation _newsquery;
            EntityRecommendation _tickersymbol;
            string cat = string.Empty;
            string coun = string.Empty;
            string query = string.Empty;

            var category = result.TryFindEntity("NewsCategory", out _category);
            var country = result.TryFindEntity("NewsCountry", out _country);
            var newsquery = result.TryFindEntity("NewsQuery", out _newsquery);
            var tickersymbol = result.TryFindEntity("TickerSymbol", out _tickersymbol);


            if (category && country)
            {
                cat = _category.Entity;
                coun = _country.Entity;
                await newsapi.GetNews(context, cat, coun, "");
                context.Done(true);
            }

            else if (category && !country)
            {
                cat = _category.Entity;
                await newsapi.GetNews(context, cat, coun, "");
                context.Done(true);
            }
            else if (newsquery && country)
            {
                coun = _country.Entity;

                query = _newsquery.Entity;

                await newsapi.GetNews(context, "", coun, query);
                context.Done(true);
            }
            else if (tickersymbol && country)
            {
                coun = _country.Entity;

                query = _tickersymbol.Entity;

                await newsapi.GetNews(context, "", coun, query);
                context.Done(true);
            }
            else if (newsquery && !country)
            {

                query = _newsquery.Entity;


                await newsapi.GetNews(context, "", coun, query);
                context.Done(true);
            }
            else if (tickersymbol && !country)
            {
                query = _tickersymbol.Entity;


                await newsapi.GetNews(context, "", coun, query);
                context.Done(true);
            }
            else if (!category && country)
            {


                coun = _country.Entity;
                await newsapi.GetNews(context, cat, coun, "");
                context.Done(true);
            }
            else
            {
                //if (activity.Entities != null)
                //{
                //    var userInfo = activity.Entities.FirstOrDefault(e => e.Type.Equals("UserInfo"));
                //    if (userInfo != null)
                //    {
                //        var email = userInfo.Properties.Value<string>("UserEmail");

                //        if (!string.IsNullOrEmpty(email))
                //        {
                //            //Do something with the user's email address.
                //        }

                //        var currentLocation = userInfo.Properties["CurrentLocation"];

                //        if (currentLocation != null)
                //        {
                //            var hub = currentLocation["Hub"];

                //            //Access the latitude and longitude values of the user's location.
                //            var lat = hub.Value<double>("Latitude");
                //            var lon = hub.Value<double>("Longitude");

                //            //Do something with the user's location information.
                //        }
                //    }
                //}

                PromptDialog.Text(context, this.GetAllNewsAsync, "Hey! What News do you want?\nType a KEYWORD for topic specific news or type **ALL** for general news .", "Retry", 3);
            }




        }

        public async Task GetAllNewsAsync(IDialogContext context, IAwaitable<string> result)
        {
            var message = await result;

            if (message.Trim().Equals("all", StringComparison.InvariantCultureIgnoreCase))
            {
                await newsapi.GetNews(context, "", "", "");
            }
            else
            {
                await context.PostAsync("Hold On!, contacting my servers...");
                await newsapi.QueryNews(context, message.Trim());
            }
            context.Done(true);
        }
    }
}