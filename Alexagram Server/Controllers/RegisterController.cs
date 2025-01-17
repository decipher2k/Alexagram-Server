﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexagram_Server.Views.EmptyData;
using Alexagram_Server.Views.UserCreated;
using Alexagram_Server.Views.UsernameTaken;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alexagram_Server.Controllers
{
    [Route("Register")]
    public class RegisterController : Controller
    {

        // GET: RegisterController
        [HttpGet]
        public ActionResult Index([FromQuery(Name = "state")] String state)
        {
            if (state == null)
            {
                return Ok("Please register using the alexa app.");
            }
            else
            {
                return View("Register", new Alexagram_Server.Views.Register.IndexModel() { state = state });
            }
        }


        private string genSession()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString();
        }


        // POST: RegisterController/Create
        [HttpPost("Create")]
        public ActionResult Create(IFormCollection collection)
        {


            if (collection["username"].ToString() == "" || collection["password"].ToString() == "")
                return View("EmptyData", new EmptyDataModel() { state = collection["state"].ToString() });
            var db = new SQLiteDBContext();
            if (db.Users.Where(a => a.username == collection["username"].ToString()).Count()==0)
            {
                db.Users.Add(new Entities.Users() { password = Globals.CreateMD5(collection["password"].ToString()), username = collection["username"].ToString(), session = genSession() });
                db.SaveChanges();
            }
            else
                return View("UsernameTaken", new UsernameTakenModel() { state = collection["state"].ToString() });
            return View("UserCreated", new UserCreatedModel() { state = collection["state"].ToString() });
        }

    }

}

    