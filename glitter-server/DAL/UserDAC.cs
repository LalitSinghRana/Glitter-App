using DAL.Entities;
using EntityModel;
using EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAC
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
                    System.Diagnostics.Debug.WriteLine("hashcount " + list.Count());
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

        public bool IsValidUser(User user)
        {
            var newUser = db.Users.Where(a => a.EmailId.Equals(user.EmailId) && a.Password.Equals(user.Password)).FirstOrDefault();
            if (newUser != null)
            {

                return true;
            }

            else
            {
                return false;
            }
        }

        public bool IsUserExist(User user)
        {
            var newUser = db.Users.FirstOrDefault(p => p.EmailId == user.EmailId);

            if (newUser != null)
                return true;
            else
                return false;
        }

        public bool IsEmailExist(string user)
        {
            var newUser = db.Users.FirstOrDefault(p => p.EmailId == user);

            if (newUser != null)
                return true;
            else
                return false;
        }

        public List<User> GetAllUser()
        {
            return db.Users.ToList();
        }
        public User getUser(User user)
        {
            var newUser = db.Users.FirstOrDefault(p => p.EmailId == user.EmailId);

            if (newUser != null)
                return newUser;
            else
                return null;

        }

        public User SaveCustomerDetails(User user)
        {
            User newuser = new User
            {
                Name = user.Name,
                EmailId = user.EmailId,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                CountryOfOrigin = user.CountryOfOrigin,
                Image = user.Image
            };
            db.Users.Add(newuser);
            db.SaveChanges();
            return newuser;
        }


        //----------------------

        // Add Tweet By User

        public void AddTweet(string message, string userId)
        {

            try
            {
                Tweet tmp = new Tweet();
                using (GlitterDB context = new GlitterDB())
                {
                    Tweet tweet = new Tweet
                    {
                        Message = message,
                        UserId = userId,
                        Date = DateTime.Now
                    };
                    context.Tweets.Add(tweet);
                    context.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Tweet id goes brr! " + tweet.TweetId);
                    tmp.TweetId = tweet.TweetId;
                }
                
                AddHashtag(message, tmp.TweetId);

            }
            catch (NullReferenceException)
            {
                //log for errors
                //Console.WriteLine(e);
            }

        }

        //Add HashTag

        private void AddHashtag(string message, int tweetId)
        {

            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    var regex = new Regex(@"(?<=#)\w+");
                    var matches = regex.Matches(message);

                    foreach (Match m in matches)
                    {
                        var hashtag = context.Hashtags.Where(x => x.TweetId == tweetId && x.TagName == m.Value).FirstOrDefault();
                        if (hashtag != null)
                        {
      
                            context.Entry(hashtag).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            Hashtag hash = new Hashtag
                            {
                                TagName = m.Value,
                                TweetId = tweetId,
  
                            };
                            context.Hashtags.Add(hash);
                            context.SaveChanges();
                        }
                    }

                }

            }
            catch (NullReferenceException)
            {
                //log for errors
                //Console.WriteLine(e);
            }
        }

        //EditTweet

        public void EditTweet(string message, int TweetId)
        {

            try
            {

                using (GlitterDB context = new GlitterDB())
                {
                    var tweet = context.Tweets.Where(x => x.TweetId == TweetId).FirstOrDefault();
                    if (tweet != null)
                    {
                        RemoveHashtags(tweet.Message, tweet.TweetId);
                        tweet.Message = message;
                        tweet.Date = DateTime.Now;
                        context.Entry(tweet).State = EntityState.Modified;
                        context.SaveChanges();

                        System.Diagnostics.Debug.WriteLine("Inside dac "+ tweet.Message);
                        AddHashtag(message, tweet.TweetId);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Inside dac error ");
                    }
                }


            }
            catch (NullReferenceException)
            {
                //log for errors
                //Console.WriteLine(e);
            }

        }

        public void DeleteTweet(int TweetId)
        {

            try
            {

                using (GlitterDB context = new GlitterDB())
                {
                    var tweet = context.Tweets.FirstOrDefault(x => x.TweetId == TweetId);
                    RemoveHashtags(tweet.Message, tweet.TweetId);
                    RemoveLike(tweet.TweetId);
                    System.Diagnostics.Debug.WriteLine(tweet.Message);

                    if (tweet != null)
                    {
                        context.Tweets.Remove(tweet);
                    }
                    context.SaveChanges();
                }


            }
            catch (NullReferenceException)
            {
                //log for errors
                //Console.WriteLine(e);
            }

        }

        
        public void RemoveLike(int tweetId)
        {


            try
            {
                using (GlitterDB context = new GlitterDB())
                {

                    List<TweetLikeDislike> likedislikes = context.Likes.Where(x => x.TweetId == tweetId).ToList();
                    foreach (TweetLikeDislike like in likedislikes)
                    {
                            context.Entry(like).State = EntityState.Deleted;

                            context.SaveChanges();
                    }

                }

            }
            catch (NullReferenceException)
            {
                //log for errors
                //Console.WriteLine(e);
            }

        }

        //Remove HashTags

        private void RemoveHashtags(string message, int tweetId)
        {

            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    var regex = new Regex(@"(?<=#)\w+");
                    var matches = regex.Matches(message);

                    foreach (Match m in matches)
                    {
                        var hashtag = context.Hashtags.Where(x => x.TweetId == tweetId && x.TagName == m.Value).FirstOrDefault();
                        if (hashtag != null)
                        {
                            context.Entry(hashtag).State = EntityState.Deleted;
                            
                            context.SaveChanges();

                        }
                        else
                        {
                            //log for errors
                        }
                    }

                }

            }
            catch (NullReferenceException)
            {
                //log for errors
                //Console.WriteLine(e);
            }
        }


        //Get All Tweets of the User

        public IList<Tweet> GetAllTweets(string userId)
        {

            IList<Tweet> returnedTweets = null;
            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    IList<string> users = GetFollowingUsers(userId);
                    users.Add(userId);
                    var tweets = (from p in context.Tweets
                                  where users.Contains(p.UserId)
                                  orderby p.Date descending
                                  select p).ToList();

                    if (tweets != null)
                    {
                        returnedTweets = new List<Tweet>();
                        foreach (var tweet in tweets)
                        {
                            Tweet returnedTweet = new Tweet
                            {
                                TweetId = tweet.TweetId,
                                Message = tweet.Message,
                                UserId = tweet.UserId,
                                Date = tweet.Date
                            };
                            returnedTweets.Add(returnedTweet);
                        }

                    }

                }
            }
            catch (NullReferenceException)
            {
                returnedTweets = null;
            }

            return returnedTweets;
        }

        //func used by get all tweet   AND      to Find Followee

        public IList<string> GetFollowingUsers(string userId)
        {

            IList<string> returnedUsers = null;
            try
            {
                using (GlitterDB context = new GlitterDB())
                {

                    var users = (from p in context.UserLinks
                                 select p).Where(x => x.FollowerId.Equals(userId)).ToList();

                    if (users != null)
                    {
                        returnedUsers = new List<string>();
                        foreach (var user in users)
                        {
                            returnedUsers.Add(user.FolloweeId);
                        }

                    }

                }
            }
            catch (NullReferenceException)
            {
                returnedUsers = null;
            }

            return returnedUsers;
        }


        // Follow User
        public void FollowUser(string currentUserId, string followingUserId)
        {

            try
            {
                using (GlitterDB context = new GlitterDB())
                {

                    var followEntry = (from p in context.UserLinks
                                       select p).Where(x => x.FolloweeId.Equals(currentUserId) && x.FollowerId.Equals(followingUserId)).FirstOrDefault();

                    if (followEntry != null)
                    {

                        context.Entry(followEntry).State = EntityState.Deleted;
                        context.SaveChanges();
                    }

                    else
                    {
                        FollowingUser returnedfollowEntry = new FollowingUser
                        {
                            FolloweeId = currentUserId,
                            FollowerId = followingUserId,
                        };
                        context.UserLinks.Add(returnedfollowEntry);
                        context.SaveChanges();
                    }

                }
            }
            catch (NullReferenceException)
            {

            }
        }


        //>>>>>>>>>>>>>>>>>>>

        public IList<string> GetFollowers(string userId)
        {

            IList<string> returnedUsers = null;
            try
            {
                using (GlitterDB context = new GlitterDB())
                {

                    var users = (from p in context.UserLinks
                                 select p).Where(x => x.FolloweeId.Equals(userId)).ToList();

                    if (users != null)
                    {
                        returnedUsers = new List<string>();
                        foreach (var user in users)
                        {
                            User tmp = context.Users.Where(x => x.EmailId == user.FollowerId).FirstOrDefault();
                            returnedUsers.Add(tmp.Name);
                        }

                    }

                }
            }
            catch (NullReferenceException)
            {
                returnedUsers = null;
            }

            return returnedUsers;
        }

        //  Get Followee
        public IList<User> GetFollowee(string userId)
        {

            IList<User> returnedUsers = null;
            try
            {
                using (GlitterDB context = new GlitterDB())
                {

                    var users = (from p in context.UserLinks
                                 select p).Where(x => x.FollowerId.Equals(userId)).ToList();

                    if (users != null)
                    {
                        returnedUsers = new List<User>();
                        foreach (var user in users)
                        {
                            User tmp = context.Users.Where(x => x.EmailId == user.FolloweeId).FirstOrDefault();
                            returnedUsers.Add(tmp);
                        }

                    }

                }
            }
            catch (NullReferenceException)
            {
                returnedUsers = null;
            }

            return returnedUsers;
        }

        //------------------------------------------------------- Search On Basis Of HashTags

        public IList<Tweet> SearchPosts(string searchString)
        {

            IList<Tweet> returnTweets = null;
            try
            {
                using (GlitterDB context = new GlitterDB())
                {

                    var Hashtags = (from p in context.Hashtags
                                    select p).Where(x => x.TagName.Contains(searchString)).ToList();

                    IList<int> tweetIds = new List<int>();
                    foreach (var hashtag in Hashtags)
                    {
                        if (hashtag != null)
                        {

                            tweetIds.Add(hashtag.TweetId);
                            context.Entry(hashtag).State = EntityState.Modified;
                            context.SaveChanges();
                        }
        
                    }

                    //Search Entries
                    List<Hashtag> uniqueTags = Hashtags.GroupBy(x => x.TagName).Select(x => x.First()).ToList();
                    
                    foreach(Hashtag hash in uniqueTags)
                    {
                        Search newSearch = new Search{ TagName = hash.TagName};
                        System.Diagnostics.Debug.WriteLine("Search " + newSearch.TagName);
                        context.Searchs.Add(newSearch);
                        context.SaveChanges();
                        
                    }

                    var tweets = (from p in context.Tweets
                                 where tweetIds.Contains(p.TweetId)
                                 select p).ToList();

                    if (tweets != null)
                    {
                        returnTweets = new List<Tweet>();
                        foreach (var tweet in tweets)
                        {
                            Tweet returnTweet = new Tweet
                            {
                                TweetId = tweet.TweetId,
                                Message = tweet.Message,
                                Date = tweet.Date,
                                UserId = tweet.UserId
                            };
                            returnTweets.Add(returnTweet);
                        }

                        return returnTweets;

                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        //-------------- Search Tweets Based on People

        public IList<User> SearchPeople(string searchString)
        {

            IList<User> returnUsers = null;
            try
            {
                using (GlitterDB context = new GlitterDB())
                {

                    var users = (from p in context.Users
                                 select p).Where(x => x.EmailId.Contains(searchString) || x.Name.Contains(searchString)).ToList();

                    if (users != null)
                    {
                        returnUsers = new List<User>();
                        foreach (var user in users)
                        {
                            User returnUser = new User
                            {
                                EmailId = user.EmailId,
                                Name = user.Name,
                                CountryOfOrigin = user.CountryOfOrigin,
                                Image = user.Image,
                                PhoneNumber = user.PhoneNumber,
                                Password = ""
                            };
                            returnUsers.Add(returnUser);
                        }

                    }

                }
            }
            catch (NullReferenceException)
            {
                returnUsers = null;
            }

            return returnUsers;

        }





        // Like Tweet

        public void LikeTweet(int TweetId, string userId)
        {

            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    var likedTweet = context.Likes.Where(x => x.UserId == userId && x.TweetId == TweetId).FirstOrDefault();
                    if (likedTweet != null)
                    {
                        if (likedTweet.IsLiked == true)
                        {
                            context.Entry(likedTweet).State = EntityState.Deleted;
                            context.SaveChanges();
                        }
                        else
                        {
                            likedTweet.IsLiked = true;
                            context.Entry(likedTweet).State = EntityState.Modified;
                            context.SaveChanges();
                        }

                    }
                    else
                    {
                        TweetLikeDislike like = new TweetLikeDislike
                        {
                            TweetId = TweetId,
                            UserId = userId,
                            IsLiked = true
                        };
                        context.Likes.Add(like);
                        context.SaveChanges();
                    }

                }

            }
            catch (NullReferenceException)
            {
                //log for errors
                //Console.WriteLine(e);
            }

        }


        //Dislike Tweet

        public void DislikeTweet(int TweetId, string userId)
        {

            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    var likedTweet = context.Likes.Where(x => x.UserId == userId && x.TweetId == TweetId).FirstOrDefault();
                    if (likedTweet != null)
                    {
                        if (likedTweet.IsLiked == false)
                        {
                            context.Entry(likedTweet).State = EntityState.Deleted;
                            context.SaveChanges();
                        }
                        else
                        {
                            likedTweet.IsLiked = false;
                            context.Entry(likedTweet).State = EntityState.Modified;
                            context.SaveChanges();
                        }

                    }
                    else
                    {
                        TweetLikeDislike like = new TweetLikeDislike
                        {
                            TweetId = TweetId,
                            UserId = userId,
                            IsLiked = false
                        };
                        context.Likes.Add(like);
                        context.SaveChanges();
                    }

                }

            }
            catch (NullReferenceException)
            {
                //log for errors
                //Console.WriteLine(e);
            }

        }



    }
}
