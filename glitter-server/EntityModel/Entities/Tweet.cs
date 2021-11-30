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
    public class Tweet
    {
        [Key]
        public int TweetId { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
