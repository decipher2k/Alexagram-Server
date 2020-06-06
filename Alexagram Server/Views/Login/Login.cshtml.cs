using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Alexagram_Server.Views.Login
{
    public class IndexModel : PageModel
    {
        public string state { get; internal set; }

        public void OnGet()
        {
        }
    }
}
