using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Alexagram_Server.Views.AuthStep2
{
    public class AuthStep2Model : PageModel
    {
        public String phone { get; set; }
        public String hash { get; set; }
        public string session { get; internal set; }
        public string state { get; internal set; }

        public void OnGet()
        {
        }
    }
}
