using DAL;
using DAL.Entities;
using EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AnalyticsBDC
    {
        AnalyticsDAC analyticsDAC = new AnalyticsDAC();
        public IList<Hashtag> GetHashtags()
        {
            IList<Hashtag> hashtags = analyticsDAC.GetHashtags();

            return hashtags;
        }



        public string GetTrendyTag()
        {
            string trendiest = analyticsDAC.GetTrendyTag();

            return trendiest;

        }

        public int GetTotalTweet()
        {
            int totalTweet = 27;

            totalTweet = analyticsDAC.TotalTweet();

            return totalTweet;

        }

        public User MaxTweeter()
        {

            User maxTweeter = new User();

            maxTweeter = analyticsDAC.MaxTweeter();

            return maxTweeter;
        }

        public Tweet MostLikedTweet()
        {

            Tweet mostLikedTweet = new Tweet();

            try
            {
                mostLikedTweet = analyticsDAC.MostLikedTweet();
            }
            catch( Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
           

            return mostLikedTweet;
            
        }


    }
}
