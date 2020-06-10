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
        public ActionResult StartAuth([FromQuery(Name="state")]String state, [FromQuery(Name = "session")] String session)
        {
            return View("StartAuth",new IndexModel() { state = state,session=session });
        }

     

        [HttpPost("Step1")]
        public async Task<ActionResult> Step1(IFormCollection collection)
        {
            try { 
            // String session = "";
            //if(collection["session"].ToString()!="")
            String session = collection["session"].ToString();
           //else
           // session = collection["session"].ToString();

            TelegramClient client = new TelegramClient(1712234, "9d90d5de6f3b358e615873ce4ca4e8e1", Globals.store, session);
            await client.ConnectAsync();
            var hash = await client.SendCodeRequestAsync(collection["phone"].ToString());

            String state = collection["state"].ToString();
            return View("AuthStep2",new Views.AuthStep2.AuthStep2Model() { phone = collection["phone"].ToString() ,hash=hash, session=session,state=state});
            }catch(Exception ex)
            {
                return View("StartAuth", new IndexModel() { state = collection["state"].ToString(), session = collection["session"].ToString()
            });
            }
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
            try { 
                await client.MakeAuthAsync(phone.Replace("&#x2B;","+"), hash, code);
                
            }
            catch
            {
                return View("AuthAuthError", new Alexagram_Server.Views.AuthError.AuthAuthErrorModel { state = collection["state"].ToString() });
            }


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
