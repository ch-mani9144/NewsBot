using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using NewsBot.Utilites;
using NewsBot.Utilites.NewsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NewsBot.Cards
{
    public class Botcards
    {
        private List<Attachment> _attachmentList;

        public Botcards()
        {
            _attachmentList = new List<Attachment>();
        }

        public void AddCardAttachment(string title, string subtitle, List<string> text, string mediaUrl)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in text)
                {
                    sb.AppendLine($"{item}" + "  \n");
                }

                var attachment = new HeroCard()
                {
                    Title = title,
                    Subtitle = subtitle,
                    Text = sb.ToString()
                    //Media = new List<MediaUrl> { new MediaUrl() { Url = $"{mediaUrl}" } }
                }.ToAttachment();
                _attachmentList.Add(attachment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<Attachment> ReturnCardAttachment()
        {
            return _attachmentList;
        }


        public static Attachment CreateStockQuoteCard(IDialogContext context, EixQuoteDS result)
        {
            var reply = context.MakeMessage();

            //var attachment = new HeroCard()
            //{
            //    Title = result.companyName,
            //    Subtitle = $"Exchange : {result.primaryExchange}",
            //    Text = $"• Open : {result.open}" + "$" + $"  \n• Close : {result.close}" + "$" + $"  \n• High : {result.high}" + "$" + $"  \n• Low : {result.low} " + "$" + $"  \n• Latest Price : {result.latestPrice}" + "$" + $"  \n• Time : {result.latestTime}"
            //}.ToAttachment();



            var adpcard = new AdaptiveCard();
            adpcard.Body = new List<CardElement>
            {


                new TextBlock()
                {
                 Text=result.companyName,
                 Weight=TextWeight.Bolder,
                 Size=TextSize.Large,
                 HorizontalAlignment=HorizontalAlignment.Left,
                 MaxLines=2,
                 Wrap=true
                },
                new TextBlock()
                {
                 Text=$"Exchange : {result.primaryExchange}",
                 Weight=TextWeight.Lighter,
                 Size=TextSize.Large,
                 HorizontalAlignment=HorizontalAlignment.Left,
                 MaxLines=4,
                 Wrap=true,
                 IsSubtle=true


                },
                new TextBlock()
                {
                 Text=$"• Open : {result.open}" + "$" + $"  \n• Close : {result.close}" + "$" + $"  \n• High : {result.high}" + "$" + $"  \n• Low : {result.low} " + "$" + $"  \n• Latest Price : {result.latestPrice}" + "$" + $"  \n• Time : {result.latestTime}",
                 Weight=TextWeight.Lighter,
                 Size=TextSize.Large,
                 HorizontalAlignment=HorizontalAlignment.Left,
                 MaxLines=5,
                 Wrap=true,
                 IsSubtle=true


                }
            };

            Attachment attachment = new Attachment()
            {
                Content = adpcard,
                ContentType = AdaptiveCard.ContentType
            };
            //attachme.Add(adpattachment);




            return attachment;
        }





        public static IMessageActivity NewsCard(IDialogContext context, ApiResponse res)
        {
            int i = 0;
            var reply = context.MakeMessage();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = new List<Attachment>();



            if (res.Status == Statuses.Ok)
            {
                //List<string> titleList = new List<string>();
                //List<string> descriptionList = new List<string>();
                //List<string> urlList = new List<string>();
                //List<string> imageList = new List<string>();

                // here's the first 20
                if (res.Articles.Count() > 0)
                {
                    foreach (var article in res.Articles)
                    {

                        //// author
                        //Console.WriteLine(article.Author);
                        //titleList.Add(article.Title);
                        //descriptionList.Add(article.Description);
                        //urlList.Add(article.Url);
                        //imageList.Add(article.UrlToImage);



                        //var adpcard = new AdaptiveCard();
                        //adpcard.Body = new List<CardElement>
                        //{

                        //    new Image()
                        //    {
                        //       Url=article.UrlToImage,
                        //       Size=ImageSize.Stretch,
                        //       HorizontalAlignment=HorizontalAlignment.Center,

                        //       SelectAction= new OpenUrlAction()
                        //       {
                        //           Url=article.Url
                        //       }
                        //    },
                        //    new TextBlock()
                        //    {
                        //     Text=article.Title,
                        //     Weight=TextWeight.Bolder,
                        //     Size=TextSize.Medium,
                        //     HorizontalAlignment=HorizontalAlignment.Left,
                        //     MaxLines=2,
                        //     Wrap=true
                        //    },
                        //    new TextBlock()
                        //    {
                        //     Text=article.Description,
                        //     Weight=TextWeight.Lighter,
                        //     Size=TextSize.Normal,
                        //     HorizontalAlignment=HorizontalAlignment.Left,
                        //     MaxLines=4,
                        //     Wrap=true,
                        //     IsSubtle=true,
                        //     Speak=article.Title

                        //    }
                        //};

                        //Attachment adpattachment = new Attachment()
                        //{
                        //    Content = adpcard,
                        //    ContentType = AdaptiveCard.ContentType
                        //};
                        //reply.Attachments.Add(adpattachment);


                        List<CardImage> cardImages = new List<CardImage>();
                        cardImages.Add(new CardImage(url: article.UrlToImage));

                        var attachment = new HeroCard()
                        {
                            Title = article.Title,
                            Subtitle = article.Description,
                            Images = cardImages,
                            Tap = new CardAction(ActionTypes.OpenUrl, "View More", value: article.Url)

                        }.ToAttachment();
                        reply.Attachments.Add(attachment);
                        i++;


                    }
                }
                else
                {
                    reply.Text = "Sorry,I didn't find any news at this moment";
                }

                //        reply.Attachments.Add(attachment);
                //        i++;
                //    }

                //}
                //else
                //{
                //    reply.Text = "Sorry,I didn't find any news at this moment";

                //}
            }


            else
            {
                reply.Text = "Sorry,I didn't find any news at this moment";

            }
            return reply;


        }
    }
}