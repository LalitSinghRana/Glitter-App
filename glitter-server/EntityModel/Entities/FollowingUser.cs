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
    public class FollowingUser
    {
        [Key]
        public int UserLinkId { get; set; }

        public string FolloweeId { get; set; }

        public string FollowerId { get; set; }

        [ForeignKey("FolloweeId")]
        public virtual User Followee { get; set; }

        [ForeignKey("FollowerId")]
        public virtual User Follower { get; set; }
    }

}
