using Microsoft.Bot.Builder.Dialogs;
using NewsBot.Cards;
using NewsBot.Utilites.NewsApi;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NewsBot.Utilites
{
    [Serializable]
    public class NewsApiManager
    {


        public async Task GetNews(IDialogContext context, string newscategory, string newscountry, string que)
        {

            var reply = context.MakeMessage();

            var cat = "";
            var lan = "en";
            var query = "";
            var country = "";
            if (string.IsNullOrEmpty(que) == false)
            {
                query = que;
            }

            if (string.IsNullOrEmpty(newscategory) == false)
            {
                cat = newscategory;
            }
            if (string.IsNullOrEmpty(newscountry) == false)
            {
                if (newscountry == "us" || newscountry == "usa")
                {
                    country = "us";
                }
                else if (newscountry == "india")
                {
                    country = "in";
                }
                else if (newscountry == "australia")
                {
                    country = "au";
                }
                else if (newscountry == "china")
                {
                    country = "cn";
                }
                else if (newscountry == "canada")
                {
                    country = "ca";
                }
                else if (newscountry == "japan")
                {
                    country = "jp";
                }
                else if (newscountry == "germany")
                {
                    country = "de";
                    lan = "de";
                }
                else if (newscountry == "southafrica" || newscountry == "south africa")
                {
                    country = "za";
                }


            }


            var client = new RestClient($"https://newsapi.org/v2/top-headlines?country={country}&apiKey=02dea25aabb64d3ea51c0cb3226646e2&category={cat}&language={lan}&q={query}");

            var request = new RestRequest(Method.GET);

            var res = client.Execute(request);
            var articlesResponse = JsonConvert.DeserializeObject<ApiResponse>(res.Content);

            reply = Botcards.NewsCard(context, articlesResponse);
            await context.PostAsync(reply);


            context.Done(true);


        }
        public async Task QueryNews(IDialogContext context, string que)
        {
            var reply = context.MakeMessage();

            var lan = "en";
            var query = "";
            //var country = "in";
            if (string.IsNullOrEmpty(que) == false)
            {
                query = que;
            }

            string nDaysAgo = DateTime.Today.AddDays(-2).ToString("yyyy/MM/dd").Replace('/', '-');
            string today = DateTime.Today.ToString("yyyy/MM/dd").Replace('/', '-');



            var client = new RestClient($"https://newsapi.org/v2/everything?apiKey=02dea25aabb64d3ea51c0cb3226646e2&language={lan}&q={query}&from={nDaysAgo}&to={today}");

            var request = new RestRequest(Method.GET);
            var res = client.Execute(request);
            var articlesResponse = JsonConvert.DeserializeObject<ApiResponse>(res.Content);

            reply = Botcards.NewsCard(context, articlesResponse);
            await context.PostAsync(reply);


            context.Done(true);

        }
    }
}