using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DB_Activities;

namespace CoreServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserDataController> _logger;

        public UserDataController(ILogger<UserDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetUserData")]
        public UserInfo Get(string content)
        {
            Dataaccess db = new Dataaccess();
            UserInfo user = new UserInfo();
            //if (db.Signup(user))
            //{

            //}

            if (db.CheckLogin(user))
            {
            }

            //db.DeleteUser(user);

            return user;

            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserInfo model)
        {
            Dataaccess db = new Dataaccess();
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = await db.Signup(model);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpPost("Login")]
        public ActionResult CheckUser(UserInfo user)
        {
            Dataaccess db = new Dataaccess();
            if (db.CheckLogin(user))
                return Ok();
            return Unauthorized();

        }
        //[HttpPost("SignUp")]
        //public ActionResult Post(UserInfo user)
        //{
        //    Dataaccess db = new Dataaccess();
        //    if (db.Signup(user))
        //        return Ok();

        //    return Conflict();

        //}

        [HttpGet("GetUsers")]
        public List<Profile> GetAllUsers()
        {
            Dataaccess db = new Dataaccess();
            return db.GetAllUsers();
        }

        [HttpGet("GetUser")]
        public Profile GetSelectedUser(string email)
        {
            Dataaccess db = new Dataaccess();
            return db.GetSelectedUser(email);
        }

        [HttpPut]
        public Profile Put([FromForm] Profile res) => UpdateUserData(res);

        private Profile UpdateUserData(Profile res)
        {
            Dataaccess db = new Dataaccess();
            db.UpdateUser(res);
            return res;
        }

        [HttpDelete("{id}")]
        public void Delete(string id) => DeleteUser(id);

        private void DeleteUser(string id)
        {
            Dataaccess db = new Dataaccess();
            db.DeleteUser(id);
        }
    }
}
