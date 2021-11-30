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
    public class TweetDAC
    {

        public GlitterDB db = new GlitterDB();
        public List<LikeDTO> GetLikeDislikes()
        {
            List<TweetLikeDislike> likeDislikes = new List<TweetLikeDislike>();
            List<LikeDTO> likeDTOs = new List<LikeDTO>();
            try
            {
                using (GlitterDB context = new GlitterDB())
                {
                    likeDislikes = context.Likes.ToList();

                    foreach (TweetLikeDislike ele in likeDislikes)
                    {
                        LikeDTO newDTO = new LikeDTO
                        {
                            LikeId = ele.LikeId,
                            TweetId = ele.TweetId,
                            UserId = ele.UserId,
                            IsLiked = ele.IsLiked
                        };
                        likeDTOs.Add(newDTO);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Console.Error.WriteLine(e);
            }

            return likeDTOs;
        }
    }
}
