using BL;
using DAL.Entities;
using EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
namespace Glitter.API.Controllers
{
    public class AnalyticsController : ApiController
    {
        AnalyticsBDC analyticsBDC = new AnalyticsBDC();

        [Route("api/Analytics/getHashtags")]
        [HttpGet]
        public IList<Hashtag> GetHashtags()
        {
            IList<Hashtag> hashtags = analyticsBDC.GetHashtags();

            return hashtags;
        }


        [Route("api/Analytics/trendytag")]
        [HttpGet]
        //[ResponseType(typeof(Hashtag))]
        public string TrendyTag()
        {

            string hashtag = analyticsBDC.GetTrendyTag();

            if (hashtag == null) return null;

            else return hashtag;
        }

     
        [Route("api/Analytics/totaltweet")]
        [HttpGet]
        [ResponseType(typeof(int))]
        public int TotalTweet()
        {
            int result = analyticsBDC.GetTotalTweet();

            return result;
        }


        [Route("api/Analytics/maxtweet")]
        [HttpGet]
        [ResponseType(typeof(User))]
        public User MostTweets()
        {
            User result = analyticsBDC.MaxTweeter();

            return result;
        }

        [Route("api/Analytics/mostliked")]
        [HttpGet]
        [ResponseType(typeof(Tweet))]
        public Tweet MostLiked()
        {
            Tweet result = analyticsBDC.MostLikedTweet();

            return result;
        }



    }
}
