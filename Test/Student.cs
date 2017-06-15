using SqlHelp.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [Serializable]
    [Table("t_schools")]
   public class School 
    {
        [PriamKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
