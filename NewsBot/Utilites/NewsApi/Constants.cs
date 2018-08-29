using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsBot.Utilites.NewsApi
{
    [JsonConverter(typeof(StringEnumConverter))]
    [Serializable]
    public enum Categories
    {
        Business,
        Entertainment,
        Health,
        Science,
        Sports,
        Technology
    }

    [Serializable]
    public enum Statuses
    {
        /// <summary>
        /// Request was successful
        /// </summary>
        Ok,
        /// <summary>
        /// Request failed
        /// </summary>
        Error
    }
}