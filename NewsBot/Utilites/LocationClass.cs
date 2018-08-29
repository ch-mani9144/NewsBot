using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsBot.Utilites
{
    public class LocationClass
    {
        public static IList<Entity> EntitiesFromActivity { get; set; }
        public static double? UserLatitude { get; set; }
        public static double? UserLongitude { get; set; }
        public static double? EndLatitude { get; set; }
        public static double? EndLongitude { get; set; }
    }
}