using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class HashTagDTO
    {

        public HashTagDTO()
        {
            TagName = "";
            Count = 0;
            SearchIndex = 1;
        }
        public string TagName;

        public int Count;

        public int SearchIndex;
    }
}
