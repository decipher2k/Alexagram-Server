using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace Alexagram_Server.Views.UserCreated
{
    public class UserCreatedModel : PageModel
    {
        public StringValues state { get; internal set; }

        public void OnGet()
        {
        }
    }
}
