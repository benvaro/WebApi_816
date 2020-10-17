using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext context;

        public UserController(ApplicationContext _context)
        {
            context = _context;
        }

        //static List<User> Users { get; set; } = new List<User>
        //{
        //    new User{Id = 1, Email = "olia@gmail.com", Name = "Olia"},
        //    new User{Id = 2, Email = "ivan@gmail.com", Name = "Ivan"},
        //    new User{Id = 3, Email = "ania@gmail.com", Name = "ann"}
        //};

        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            return context.Users.Select(x => new UserDTO { Id = x.Id, Email = x.Email, Name = x.Name, Role = x.Role.Name });
        }

        [HttpPost]
        public IActionResult Add(UserDTO user)
        {
            if (user != null)
            {
                context.Users.Add(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Role = context.Roles.FirstOrDefault(x => x.Name == user.Role)
                });
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddRole(RoleDTO role)
        {
            if (role != null)
            {
                context.Roles.Add(new Role
                {
                    Name = role.Name,
                });
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("all")]
        public IActionResult All()
        {
            return Ok(context.Roles.Include(x => x.Users).ToList());
        }

        [HttpPut]
        public IActionResult Update(int id, UserDTO user)
        {
            var res = context.Users.FirstOrDefault(opt => opt.Id == id);
            if (res != null && user != null)
            {
                res.Name = user.Name;
                res.Email = user.Email;
                res.Role = context.Roles.FirstOrDefault(x => x.Name == user.Role);

                context.Update(res);
                context.SaveChanges();
                return Ok();
            }
            else if (res == null)
                return NotFound();
            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        // api/user/1
        public IActionResult Get(int id)
        {
            var user = context.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == id);
            if (user != null)
                return Ok(new UserDTO { Id = user.Id, Name = user.Name, Email = user.Email, Role = user.Role.Name });
            return NotFound();
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
