using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsBot.Utilites
{

    public class EixQuoteDS
    {
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string primaryExchange { get; set; }
        public string sector { get; set; }
        public string calculationPrice { get; set; }
        public float open { get; set; }
        public long openTime { get; set; }
        public float close { get; set; }
        public long closeTime { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float latestPrice { get; set; }
        public string latestSource { get; set; }
        public string latestTime { get; set; }
        public long latestUpdate { get; set; }
        public int latestVolume { get; set; }
        public object iexRealtimePrice { get; set; }
        public object iexRealtimeSize { get; set; }
        public object iexLastUpdated { get; set; }
        public float delayedPrice { get; set; }
        public long delayedPriceTime { get; set; }
        public float previousClose { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public object iexMarketPercent { get; set; }
        public object iexVolume { get; set; }
        public int avgTotalVolume { get; set; }
        public object iexBidPrice { get; set; }
        public object iexBidSize { get; set; }
        public object iexAskPrice { get; set; }
        public object iexAskSize { get; set; }
        public long marketCap { get; set; }
        public float peRatio { get; set; }
        public float week52High { get; set; }
        public float week52Low { get; set; }
        public float ytdChange { get; set; }
    }

}