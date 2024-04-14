using diploma.Models;
using diploma.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace diploma.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationContext _context;
        static List<Role> rol = new List<Role>();

        public RoleController(ApplicationContext context)
        {
            _context = context;

            rol = _context.Roles.ToList();
        }

        public IActionResult Index()
        {
            return View(_context.Roles.ToList());
        }

        //[HttpPost]
        public async Task<ActionResult> Delete([FromBody] string id)
        {
            Role role = await _context.Roles.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(id));
            if (role != null)
            {
                _context.Roles.Remove(role);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Role");
            }
            else
                ModelState.AddModelError("", "Не удалось удалить!");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit([FromBody] string parameters)
        {
            string[] data = parameters.Split("+");

            int id =  Convert.ToInt32(data[0]);
            var name = data[1];

            if (Convert.ToInt32(id) != null)
            {
                Role role = await _context.Roles.FirstOrDefaultAsync(p => p.Id == Convert.ToInt32(id));
                if (role != null){

                    role.Name = name; // задаем новое имя, введенное пользователем

                    _context.Roles.Update(role);
                    await _context.SaveChangesAsync();
                }
            }

            return Json(id);
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id != null)
        //    {
        //        Role role = await _context.Roles.FirstOrDefaultAsync(p => p.Id == id);
        //        if (role != null)
        //            return View(role);
        //    }
        //    return NotFound();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(Role role)
        //{
        //    _context.Roles.Update(role);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        //public async Task<IActionResult> Create([FromBody] string name)//RoleViewModel model)
        //{
        //    RoleViewModel model = new RoleViewModel() { Name = name };

        //    if (ModelState.IsValid)
        //    {
        //        Role role = await _context.Roles.FirstOrDefaultAsync(u => u.Id == model.Id); // !!!!
        //        Console.WriteLine("2");
        //        if (role == null)
        //        {
        //            // добавляем пользователя в бд
        //            role = new Role { Name = model.Name};

        //            _context.Roles.Add(role);
        //            await _context.SaveChangesAsync();

        //            return RedirectToAction("Index", "Role");
        //        }
        //        else
        //            ModelState.AddModelError("", "Некорректные данные!");
        //    }
        //    return Json(model);
        //}


        //[HttpPost]
        //public IActionResult Test(Role role)
        //{
        //    _context.Roles.Add(role);
        //    _context.SaveChanges();

        //    //Fetch the CustomerId returned via SCOPE IDENTITY.
        //    int id = role.Id;

        //    return View(role);
        //}
    }
}
