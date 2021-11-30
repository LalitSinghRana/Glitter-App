using BL;
using DAL.Entities;
using EntityModel.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Glitter.API.Controllers
{
    public class AccountController : ApiController
    {
        UserBDC userBusinessLogic = new UserBDC();

        //[AllowCrossSiteJson]
        [Route("api/Account/Login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody] User user)
        {
            bool IsUserExist = userBusinessLogic.IsValidUser(user);

            if (IsUserExist == true)
            {

                User usr = userBusinessLogic.GetUser(user);
                System.Diagnostics.Debug.WriteLine(usr);
                return Ok(usr);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("fail");
                return NotFound();
            }
                
        }

        [Route("api/Account/IsValid")]
        [HttpPost]
        public bool IsValid([FromBody] User user)
        {

            //user.Image = saveImage(user.Image, user.Name);
            string email = user.EmailId;
            bool IsUserExist = userBusinessLogic.IsEmailPresentAlready(email);

            if (IsUserExist == true)
            {
                return false;
            }
            else
            {

                return true;
            }

        }


        public IHttpActionResult Register([FromBody] User user)
        {

            //user.Image = saveImage(user.Image, user.Name);

            bool IsUserExist = userBusinessLogic.IsUserPresentAlready(user);

            if (IsUserExist == true)
            {
                System.Diagnostics.Debug.WriteLine(" Failed Reg");
                return Content(HttpStatusCode.BadRequest, "Any object");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    userBusinessLogic.SaveUserData(user);
                }

                return Ok("Registered");
            }

        }




        
        [Route("api/Account/followUser")]
        [HttpPost]
        public IHttpActionResult FollowUser([FromBody] FollowingUser userLinkObj)
        {
            if (userLinkObj.FolloweeId != null && userLinkObj.FollowerId != null)
            {
                userBusinessLogic.Follow(userLinkObj.FolloweeId, userLinkObj.FollowerId);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/Account/getFollower")]
        [HttpPost]
        public IHttpActionResult GetFollower([FromBody] User userObj)
        {
            IList<string> UserList = userBusinessLogic.GetAllFollow(userObj);

            if (UserList != null)
            {

                return Ok(UserList);
            }
            else
            {
                return NotFound();
            }
        }


        
        [Route("api/Account/getFollowee")]
        [HttpPost]
        public IHttpActionResult GetFollowee([FromBody] User userObj)
        {
            IList<User> UserList = userBusinessLogic.GetAllFollowee(userObj);

            if (UserList != null)
            {

                return Ok(UserList);
            }
            else
            {
                return NotFound();
            }
        }


        
        [Route("api/Account/searchPostTag")]
        [HttpPost]
        public IHttpActionResult SearchPostTag([FromBody] Hashtag hastagObj)
        {
            IList<Tweet> UserList = userBusinessLogic.GetAllTweetBasedOnHashTags(hastagObj);

            if (UserList != null)
            {

                return Ok(UserList);
            }
            else
            {
                return NotFound();
            }
        }


        
        [Route("api/Account/searchPostPeople")]
        [HttpPost]
        public IHttpActionResult SearchPostPeople([FromBody] User userObj)
        {
            IList<User> UserList = userBusinessLogic.GetAllTweetBasedOnPeople(userObj);

            if (UserList != null)
            {

                return Ok(UserList);
            }
            else
            {
                return NotFound();
            }
        }





        
        [Route("api/Account/likeTweet")]
        [HttpPost]
        public IHttpActionResult LikeTweet([FromBody] Tweet tweeObj)
        {
            bool result = userBusinessLogic.LikeTweet(tweeObj);

            if (result)
            {

                return Ok("Succesfully like");
            }
            else
            {
                return NotFound();
            }
        }


        
        [Route("api/Account/dislikeTweet")]
        [HttpPost]
        public IHttpActionResult DisLikeTweet([FromBody] Tweet tweeObj)
        {
            bool result = userBusinessLogic.DisLikeTweet(tweeObj);

            if (result)
            {

                return Ok("Succesfully dislike");
            }
            else
            {
                return NotFound();
            }
        }


        public string saveImage(string image, string name)
        {
            string imageName = null;
            imageName = new string(Path.GetFileNameWithoutExtension(name).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(name);

            byte[] bytes = Convert.FromBase64String(image);
            using (Image actualImage = Image.FromStream(new MemoryStream(bytes)))
            {
                //actualImage.Save("output.jpg", ImageFormat.Jpeg); 
                actualImage.Save(System.Web.HttpContext.Current.Server.MapPath("~/Images/" + imageName));// Or Png
            }

            return imageName;
        }


        public string getImage(string imageName)
        {

            string path = HttpContext.Current.Server.MapPath("~/Images/") + imageName;
            string base64String;
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }

        }




    }
}
