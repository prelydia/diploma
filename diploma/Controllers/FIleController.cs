using diploma.Models;
using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using diploma.ViewModels;

namespace diploma.Controllers
{
    public class FIleController : Controller
    {
        private ApplicationContext _context;

        private string filename;
        private string path;
        public static string fullPath;
        public static string filetype;
        private readonly IWebHostEnvironment _appEnvironment;

        public FIleController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            filename = null;
            path = @"wwwroot\files\";
            // path = @"C:\Users\l.khusainova\source\repos\diploma\diploma\wwwroot\files\";
            SelectListItem test = new SelectListItem();

        }
        public IActionResult Index(int author, DateTime seldate, string name, int page = 1)
        {

            /*
            if (name == null) { name = ""; }

            if (seldate.Date.Equals(Convert.ToDateTime("01.01.0001"))){    seldate = DateTime.Now;}

            */

            var UsersList = (from user in _context.Users
                             where user.Login != "admin"
                             select new SelectListItem()
                             {
                                 Text = user.Login,
                                 Value = user.Id.ToString(),
                             }).ToList();

            ViewBag.T = UsersList; // вывести список пользователей системы (для варианта фильтрации)

            IQueryable<diploma.Models.File> source = _context.Files.Include(n => n.Author); // Все файлы системы

            
            List<diploma.Models.File> result = new List<diploma.Models.File>();

            // Вывести все файлы системы
            if (name == null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001")) && (author == 0 || author == 10012))
            {
                source = source;
            }

            // Если ввели только название файла
            else if (name != null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001")) && author == 10012)
            {
                source = source.Where(n => n.Name.Contains(name));//_context.Files.Where(n => n.Name.Contains(name)).ToList();
            }

            // Если ввели только дату создания файлов
            else if (name == null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001")) && author == 10012)
            {
                source = source.Where(n => n.Date.Date == seldate.Date);//_context.Files.Where(n => n.Date.Date == seldate.Date).ToList();
            }

            // Если ввели название файла и автора
            else if (name != null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001")) && author != 10012)
            {
                source = source.Where(n => n.AuthorId.ToString().Contains(author.ToString()) && n.Name.Contains(name));//_context.Files.Where(n => n.AuthorId.ToString().Contains(author.ToString()) && n.Name.Contains(name)).ToList();
            }

            // Если ввели только автора
            else if (name == null && seldate.Date.Equals(Convert.ToDateTime("01.01.0001")) && author != 10012)
            {
                source = source.Where(n => n.Author.Id == author);//_context.Files.Where(n => n.Author.Id == author).ToList();
            }

            // Если ввели только название и дату создания файла
            else if (name != null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001")) && author == 10012)
            {
                source = source.Where(n => n.Date.Date == seldate.Date && n.Name.Contains(name));//_context.Files.Where(n => n.Date.Date == seldate.Date && n.Name.Contains(name)).ToList();
            }

            // Если ввели только автора и дату создания
            else if (name == null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001")) && author != 10012 )
            {
                source = source.Where(n => n.Date.Date == seldate.Date && n.Author.Id == author);//_context.Files.Where(n => n.Date.Date == seldate.Date && n.Author.Id == author).ToList();
            }

            // Если все поля заполнены
            else if (name != null && !seldate.Date.Equals(Convert.ToDateTime("01.01.0001")) && author != 10012)
            {
                source = source.Where(n => n.Date.Date == seldate.Date && n.Author.Id == author && n.Name.Contains(name));//_context.Files.Where(n => n.Date.Date == seldate.Date && n.Author.Id == author && n.Name.Contains(name)).ToList();
            }
            

            // Вариант с пагинацией
            int pageSize = 10;

            var count =  source.Count();
            var items = source.OrderBy(n => n.Date).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ShowViewModel viewModel = new ShowViewModel {
                PageViewModel = pageViewModel,
                Files = items
            };

            return View(viewModel);
        }

        public IActionResult ViewDoc(string fileToView)
        {    
            /*
            string fileName = fileToView;
            string outputDirectory = ("Output/");
            string outputFilePath = Path.Combine(outputDirectory, "output.pdf");

            using (Viewer viewer = new Viewer("SourceDocument/" + fileName))
            {
                PdfViewOptions options = new PdfViewOptions(outputFilePath);
                viewer.View(options);
            }
            
            var fileStream = new FileStream("Output/" + "output.pdf",
                FileMode.Open,
                FileAccess.Read
                );

            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            */
            return View();

        }

        public async Task<IActionResult> Edit(int? id, string pathh)
        {
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

                ViewBag.Name = filename;

                if (file != null)
                    return View(file);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(diploma.Models.File file)
        {
            // извлекаем нужный файл
            _context.Files.Update(file);

            User author = _context.Users.FirstOrDefault(n => n.Id == file.AuthorId);

            file.Author = author;
            file.Name = file.Name + "_" + author.Login + filetype;

            // проверка, файл лежит в другой папке или общей
            int i = 0, count = 0;
            while ((i = file.Path.IndexOf("\\", i)) != -1) { ++count; i += "\\".Length; }

            if (count > 8) { file.Path = fullPath.Substring(0, fullPath.LastIndexOf("\\") + 1) + file.Name; }
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


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult CDoc(string fileToView)
        {
            string fileName = fileToView;
            string outputDirectory = ("Output/");
            string outputFilePath = Path.Combine(outputDirectory, "output.pdf");

            using (Viewer viewer = new Viewer("SourceDocuments/" + fileName))
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

        public IActionResult View(string fileToView, string pathh)
        {

            string fileName = fileToView;
            string outputDirectory = ("Output/");
            string outputFilePath = Path.Combine(outputDirectory, "output.pdf");

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
                using (Viewer viewer = new Viewer(path + fileName))
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

        public IActionResult Download(string filePath, string fileName)
        {
            // var webroot = ""; // this._appEnvironment.WebRootPath;
            var webroot = this._appEnvironment.ContentRootPath;
            Console.WriteLine(filePath);
            /*
            if (fileName == "output.zip") { webroot = this._appEnvironment.ContentRootPath; } // для метода 1004
            else { webroot = this._appEnvironment.WebRootPath; }*/

            string _path = Path.Combine(webroot + "\\" + filePath);

            byte[] mas = System.IO.File.ReadAllBytes(_path);
            string file_type = "application/force-download";
            string fileToView = fileName;

            return File(mas, file_type, fileToView);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string pathh, string id)
        {
            if (System.IO.File.Exists(pathh))
            {
                System.IO.File.Delete(pathh);
                diploma.Models.File file = await _context.Files.FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(id));
                if (file != null)
                {
                    _context.Files.Remove(file);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Не удалось удалить!");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Метод, возвращающий расширение выбранного файла
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string Extension(string path, string fileName)
        {
            string[] exts = { ".jpeg", ".svg", ".png", ".eps", ".pdf"} ; // массив с реализованными расширениями
            string result = ".svg";

            foreach(var i in exts)
            {
                if (path.Contains(i)) { result = i; }
            }

            return result;
        }

        public IActionResult Move([FromBody] string path)
        {

            // разделяем входящие параметры на:
            // 1. папку, куда мы переносим файлы splitted[0]
            // 2. путь файла, которого мы переносим splitted[1]


            /*
            var newFilePath = splitted[1].Replace("//", "\\").Replace(Environment.NewLine, ""); // меняем слеши на обратные в пути файла

            var newFileName = newFilePath.Substring(newFilePath.LastIndexOf("\\") + 1); // что переносим

            string destinationFile = splitted[0] + '\\' + newFileName.Replace(".png", "") + "_" + User.Identity.Name + ".png"; // КУДА ПЕРЕНОСИМ
            string sourceFile = newFilePath; // ЧТО ПЕРЕНОСИМ*/

            string[] splitted = path.Split("+");

            // в случае, если скачиваются несколько файлов
            if (splitted.Length > 2)
            {
                for (int i = 1; i < splitted.Length; i++)
                {
                    var newFilePath = splitted[i].Trim().Replace("//", "\\").Replace(Environment.NewLine, ""); // меняем слеши на обратные в пути файла

                    var newFileName = newFilePath.Substring(newFilePath.LastIndexOf("\\") + 1); // что переносим

                    string extension = Extension(newFilePath, newFileName); // находим выбранное расширение файла

                    string destinationFile = splitted[0] + '\\' + newFileName.Replace(extension, "") + "_" + User.Identity.Name + extension; // КУДА ПЕРЕНОСИМ
                    string sourceFile = newFilePath.Trim(); // ЧТО ПЕРЕНОСИМ         

                    // проверка если пользователь нажал второй раз сохранить в папке            
                    FileInfo check = new FileInfo(sourceFile);

                    Console.WriteLine(check);

                    if (check.Exists)
                    {
                        //// создаем экземплял файл в БД
                        User aut = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

                        // создание в БД
                        diploma.Models.File newFile = new diploma.Models.File
                        {
                            Name = newFileName.Replace(extension, "") + "_" + User.Identity.Name + extension,
                            Author = aut,
                            AuthorId = aut.Id,
                            Date = DateTime.Now,
                            Path = destinationFile
                        };

                        // проверка на существование файла с таким же названием 
                        var tempFile = _context.Files.Where(n => n.Name.Equals(newFile.Name));

                        if (newFile != null && tempFile == null)
                        {
                            _context.Files.Add(newFile);
                            _context.SaveChanges();

                            // Перенос файла в новую папку:
                            System.IO.File.Move(sourceFile, destinationFile);
                        }
                        else
                        {
                            // меняем название картинки
                            string iPass = "";
                            string[] arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y" };
                            Random rnd = new Random();
                            for (int j = 0; j < 5; j = j + 1)
                            {
                                iPass = iPass + arr[rnd.Next(0, 57)];
                            }

                            // в соответствии с новым названием меняем новый путь картинки
                            // destinationFile = splitted[0] + '\\' + newFileName.Replace(extension, "") + iPass + "_" + User.Identity.Name + extension;
                            destinationFile = this._appEnvironment.WebRootPath + splitted[0] + '\\' + newFileName.Replace(extension, "") + iPass + "_" + User.Identity.Name + extension;
                            // присваиваем новое название и путь
                            newFile.Name = newFileName.Replace(extension, "") + iPass + "_" + User.Identity.Name + extension;
                            newFile.Path = splitted[0] + '\\' + newFileName.Replace(extension, "") + iPass + "_" + User.Identity.Name + extension;

                            // добавляем новые данные в БД
                            _context.Files.Add(newFile);
                            _context.SaveChanges();

                            // Перенос файла в новую папку:
                            System.IO.File.Move(sourceFile, destinationFile);
                        }
                    }
                    else
                    {
                        return Json("error");
                    }
                }
            }
            // если переносим одну картинку (length == 2 with )
            else if (splitted.Length == 2)
            {
                var newFilePath = splitted[1].Trim().Replace("//", "\\").Replace(Environment.NewLine, ""); // меняем слеши на обратные в пути файла

                var newFileName = newFilePath.Substring(newFilePath.LastIndexOf("\\") + 1); // что переносим

                string extension = Extension(newFilePath, newFileName); // находим выбранное расширение файла

                string destinationFile = splitted[0] + '\\' + newFileName.Replace(extension, "") + "_" + User.Identity.Name + extension; // КУДА ПЕРЕНОСИМ
                string sourceFile = newFilePath.Trim(); // ЧТО ПЕРЕНОСИМ         

                // проверка если пользователь нажал второй раз сохранить в папке            
                FileInfo check = new FileInfo(sourceFile);

                if (check.Exists)
                {
                    //// создаем экземплял файл в БД
                    User aut = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

                    // создание в БД
                    diploma.Models.File newFile = new diploma.Models.File
                    {
                        Name = newFileName.Replace(extension, "") + "_" + User.Identity.Name + extension,
                        Author = aut,
                        AuthorId = aut.Id,
                        Date = DateTime.Now,
                        Path = destinationFile
                    };

                    // проверка на существование файла с таким же названием 
                    var tempFile = _context.Files.Where(n => n.Name.Equals(newFile.Name));

                    if (newFile != null && tempFile == null)
                    {
                        _context.Files.Add(newFile);
                        _context.SaveChanges();

                        // Перенос файла в новую папку:
                        System.IO.File.Move(sourceFile, destinationFile);
                    }
                    else
                    {
                        // меняем название картинки
                        string iPass = "";
                        string[] arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y" };
                        Random rnd = new Random();
                        for (int j = 0; j < 5; j = j + 1)
                        {
                            iPass = iPass + arr[rnd.Next(0, 57)];
                        }

                        // в соответствии с новым названием меняем новый путь картинки
                        // destinationFile = splitted[0] + '\\' + newFileName.Replace(extension, "") + iPass + "_" + User.Identity.Name + extension;
                        destinationFile = this._appEnvironment.WebRootPath + splitted[0] + '\\' + newFileName.Replace(extension, "") + iPass + "_" + User.Identity.Name + extension;
                        // присваиваем новое название и путь
                        newFile.Name = newFileName.Replace(extension, "") + iPass + "_" + User.Identity.Name + extension;
                        newFile.Path = splitted[0] + '\\' + newFileName.Replace(extension, "") + iPass + "_" + User.Identity.Name + extension;

                        // добавляем новые данные в БД
                        _context.Files.Add(newFile);
                        _context.SaveChanges();

                        // Перенос файла в новую папку:
                        System.IO.File.Move(sourceFile, destinationFile);
                    }
                }
                else
                {
                    return Json("error");
                }
            }

            return Json(path);
        }

        public IActionResult Write([FromBody] string path)
        {
            /*
            StreamWriter f = new StreamWriter(_appEnvironment.ContentRootPath + "particlesData.txt");
            f.WriteLine(path);
            f.Close();*/

            Console.WriteLine(_appEnvironment.ContentRootPath + "MSD.txt");

            return Json(path);
        }

    }
}
