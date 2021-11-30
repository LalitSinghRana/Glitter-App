using BL;
using DAL.Entities;
using EntityModel.Entities;
using Shared.DTO;
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
    public class TweetController : ApiController
    {

        TweetBDC tweetBusinessLogic = new TweetBDC();
        UserBDC userBusinessLogic = new UserBDC();

        //[AllowCrossSiteJson]
        [Route("api/Tweet/likes")]
        [HttpGet]
        public IHttpActionResult GetLikes()
        {
            List<LikeDTO> likes = tweetBusinessLogic.GetLikes();

            return Ok(likes);
        }



        [Route("api/Account/addTweet")]
        [HttpPost]
        public IHttpActionResult AddTweet([FromBody] Tweet tweetObj)
        {
            bool result = userBusinessLogic.AddTweet(tweetObj);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok("Tweet created");
            }
        }


        [Route("api/Account/editTweet")]
        [HttpPost]
        public IHttpActionResult EditTweet([FromBody] Tweet tweetObj)
        {
            bool result = userBusinessLogic.EditTweet(tweetObj);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        [Route("api/Account/deleteTweet")]
        [HttpPost]
        public IHttpActionResult DeleteTweet([FromBody] Tweet tweetObj)
        {
            bool result = userBusinessLogic.DeleteTweet(tweetObj);

            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }


        //[AllowCrossSiteJson]
        [Route("api/Account/getAllTweet")]
        [HttpGet]
        public IList<Tweet> GetAllTweet(string email)
        {
            User userObj = new User();
            userObj.EmailId = email;
            IList<Tweet> ListOfTweet = userBusinessLogic.GetAllTweets(userObj);
            if (ListOfTweet == null)
            {
                return null;
            }
            else
            {
                return ListOfTweet;
            }
        }


    }
}
