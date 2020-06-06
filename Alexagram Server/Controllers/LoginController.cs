﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alexagram_Server.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        [HttpGet]
        // GET: LoginController
        public ActionResult Index([FromQuery(Name = "state")] String state)
        {
            return View("Login",new Alexagram_Server.Views.Login.IndexModel() { state = state });
        }

      

        // POST: LoginController/Create
        [HttpPost("Login")]
        public ActionResult Login(IFormCollection collection)
        {
            try
            {
                var db = new SQLiteDBContext();
                if (db.Users.Where(a => a.username == collection["username"].ToString() && a.password == collection["password"].ToString()).Count() > 0)//TODO MD5
                {
                    if (db.Users.Where(a => a.username == collection["username"].ToString() && a.password == collection["password"].ToString()).Single().session != null && db.Users.Where(a => a.username == collection["username"].ToString() && a.password == collection["password"].ToString()).Single().session != "")
                        return Redirect("https://pitangui.amazon.com/api/skill/link/M119BPCFJ72YPD?state=" + collection["state"].ToString() + "&code=" + db.Users.Where(a => a.username == collection["username"].ToString() && a.password == collection["password"].ToString()).Single().session);
                    // return Ok("{'access_token':'"+db.Users.Where(a => a.username == collection["username"] && a.password == collection["password"]).Single().session+"','token_type':'bearer','expires_in':3600,'refresh_token':'" + db.Users.Where(a => a.username == collection["username"] && a.password == collection["password"]).Single().session + "'}");
                    else
                    {
                        return Redirect("/Auth/StartAuth/?state=" + collection["state"].ToString());
                    }
                }
                return View("AuthError");
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
     
    }
}
