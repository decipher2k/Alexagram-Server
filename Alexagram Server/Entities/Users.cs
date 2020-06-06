using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexagram_Server.Entities
{
    public class Users
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String session { get; set; }
    }
}
