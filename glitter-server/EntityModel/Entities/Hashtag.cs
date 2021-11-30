using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel.Entities
{
    public class Hashtag
    {
        [Key]
        public int HashTagId { get; set; }

        public string TagName { get; set; }

        public int TweetId { get; set; }

        [ForeignKey("TweetId")]
        public virtual Tweet Tweet { get; set; }
    }
}
