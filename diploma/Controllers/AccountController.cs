using diploma.Models;
using diploma.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace diploma.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext _context;
        public static string oldPassword;
        public AccountController(ApplicationContext context)
        {
            _context = context;
        }
        /*
        [HttpGet]
        public IActionResult Register()
        {
            var RoleList = (from role in _context.Roles
                            select new SelectListItem()
                            {
                                Text = role.Name,
                                Value = role.Id.ToString(),
                            }).ToList();

            ViewBag.Roles = RoleList;

            return View();
        }*/

        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] string parameters)
        {
            string[] data = parameters.Split("+");

            var name = data[0]; // имя нового пользователя
            var surname = data[1]; // фамилия нового пользователя
            var patr = data[2]; // отчество нового пользователя
            var email = data[3]; // почта нового пользователя
            var roleid = data[4]; // роль нового пользователя
            var login = data[5]; // логин нового пользователя
            var passw = data[6]; // пароль нового пользователя

            Console.WriteLine(name);
            Console.WriteLine(surname);
            Console.WriteLine(patr);
            Console.WriteLine(email);
            Console.WriteLine(roleid);
            Console.WriteLine(login);
            Console.WriteLine(passw);

            Role newRole = _context.Roles.FirstOrDefault(n => n.Id == Convert.ToInt32(roleid));

            User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

            if (user == null)
            {
                // добавляем пользователя в бд
                user = new User { Surname = surname, Name = name, Patronymic = patr, Role = newRole, Login = login, Password = passw, Email = email };

                string folder_name = user.Login;

                // Используем тогда, когда надо добавить только пользователя
                //Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == 1);
                //if (userRole != null)
                //user.Role = userRole;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                //return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return Json(parameters);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, string pass)
        {
            LoginModel model = new LoginModel() { 
                Login = login,
                Password = pass
            };

            if (ModelState.IsValid)
            {
                User user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                }
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EditProfile(string login)
        {
            if (login != null)
            {
                User user = _context.Users.FirstOrDefault(n => n.Login.Equals(login));

                ViewBag.NameProfile = user.Surname + " " + user.Name + " " + user.Patronymic;
                ViewBag.Name = user.Surname + " " + user.Name;

                if (user != null)
                {
                    oldPassword = user.Password;

                    ViewBag.OldPass = user.Password;

                    return View(user);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(User user)
        {

            // извлекаем нужную роль
            Role role = _context.Roles.Where(n => n.Name == "user").FirstOrDefault();

            user.Role = role;

            user.Password = oldPassword;

            // обновляем данные в бд
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("EditProfile", "Account", new { login = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name)).Login});
        }

        public async Task<IActionResult> ChangePassword([FromBody] string parameters)
        {
            string[] splitted = parameters.Split("-");

            var id = splitted[1];
            var newPass = splitted[0];

            User user = _context.Users.FirstOrDefault(n => n.Id.Equals(Convert.ToInt32(id)));

            if (user != null)
            {
                user.Password = newPass;

                // обновляем данные в бд
                _context.Users.Update(user);
                await _context.SaveChangesAsync();


                //return RedirectToAction("EditProfile", "Account", new { login = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name)).Login });
            }

            //return RedirectToAction("EditProfile", "Account", new { login = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name)).Login });
            return Json(parameters) ;
        }
    }
}
