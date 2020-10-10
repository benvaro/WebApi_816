using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static List<User> Users { get; set; } = new List<User>
        {
            new User{Id = 1, Email = "olia@gmail.com", Name = "Olia"},
            new User{Id = 2, Email = "ivan@gmail.com", Name = "Ivan"},
            new User{Id = 3, Email = "ania@gmail.com", Name = "ann"}
        };

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Users;
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            if (user != null)
            {
                Users.Add(user);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update(int id, User user)
        {
            var res = Users.FirstOrDefault(opt => opt.Id == id);
            if (res != null && user != null)
            {
                res.Name = user.Name;
                res.Email = user.Email;
                return Ok();
            }
            else if (res == null)
                return NotFound();
            return BadRequest();
        }
    }
    //CRUD  ?
    // HTTP - POST, PUT, DELETE, GET
    // Create  POST
    // Read    GET
    // Update  PUT
    // Delete  DELETE
    // domain.com/api/user
}
