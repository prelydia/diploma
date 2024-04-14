using diploma.Models;
using diploma.ViewModels;
using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class UserController : Controller
    {
        private ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;
        private string path;
        public static string fullPath;
        private string filename;
        public static string filetype;
        public static string foldername;
        public static string main_path;
        public static string old_name;
        public string check;

        public UserController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            filename = null;
            //path = @"C:\Users\l.khusainova\source\repos\diploma\diploma\wwwroot\files\";
            path = @"wwwroot\files\";
        }

        public IActionResult Index(int page = 1)
        {
            // Извлекаем роли системы
            var RoleList = (from role in _context.Roles
                            select new SelectListItem()
                            {
                                Text = role.Name,
                                Value = role.Id.ToString(),
                            }).ToList();

            ViewBag.Roles = RoleList;

            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            // Вариант с пагинацией пользователей
            int pageSize = 10;
            var source = _context.Users.Include(n => n.Role).Where(r => r.Role.Id == 1 || r.Role.Id == 2003).ToList();

            var count = source.Count();
            var items = source.OrderBy(n => n.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            UsersViewModel viewModel = new UsersViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
            };

            return View(viewModel);
        }

        //[HttpPost]
        public async Task<IActionResult> Delete([FromBody] string id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(id));
            if (user != null)
            {
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "User");
            }
            else
                ModelState.AddModelError("", "Не удалось удалить!");
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            var RoleList = (from role in _context.Roles
                            select new SelectListItem()
                            {
                                Text = role.Name,
                                Value = role.Id.ToString(),
                            }).ToList();

            ViewBag.Roles = RoleList.Where(n => n.Value != "2");

            if (id != null)
            {
                User user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

                ViewBag.User = user.Login;

                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            // извлекаем нужную роль
            Role role = _context.Roles.Where(n => n.Id == user.Role.Id).FirstOrDefault();
            user.Role = role;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult MyDirectory(string name, DateTime seldate)
        {
            /*
            User usr = _context.Users.Where(m => m.Login == User.Identity.Name).FirstOrDefault();

            List<diploma.Models.File> result = new List<diploma.Models.File>();

            // Вывести все файлы системы
            if (name == null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001")))
            {
                result = _context.Files.Where(n => n.Author.Equals(usr)).ToList();
            }
            else if (name != null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001")))
            {
                result = _context.Files.Where(n => n.Author.Equals(usr) && n.Name.Contains(name)).ToList();
            }
            else if (name != null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001")))
            {
                result = _context.Files.Where(n => n.Author.Equals(usr) && n.Name.Contains(name) && n.Date.Date == seldate.Date).ToList();
            }
            else if (name == null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001")))
            {
                result = _context.Files.Where(n => n.Author.Equals(usr) && n.Date.Date == seldate.Date).ToList();
            }

            return View(result);
            */

            return View();
        }

        public IActionResult FoldersFiles(string foldpath, string name, DateTime seldate, string contains)
        {
            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            if ( foldpath != null){    main_path = foldpath;}

            User usr = _context.Users.Where(m => m.Login == User.Identity.Name).FirstOrDefault();

            List<diploma.Models.File> result = new List<diploma.Models.File>();

            string locfoldername = main_path.Substring(main_path.LastIndexOf("\\")+1);

            ViewBag.Author = usr.Login;

            foldername = locfoldername;

            /*
            if (contains != null)
            {
                foreach (var s in Directory.GetFiles(main_path)) //перебираем по файлам
                {
                    if (System.IO.File.ReadAllText(s).Contains(contains))
                    { //считываем и проверяем, содержит ли он нашу искомую строку
                        //Console.WriteLine(s.Substring(s.LastIndexOf("\\") + 1));
                        diploma.Models.File new_file = _context.Files.Where(n => n.Name.Equals(s.Substring(s.LastIndexOf("\\") + 1))).FirstOrDefault();

                        result.Add(new_file);
                    }
                }
            }
            */

            // Вывести все файлы системы
            if (contains == null && name == null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))) // если нет названия и даты
            {
                result = _context.Files.Where(n => n.Author.Equals(usr) && n.Path.Contains(foldername)).ToList();
            }
            else if (contains == null && name != null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))) // если введено имя и не введена дата
            {
                result = _context.Files.Where(n => n.Author.Equals(usr) && n.Name.Contains(name) && n.Path.Contains(foldername)).ToList();
            }
            else if (contains == null && name != null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))) // если введено имя и введена дата
            {
                result = _context.Files.Where(n => n.Author.Equals(usr) && n.Name.Contains(name) && n.Date.Date == seldate.Date && n.Path.Contains(foldername)).ToList();
            }
            else if (contains == null && name == null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))) // если нет названия и нет даты
            {
                result = _context.Files.Where(n => n.Author.Equals(usr) && n.Date.Date == seldate.Date && n.Path.Contains(foldername)).ToList();
            }

            if (contains != null && name == null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))) // если нет названия и даты
            {
                foreach (var s in Directory.GetFiles(main_path)) //перебираем по файлам
                {
                    if (System.IO.File.ReadAllText(s).Contains(contains.ToLower()) || System.IO.File.ReadAllText(s).Contains(contains.ToUpper()))
                    { //считываем и проверяем, содержит ли он нашу искомую строку
                        //Console.WriteLine(s.Substring(s.LastIndexOf("\\") + 1));
                        diploma.Models.File new_file = _context.Files.Where(n => n.Name.Equals(s.Substring(s.LastIndexOf("\\") + 1))).FirstOrDefault();

                        if (!result.Any(n => n.Name == new_file.Name)) { result.Add(new_file); }

                    }
                }
            }
            else if (contains != null && name != null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))) // если введено имя и не введена дата и введено содержимое
            {
                //result = _context.Files.Where(n => n.Author.Equals(usr) && n.Name.Contains(name) && n.Path.Contains(foldername)).ToList();

                foreach (var s in Directory.GetFiles(main_path)) //перебираем по файлам
                {

                    if ((System.IO.File.ReadAllText(s).Contains(contains.ToLower()) || System.IO.File.ReadAllText(s).Contains(contains.ToUpper())) && (s.Substring(s.LastIndexOf("\\") + 1)).Contains(name))
                    { //считываем и проверяем, содержит ли он нашу искомую строку
                        //Console.WriteLine(s.Substring(s.LastIndexOf("\\") + 1));
                        diploma.Models.File new_file = _context.Files.Where(n => n.Name.Equals(s.Substring(s.LastIndexOf("\\") + 1))).FirstOrDefault();

                        result.Add(new_file); 

                    }

                }
            }
            else if (contains != null && name != null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))) // если введено имя и введена дата и введено содержимое (???)
            {
                result = _context.Files.Where(n => n.Author.Equals(usr) && n.Name.Contains(name) && n.Date.Date == seldate.Date && n.Path.Contains(foldername)).ToList();

                foreach (var s in Directory.GetFiles(main_path)) //перебираем по файлам
                {
                    if (System.IO.File.ReadAllText(s).Contains(contains.ToLower()) || System.IO.File.ReadAllText(s).Contains(contains.ToUpper()))
                    { //считываем и проверяем, содержит ли он нашу искомую строку
                        //Console.WriteLine(s.Substring(s.LastIndexOf("\\") + 1));
                        diploma.Models.File new_file = _context.Files.Where(n => n.Name.Equals(s.Substring(s.LastIndexOf("\\") + 1))).FirstOrDefault();


                    }
                }
            }
            else if (contains != null && name == null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))) // если нет названия и есть дата но есть содержимое
            {
                List<diploma.Models.File> result2 = new List<diploma.Models.File>();
                List<diploma.Models.File> result3 = new List<diploma.Models.File>();
                result3 = _context.Files.Where(n => n.Author.Equals(usr) && n.Date.Date == seldate.Date && n.Path.Contains(foldername)).ToList();

                foreach (var s in Directory.GetFiles(main_path)) //перебираем по файлам
                {
                    if (System.IO.File.ReadAllText(s).Contains(contains.ToLower()) || System.IO.File.ReadAllText(s).Contains(contains.ToUpper()))
                    { //считываем и проверяем, содержит ли он нашу искомую строку
                        //Console.WriteLine(s.Substring(s.LastIndexOf("\\") + 1));
                        diploma.Models.File new_file = _context.Files.Where(n => n.Name.Equals(s.Substring(s.LastIndexOf("\\") + 1))).FirstOrDefault();

                        result2.Add(new_file);
                    }
                }

                var res = result3.Intersect(result2);

                foreach (var t in res){result.Add(t);}


            }

            ViewBag.FoldName =  main_path.Substring(main_path.LastIndexOf("\\") + 1, main_path.LastIndexOf("_") - main_path.LastIndexOf("\\") - 1);

            return View(result);

        }

        public IActionResult ShowFolders(string name, DateTime seldate)
        {

            // проверяем наличие ошибок при удалении
            if (TempData["check"] != null)
            {
                ViewBag.Check = TempData["check"].ToString();
            }

            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            string[] allfolders = Directory.GetDirectories(path);
            List<string> result = new List<string>(); ;

            
            foreach (string t in allfolders)
            {
                string resultLetter = t;
                if (t.Contains(User.Identity.Name))
                {
                    var indexes = t.Select((c, index) => new { c, index })
                                      .Where(s => s.c == '\\')
                                      .Select(s => s.index - 1);
                    foreach (var ind in indexes)
                    {
                        resultLetter = resultLetter.Insert(ind + 1, "\\");
                    }
                    result.Add(t);
                }                
            }

            string text = "a\\d\\a\\f";
            var array = text.Select((c, index) => new { c, index })
                                      .Where(s => s.c == '\\')
                                      .Select(s => s.index + 1);


            /*
            foreach (var t in allfolders2)
            {
                Console.WriteLine(t);
            }
            */

            ViewBag.Folders = result;

            // Даты создания папок
            Dictionary<string, string> foldersDate = new Dictionary<string, string>();

            foreach(string fold in result)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(fold);

                var date = Convert.ToString(dirInfo.CreationTime);
                date = date.Substring(0, date.LastIndexOf(" "));

                foldersDate.Add(fold, date);

            }

            /*
            if (foldersDate.Any() == false)
            {
                ViewBag.Dates = "Пусто";
            }else { ViewBag.Dates = foldersDate; }
            */

            ViewBag.Dates = foldersDate;

            /* Проверка на содержание словаря
            foreach(System.Collections.Generic.KeyValuePair<string, string> i in foldersDate)
            {
                // Полная дата - i.Value
                Console.WriteLine(i.Key);
                Console.WriteLine(i.Value);
                Console.WriteLine("_______");
            }
            */

            return View(result);
        }


        public async Task<IActionResult> DeleteConfirmed(string pathh, string id)
        {
            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            bool isAdmin = false;

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;
            pathh = "wwwroot" + pathh;

            if (System.IO.File.Exists(pathh))
            {
                System.IO.File.Delete(pathh);
                diploma.Models.File file = await _context.Files.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(id));

                if (file != null)
                {
                    _context.Files.Remove(file);

                    await _context.SaveChangesAsync();

                    isAdmin = User.Identity.Name.Equals("admin") ? true : false;

                    Console.WriteLine(isAdmin);

                    if (!isAdmin) { return RedirectToAction("FoldersFiles", new { @foldpath = pathh.Substring(0, pathh.LastIndexOf("\\")) }); }
                    else { return RedirectToAction("Index", "File"); }
                }

                if (isAdmin) {
                    return RedirectToAction("Index");
                }
                else{    return RedirectToAction("FoldersFiles", new { @foldpath = pathh.Substring(0, pathh.LastIndexOf("\\")) });}

            }
            if (isAdmin)
            {
                return RedirectToAction("Index");
            }
            else { return RedirectToAction("FoldersFiles", new { @foldpath = pathh.Substring(0, pathh.LastIndexOf("\\")) }); }


        }

        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            string PathToFolder = @"\files\" + foldername + @"\";
            // string PathToFolder = "C:\\Users\\l.khusainova\\source\\repos\\diploma\\diploma\\wwwroot\\files\\" + foldername + "\\";

            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;
            
            if (uploadedFile != null)
            {
                string ext = Path.GetExtension(uploadedFile.FileName); // расширение файла

                // путь к папке Files
                string path = "/files/" + foldername + "/" + uploadedFile.FileName.Substring(0, uploadedFile.FileName.IndexOf(ext)) + "_" + User.Identity.Name + ext;                // сохраняем файл в папку Files в каталоге 


                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                diploma.Models.File file = new diploma.Models.File { Name = uploadedFile.FileName.Substring(0, uploadedFile.FileName.IndexOf(ext)) + "_" + User.Identity.Name + ext, Path = PathToFolder + uploadedFile.FileName.Substring(0, uploadedFile.FileName.IndexOf(ext)) + "_" + User.Identity.Name + ext, Author = _context.Users.Where(n => n.Login == User.Identity.Name).FirstOrDefault(), Date = DateTime.Now };

                //var _check = _context.Files.Where(n => n.Name == file.Name);

                //if (!_check.Any())
                //{
                    _context.Files.Add(file);
                    _context.SaveChanges();
                //}
            }
          
            return RedirectToAction("FoldersFiles", new { @foldpath = path + foldername });
        }

        public IActionResult View(string fileToView, string pathh)
        {
            pathh = "wwwroot" + pathh;
            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            string fileName = fileToView;
            string outputDirectory = ("Output/");
            string outputFilePath = Path.Combine(outputDirectory, "output.pdf");

            Console.WriteLine(outputFilePath);

            int i = 0, count = 0;
            while ((i = pathh.IndexOf("\\", i)) != -1) { ++count; i += "\\".Length; }

            if (count > 8)
            {
                using (Viewer viewer = new Viewer(pathh))
                {
                    PdfViewOptions options = new PdfViewOptions(outputFilePath);
                    //HtmlViewOptions options = HtmlViewOptions.ForEmbeddedResources(outputFilePath);
                    viewer.View(options);
                }

                var fileStream = new FileStream("Output/" + "output.pdf",
                    FileMode.Open,
                    FileAccess.Read
                    );
                var fsResult = new FileStreamResult(fileStream, "application/pdf");

                return fsResult;

            }
            else
            {
                // using (Viewer viewer = new Viewer(path + fileName))
                using (Viewer viewer = new Viewer(pathh))
                {
                    PdfViewOptions options = new PdfViewOptions(outputFilePath);
                    //HtmlViewOptions options = HtmlViewOptions.ForEmbeddedResources(outputFilePath);
                    viewer.View(options);
                }

                var fileStream = new FileStream("Output/" + "output.pdf",
                    FileMode.Open,
                    FileAccess.Read
                    );
                var fsResult = new FileStreamResult(fileStream, "application/pdf");

                return fsResult;

            }
            
            return View();

        }
        
        /*
        public async Task<IActionResult> EditFile(int? id, string pathh)
        {
            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            if (id != null)
            {
                diploma.Models.File file = await _context.Files.FirstOrDefaultAsync(p => p.Id == id);

                // проверка, файл лежит в другой папке или общей
                int i = 0, count = 0;
                while ((i = pathh.IndexOf("\\", i)) != -1) { ++count; i += "\\".Length; }

                if ( count > 8 )
                {
                    fullPath = pathh;
                    filename = file.Name.Substring(0, file.Name.IndexOf('_'));
                    filetype = file.Name.Substring(file.Name.LastIndexOf('.'));
                }
                else
                {
                    fullPath = file.Path;
                    filename = file.Name.Substring(0, file.Name.IndexOf('_'));
                    filetype = file.Name.Substring(file.Name.LastIndexOf('.'));
                }

                ViewBag.FileName = filename;

                if (file != null)
                    return View(file) ;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditFile(diploma.Models.File file)
        {
            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            // извлекаем нужный файл
            _context.Files.Update(file);

            User author = _context.Users.FirstOrDefault(n => n.Id == file.AuthorId);

            file.Author = author;
            file.Name = file.Name + "_" + author.Login + filetype;

            // проверка, файл лежит в другой папке или общей
            int i = 0, count = 0;
            while ((i = file.Path.IndexOf("\\", i)) != -1) { ++count; i += "\\".Length; }

            if ( count > 8){    file.Path = fullPath.Substring(0, fullPath.LastIndexOf("\\") + 1) + file.Name;}
            else{    file.Path = path + file.Name;}

            try
            {
                System.IO.File.Move(fullPath, file.Path);

                if (!System.IO.File.Exists(file.Path))
                {
                    Console.WriteLine("File successfully renamed.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The renaming failed: {0}", e.ToString());
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("FoldersFiles", new { @foldpath = fullPath.Substring(0, fullPath.LastIndexOf("\\")) });

        }
        */

        public async Task<IActionResult> EditFile([FromBody] string parameters)
        {
            string[] data = parameters.Split("+");
            int id = Convert.ToInt32(data[0]);
            var pathh = data[1];
            var name = data[2];

            int check = 0;

            if (id != null)
            {
                diploma.Models.File file = await _context.Files.FirstOrDefaultAsync(p => p.Id == id);

                // проверка, файл лежит в другой папке или общей
                int i = 0, count = 0;
                while ((i = pathh.IndexOf("\\", i)) != -1) { ++count; i += "\\".Length; }

                if (count > 8)
                {
                    fullPath = pathh;
                    filename = file.Name.Substring(0, file.Name.IndexOf('_'));
                    filetype = file.Name.Substring(file.Name.LastIndexOf('.'));
                }
                else
                {
                    fullPath = file.Path;
                    filename = file.Name.Substring(0, file.Name.IndexOf('_'));
                    filetype = file.Name.Substring(file.Name.LastIndexOf('.'));
                }
                

                if (file != null)
                {

                    User author = _context.Users.FirstOrDefault(n => n.Id == file.AuthorId);

                    file.Author = author;
                    file.Name = name + "_" + author.Login + filetype;

                    // проверка, файл лежит в другой папке или общей
                    int j = 0, counter = 0;
                    while ((j = file.Path.IndexOf("\\", j)) != -1) { ++counter; j += "\\".Length; }

                    if (counter > 8) { file.Path = fullPath.Substring(0, fullPath.LastIndexOf("\\") + 1) + file.Name; }
                    else { file.Path = path + file.Name; }

                    try
                    {
                        System.IO.File.Move(fullPath, file.Path);

                        if (!System.IO.File.Exists(file.Path))
                        {
                            Console.WriteLine("File successfully renamed.");
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("The renaming failed: {0}", e.ToString());
                    }

                    // извлекаем нужный файл
                    _context.Files.Update(file);

                }

            }

            await _context.SaveChangesAsync();
            return RedirectToAction("FoldersFiles", new { @foldpath = fullPath.Substring(0, fullPath.LastIndexOf("\\")) });
        }



        [HttpGet]
        public IActionResult AddDir()
        {
            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            return View();
        }

        public async Task<IActionResult> AddDir([FromBody] string foldnamee)
        {          
            Directory.CreateDirectory(Path.Combine(path, foldnamee + "_" + User.Identity.Name));

            return RedirectToAction("ShowFolders");
        }

        public async Task<IActionResult> DeleteDir([FromBody] string pathh)
        {
            // pathh = "wwwroot" + pathh;
            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            if (Directory.Exists(pathh))
            {
                
                 Directory.Delete(pathh);
                
                return RedirectToAction("ShowFolders");
            }
            
            return RedirectToAction("ShowFolders");
        }

        [HttpGet]
        public IActionResult EditDir(string pathh, int t)
        {
            pathh = "wwwroot" + pathh;
            old_name = pathh;

            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            return View();
        }

        public async Task<IActionResult> EditDir([FromBody] string namee)
        {
            string[] data = namee.Split("+"); // извлекаем входящие параметры в метод

            var name = data[0]; // новое название папки
            
            old_name = data[1]; // старый путь

            // Для Layout, чтобы высвечивалось имя и фамилия в личном кабинете
            User mainUser = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name)); 
            ViewBag.Name = mainUser.Surname + " " + mainUser.Name;

            // Проверяем, находятся ли какие-нибудь файлы в папке (потому что папку нельзя удалить
            // , если в ней есть хотя бы один файл)
            if (System.IO.Directory.GetDirectories(old_name).Length + System.IO.Directory.GetFiles(old_name).Length > 0)
            {
                // Папка не пуста (нужно поменять пути файлов в папке)
                // Сначала меняем пути файлов в папке на новые, только потом меняем название самой папки
                // потому что, если сначала поменять имя, то такой старой папки существовать уже не будет

                // Сохраняем файлы, которые хотим поменять 
                var from = name + "_" + User.Identity.Name; // старое название папки (sa_i.ivanov)
                var to = old_name.Substring(old_name.LastIndexOf("\\") + 1); // новое название папки (df_i.ivanov)

                string[] allfiles = Directory.GetFiles(old_name);

                foreach (string filename in allfiles)
                {
                    var tempPath = filename.Replace(to, from);

                    diploma.Models.File tempFile = _context.Files.FirstOrDefault(n => n.Path.Equals(filename));

                    tempFile.Path = tempPath;

                    _context.Files.Update(tempFile);
                    await _context.SaveChangesAsync();

                }

                // Меняем название самой папки             
                try
                {
                    Directory.Move(old_name, old_name.Substring(0, old_name.LastIndexOf("\\") + 1) + name + "_" + User.Identity.Name);

                    if (!System.IO.File.Exists(old_name.Substring(0, old_name.LastIndexOf("\\") + 1) + name + "_" + User.Identity.Name))
                    {
                        Console.WriteLine("File successfully renamed.");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("The renaming failed: {0}", e.ToString());
                }                

            }
            else
            {
                // Папка пуста (пути файлов менять не надо, поэтому просто меняем название папки)                
                try
                {
                    Directory.Move(old_name, old_name.Substring(0, old_name.LastIndexOf("\\") + 1) + name + "_" + User.Identity.Name);

                    if (!System.IO.File.Exists(old_name.Substring(0, old_name.LastIndexOf("\\") + 1) + name + "_" + User.Identity.Name))
                    {
                        Console.WriteLine("File successfully renamed.");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("The renaming failed: {0}", e.ToString());
                }
                
            }


            return RedirectToAction("ShowFolders");
        }


    }
}
