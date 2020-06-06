using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Alexagram_Server.Views.StartAuth
{
    public class IndexModel : PageModel
    {
        public String state { get; set; }
        public void OnGet()
        {
        }
    }
}
