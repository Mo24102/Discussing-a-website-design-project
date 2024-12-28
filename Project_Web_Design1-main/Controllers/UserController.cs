using MyProject.Models;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using Microsoft.AspNetCore.Http;  

namespace MyProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var existingUser = _context.users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                if (existingUser.Password == user.Password)
                {
                    HttpContext.Session.SetString("UserEmail", existingUser.Email);
                    HttpContext.Session.SetString("UserRole", existingUser.Role.ToString());  

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Invalid password.";
                    return View(user);
                }
            }
            else
            {
                ViewBag.Error = "User does not exist.";
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            
            var firstUser = _context.users.Count() == 0;
            if (firstUser)
            {
                user.Role = UserRole.Admin;  
            }
            else
            {
                user.Role = UserRole.User;  
            }

            _context.users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        
        [HttpGet]
        public IActionResult ManageUsers()
        {
            
            var users = _context.users.ToList();
            return View(users);  
        }

        //[HttpGet]
        //public IActionResult ChangeUserRole(int userId)
        //{
        //    var user = _context.users.FirstOrDefault(u => u.Id == userId);
        //    if (user != null)
        //    {
        //        // تغيير الدور بين Admin و User
        //        if (user.Role == MyProject.Models.UserRole.Admin)
        //        {
        //            user.Role = MyProject.Models.UserRole.User;
        //        }
        //        else
        //        {
        //            user.Role = MyProject.Models.UserRole.Admin;
        //        }

        //        _context.SaveChanges();
        //    }

        //    return RedirectToAction("ManageUsers"); 
        //}
        [HttpPost]
        public IActionResult ChangeUserRole(int userId)
        {
            var user = _context.users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
              
                if (user.Role == UserRole.Admin)
                {
                    user.Role = UserRole.User;
                }
                else
                {
                    user.Role = UserRole.Admin;
                }

                _context.SaveChanges();
            }

            return RedirectToAction("ManageUsers");
        }

    }
}
