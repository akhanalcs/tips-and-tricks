using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class QuoteOfTheDay
    {
        public static QuoteOfTheDay Current { get; set; }

        static QuoteOfTheDay()
        {
            Current = new QuoteOfTheDay { Quote = "There's no quote fetched at this time", Author = "Ashish", FetchedTime = null };
        }

        public string Quote { get; set; }
        public string Author { get; set; }
        public DateTime? FetchedTime { get; set; }
    }
}
