using AutoMapper;
using DAL;
using DAL.Entities;
using EntityModel.Entities;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TweetBDC
    {
        TweetDAC tweet = new TweetDAC();

       public List<LikeDTO> GetLikes()
        {
            List<LikeDTO> likes = new List<LikeDTO>();
            try
            {
                likes = tweet.GetLikeDislikes();
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex);
            }

            return likes;
        }


    }
}
