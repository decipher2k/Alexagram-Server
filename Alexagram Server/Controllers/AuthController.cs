using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexagram_Server.Views.StartAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TLSharp;

namespace Alexagram_Server.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        // GET: AuthController
        [HttpGet("StartAuth")]
        public ActionResult StartAuth([FromQuery(Name="state")]String state)
        {
            return View("StartAuth",new IndexModel() { state = state });
        }

     

        [HttpPost("Step1")]
        public async Task<ActionResult> Step1(IFormCollection collection)
        {
            String session = genSession();
            TelegramClient client = new TelegramClient(1712234, "9d90d5de6f3b358e615873ce4ca4e8e1", Globals.store, session);
            await client.ConnectAsync();
            var hash = await client.SendCodeRequestAsync(collection["phone"].ToString());

            String state = collection["state"].ToString();
            return View("AuthStep2",new Views.AuthStep2.AuthStep2Model() { phone = collection["phone"].ToString() ,hash=hash, session=session,state=state});
        }

        private string genSession()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }

        // POST: AuthController/Create
        [HttpPost("Step2")]
        public async Task<ActionResult> Step2(IFormCollection collection)
        {
            String hash = collection["hash"].ToString();
            String code = collection["code"].ToString();
            String phone = collection["phone"].ToString();
            String session = collection["session"].ToString();
            String state = collection["state"].ToString();
            TelegramClient client = new TelegramClient(1712234, "9d90d5de6f3b358e615873ce4ca4e8e1", Globals.store,session);
            await client.ConnectAsync();
            await client.MakeAuthAsync(phone.Replace("&#x2B;","+"), hash, code);


            try
            {
                return Redirect("https://pitangui.amazon.com/spa/skill/account-linking-status.html?vendorId=M119BPCFJ72YPD#state=" + state+ "&access_token=" + session+ "&token_type=Bearer");
            }
            catch
            {
                return View();
            }
        }

        

    }
}
