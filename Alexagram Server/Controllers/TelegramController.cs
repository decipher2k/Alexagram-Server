using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TLSchema;
using TLSchema.Contacts;
using TLSharp;

namespace Alexagram_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        // GET: api/Telegram
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Telegram/5
        
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Telegram
        [HttpGet("auth_start")]
        public async Task<IActionResult> auth_start([FromQuery(Name = "auth")] string auth)
        {
            if (auth != "Bl4f4s3L")
                throw new Exception();
            TelegramClient client = new TelegramClient(1712234, "9d90d5de6f3b358e615873ce4ca4e8e1");
            await client.ConnectAsync();
            var hash = await client.SendCodeRequestAsync("+4917641521701");
            
            return Ok(hash);
        }

        [HttpGet("auth_code")]
        public async Task<IActionResult> auth_code([FromQuery(Name = "code")] string code, [FromQuery(Name = "hash")] string hash, [FromQuery(Name = "auth")] string auth)
        {
            if (auth != "Bl4f4s3L")
                throw new Exception();
            TelegramClient client = new TelegramClient(1712234, "9d90d5de6f3b358e615873ce4ca4e8e1");
            await client.ConnectAsync();
            await client.MakeAuthAsync("+4917641521701", hash, code);
            return Ok("OK");
        }

        // POST: api/Telegram
        [HttpGet("send")]
        public async Task<IActionResult> Send([FromQuery(Name="username")] string username, [FromQuery(Name = "text")] string text, [FromQuery(Name = "auth")] string auth, [FromQuery(Name = "session")] string session)
        {
            if (auth != "Bl4f4s3L")
                throw new Exception();
         
                TelegramClient client = new TelegramClient(1712234, "9d90d5de6f3b358e615873ce4ca4e8e1", Globals.store, session);
                await client.ConnectAsync();
                //get available contacts
                TLContacts result = await client.GetContactsAsync();
                await client.ConnectAsync();
        
  
   
            

            //find recipient in contacts
            
            var user = result.Users.ToList()
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>();
            int user_id = 0;
            if (user.ToList().Count != 0)
            {
                foreach (var u in user)
                {
                
                    if(u.FirstName.ToLower()==username.ToLower())
                    { 
                        //send message
                        user_id = u.Id;
                    }
                }
            }
            await client.SendMessageAsync(new TLInputPeerUser() { UserId=user_id }, text);
            return Ok("OK");
        }

        // PUT: api/Telegram/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
