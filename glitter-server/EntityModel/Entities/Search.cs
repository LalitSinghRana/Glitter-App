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
    public class Search
    {
        [Key]
        public int SearchTagId { get; set; }

        public string TagName { get; set; }

     
    }
}
