using NewsAPI.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsBot.Utilites.NewsApi
{
    [Serializable]
    public class ApiResponse
    {
        public Statuses Status { get; set; }
        public ErrorCodes? Code { get; set; }
        public string Message { get; set; }
        public List<Article> Articles { get; set; }
        public int TotalResults { get; set; }
    }

    [Serializable]
    public class Article
    {
        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
    }

    [Serializable]
    public class ArticlesResult
    {
        public Statuses Status { get; set; }
        public Error Error { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }
    }

    [Serializable]
    public class Error
    {
        public ErrorCodes Code { get; set; }
        public string Message { get; set; }
    }
    [Serializable]
    public class EverythingRequest
    {
        /// <summary>
        /// The keyword or phrase to search for. Boolean operators are supported.
        /// </summary>
        public string Q { get; set; }
        /// <summary>
        /// If you want to restrict the search to specific sources, add their Ids here. You can find source Ids with the /sources endpoint or on newsapi.org.
        /// </summary>
        public List<string> Sources = new List<string>();
        /// <summary>
        /// If you want to restrict the search to specific web domains, add these here. Example: nytimes.com.
        /// </summary>
        public List<string> Domains = new List<string>();
        /// <summary>
        /// The earliest date to retrieve articles from. Note that how far back you can go is constrained by your plan type. See newsapi.org/pricing for plan details.
        /// </summary>
        public DateTime? From { get; set; }
        /// <summary>
        /// The latest date to retrieve articles from.
        /// </summary>
        public DateTime? To { get; set; }
        /// <summary>
        /// The language to restrict articles to.
        /// </summary>
        public Languages? Language { get; set; }
        /// <summary>
        /// How should the results be sorted? Relevancy = articles relevant to the Q param come first. PublishedAt = most recent articles come first. Publisher = popular publishers come first.
        /// </summary>
        public SortBys? SortBy { get; set; }
        /// <summary>
        /// Each request returns a fixed amount of results. Page through them by increasing this.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Set the max number of results to retrieve per request. The max is 100.
        /// </summary>
        public int PageSize { get; set; }
    }
    [Serializable]
    public class Source
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class TopHeadlinesRequest
    {
        /// <summary>
        /// The keyword or phrase to search for. Boolean operators are supported.
        /// </summary>
        public string Q { get; set; }
        /// <summary>
        /// If you want to restrict the results to specific sources, add their Ids here. You can find source Ids with the /sources endpoint or on newsapi.org.
        /// </summary>
        public List<string> Sources = new List<string>();
        /// <summary>
        /// If you want to restrict the headlines to a specific news category, add these here.
        /// </summary>
        public Categories? Category { get; set; }
        /// <summary>
        /// The language to restrict articles to.
        /// </summary>
        public Languages? Language { get; set; }
        /// <summary>
        /// The country of the source to restrict articles to.
        /// </summary>
        public Countries? Country { get; set; }
        /// <summary>
        /// Each request returns a fixed amount of results. Page through them by increasing this.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Set the max number of results to retrieve per request. The max is 100.
        /// </summary>
        public int PageSize { get; set; }
    }
}