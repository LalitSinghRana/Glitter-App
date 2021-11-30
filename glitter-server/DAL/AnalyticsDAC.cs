using DAL.Entities;
using EntityModel;
using EntityModel.Entities;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class AnalyticsDAC
    {
        public GlitterDB db = new GlitterDB();

        public List<Hashtag> GetHashtags()
        {

            List<Hashtag> list = new List<Hashtag>();
            try
            {
                using (GlitterDB context = new GlitterDB())
                {

                    list = context.Hashtags.ToList();
                }
            }
            catch (NullReferenceException e)
            {
                //log for errors
                //Console.WriteLine(e);

                Console.Error.WriteLine(e);
            }

            return list;
        }

        public List<HashTagDTO> GroupByCount(List<Hashtag> tags)
        {
            var groups = tags.GroupBy(x => x.TagName);
            List<HashTagDTO> list = groups.Select(x => new HashTagDTO { TagName = x.Key, Count = x.Count() }).ToList<HashTagDTO>();

            return list;
        }

        public List<HashTagDTO> GroupBySearch(List<Search> searches)
        {
            var groups = searches.GroupBy(x => x.TagName);
            List<HashTagDTO> list = groups.Select(x => new HashTagDTO { TagName = x.Key, Count = x.Count() }).ToList<HashTagDTO>();

            return list;
        }

        public string GetTrendyTag()
        {
            string trendiest = "";

            try
            {  
                using (GlitterDB context = new GlitterDB())
                {
                    var tags = context.Hashtags.ToList();

                    var searchs = context.Searchs.ToList();

                    System.Diagnostics.Debug.WriteLine("Count is wow:" + tags.Count());

                    //Logic gives equal weight to count and search and count as tie breaker


                    List<HashTagDTO> ByCountlist = GroupByCount(tags);
                    List<HashTagDTO> BySearchlist = GroupBySearch(searchs);

                    var uniqueTags = new Dictionary<string, int>();

                    foreach(var ele in ByCountlist)
                    {
                        uniqueTags.Add(ele.TagName, ele.Count);
                    }


                    foreach (var ele in BySearchlist)
                    {
                        if(uniqueTags.ContainsKey(ele.TagName)) uniqueTags[ele.TagName] += ele.Count;
                    }


                    string TagName = uniqueTags.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

                    trendiest = TagName;

                    System.Diagnostics.Debug.WriteLine("Trendiest Hashtag : " + trendiest);
                   
                }
            }
            catch (NullReferenceException e)
            {
                //log for errors
                //Console.WriteLine(e);

                Console.Error.WriteLine(e);
            }

            return trendiest;

        }

        public int TotalTweet()
        {
            int totalTweet = 14;
            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    List<Tweet> tweets = context.Tweets.ToList();

                    totalTweet = tweets.Where(ele => ele.Date.Day == DateTime.Today.Day).Count();

                    //Logic gives equal weight to count and search and count as tie breaker

                    System.Diagnostics.Debug.WriteLine("Today Tweet count : " + totalTweet);

                }

            }
            catch(NullReferenceException )
            {
               
            }

            return totalTweet;
        }

        public User MaxTweeter()
        {

            User maxTweeter = new User();
            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    List<Tweet> tweets = context.Tweets.ToList();

                    var groups = tweets.GroupBy(x => x.UserId);
                    var largest = groups.OrderByDescending(x => x.Count()).First();

                    string userId = largest.Key;

                    //Logic gives equal weight to count and search and count as tie breaker

                    System.Diagnostics.Debug.WriteLine("Most tweety guy : " + userId);

                    maxTweeter = context.Users.Where(x => x.EmailId == userId).FirstOrDefault();

                }

            }
            catch
            {
                maxTweeter = null;  
            }

            return maxTweeter;
            
        }

        public Tweet MostLikedTweet()
        {

            Tweet mostLikedTweet = new Tweet();
            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    List<TweetLikeDislike> likes = context.Likes.Where(x => (x.IsLiked == true)).ToList();
 

                    var groups = likes.GroupBy(x => x.TweetId);
                    var largest = groups.OrderByDescending(x => x.Count()).First();

                    int tweetId = largest.Key;

                    mostLikedTweet = context.Tweets.Where(x => x.TweetId == tweetId).FirstOrDefault();

                    //Logic gives equal weight to count and search and count as tie breaker

                    System.Diagnostics.Debug.WriteLine("Most tweety guy : " + tweetId);

                }

            }
            catch(NullReferenceException)
            {

                mostLikedTweet = null;
            }

            return mostLikedTweet;
        }


    }
}
