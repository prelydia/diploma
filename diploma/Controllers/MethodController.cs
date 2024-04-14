using diploma.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ionic.Zip;
using Aspose.Zip;
using Aspose.Zip.SevenZip;
using Aspose.Zip.Rar;
using Aspose.Zip.Saving;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace diploma.Controllers
{
    public class MethodController : Controller
    {
        private ApplicationContext _context;
        public int method_id;
        private IWebHostEnvironment Environment;

        public MethodController(ApplicationContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }

        public string GetCulture(string code = "")
        {
            if (!String.IsNullOrEmpty(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);
            }

            //return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
            return CultureInfo.CurrentCulture.Name;
        }

        public IActionResult Index()
        {

            User user = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));

            ViewBag.FullPath = this.Environment.WebRootPath.Replace("\\", "\\\\"); // папка со статической папкой wwwroot (меняем слеши // на //// для отображения в представлении)

            // FOR LOCALIZATION
            if (GetCulture() == "ru")
            {
                var MethodsList = (from role in _context.Methods
                                   select new SelectListItem()
                                   {
                                       Text = role.Name,
                                       Value = role.Id.ToString(),
                                   }).ToList();

                ViewBag.Methods = MethodsList;
            }else
            {
                var MethodsList = (from role in _context.MethodsENG
                                   select new SelectListItem()
                                   {
                                       Text = role.Name,
                                       Value = role.Id.ToString(),
                                   }).ToList();

                ViewBag.Methods = MethodsList;
            }

            ViewBag.Name =  user.Surname + " " + user.Name;
            ViewBag.User = user.Id;

            IEnumerable<string> allfolders = Directory.EnumerateDirectories(@"wwwroot\files").Where(n => n.Contains(User.Identity.Name));

            ViewBag.Dirs = allfolders;

            Dictionary<string, List<string>> files = new Dictionary<string, List<string>>(); // словарь файлов в папках
            
            foreach (string fold in allfolders)
            {
                IEnumerable<string> t = Directory.EnumerateFiles(fold);
                var foldd = fold.Substring(fold.LastIndexOf("\\") + 1 , (fold.LastIndexOf("_") - fold.LastIndexOf("\\") - 1) ) ;
                List<string> fileslist = new List<string>();
                foreach(string n in t)
                {
                    fileslist.Add(n);               
                }
                files.Add(foldd, fileslist);

            }

            // Папки пользователя
            IEnumerable<string> allfoldersTest = Directory.EnumerateDirectories(@"wwwroot\files").Where(n => n.Contains(User.Identity.Name));

            ViewBag.Count = allfoldersTest.Count();

            ViewBag.Files = files;

            return View();
        }
        /// <summary>
        /// Для консольных выводов будет 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(string helper)
        {

            User user = _context.Users.FirstOrDefault(n => n.Login.Equals(User.Identity.Name));
            int id = user.Id;

            ViewBag.Name = user.Surname + " " + user.Name;

            // Методы системы
            var MethodsList = (from role in _context.Methods
                               select new SelectListItem()
                               {
                                   Text = role.Name,
                                   Value = role.Id.ToString(),
                               }).ToList();

            ViewBag.Methods = MethodsList;

            // Папки пользователя
            IEnumerable<string> allfolders = Directory.EnumerateDirectories(@"wwwroot\files").Where(n => n.Contains(User.Identity.Name));
            ViewBag.Dirs = allfolders;


            Dictionary<string, List<string>> files = new Dictionary<string, List<string>>(); // словарь файлов в папках


            // Файлы пользователя
            foreach (string fold in allfolders)
            {
                IEnumerable<string> t = Directory.EnumerateFiles(fold);
                var foldd = fold.Substring(fold.LastIndexOf("\\") + 1, (fold.LastIndexOf("_") - fold.LastIndexOf("\\") - 1));
                List<string> fileslist = new List<string>();
                foreach (string n in t)
                {
                    fileslist.Add(n);
                }
                files.Add(foldd, fileslist);

            }

            ViewBag.Files = files;

            /*
            MemoryStream memStream = new MemoryStream();
            using Process process = Process.Start(new ProcessStartInfo
            {
                FileName = "python",
                Arguments = "C:\\Users\\l.khusainova\\Desktop\\script.py",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            });

            using BinaryReader reader = new BinaryReader(process.StandardOutput.BaseStream);

            var tt = new string(reader.ReadChars(1000000000));

            ViewBag.Result = tt;

            process.Kill();
            */

            return View();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            return NotFound();
        }

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        /// <summary>
        /// Метод считывания выбранного пользователем файла
        /// output: File.Path
        /// </summary>
        /// <param name="tt"></param>
        /// <returns></returns>
        public async Task<IActionResult> Test([FromBody] string inputFile)
        {

            string filename = inputFile.Substring(inputFile.LastIndexOf("\\") + 1);

            var dbFile = _context.Files.FirstOrDefault(n => n.Name.Equals(filename)); // ищем выбранный файл из БД

            string path = Environment.WebRootPath + dbFile.Path;

            string text = "";

            // асинхронное чтение из файла 
            using (StreamReader reader = new StreamReader(path))
            {
                text = await reader.ReadToEndAsync();
            }

            return Json( new { TT = path });
        }

        public ActionResult MethodsScript([FromBody] string parameters)
        {
            string[] splitted = parameters.Split("-");
            dynamic result = null;

            // ГЕНЕРАЦИЯ ЧАСТИЦ ДЛЯ МЕТОДА BOP
            if (splitted[0] == "particlesGenerate")
            {
                Console.WriteLine("you're here");
                var webroot = this.Environment.WebRootPath;

                var psi = new ProcessStartInfo();
                psi.FileName = @"python.exe";

                // в случае неупорядоченной генерации частиц 
                if (splitted[1] == "chaos")
                {
                    // ???????????????
                    var scriptt = @"wwwroot\methods\generateChaos.py";

                    psi.Arguments = $"\"{scriptt}\" \"{webroot}\" \"{splitted[2]}\" \"{splitted[3]}\" \"{splitted[4]}\"";
                }
                // в случае упорядоченной генерации
                else if (splitted[1] == "order")
                {
                    var scriptt = @"wwwroot\methods\generateOrder.py";
                    psi.Arguments = $"\"{scriptt}\" \"{webroot}\" \"{splitted[2]}\" \"{splitted[3]}\" \"{splitted[4]}\"";
                }
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;

                var errors = "";
                //var res = ""; // название файла
                using (var process = Process.Start(psi))
                {
                    // errors = process.StandardError.ReadToEnd();
                    result = process.StandardOutput.ReadToEnd();
                }

            }
            // ЕСЛИ МЕТОД ПОИСКА ОРИЕНТАЦИОННОГО ПОРЯДКА СВЯЗИ
            else if (splitted[0] == "1004")
            {
                var webroot = this.Environment.WebRootPath;

                var psi = new ProcessStartInfo();
                psi.FileName = @"python.exe";

                var scriptt = @"wwwroot\methods\IS_system.py";

                var path = webroot + "\\" + splitted[4] + "\\" + splitted[3];

                psi.Arguments = $"\"{scriptt}\" \"{webroot}\" \"{splitted[1]}\" \"{splitted[2]}\" \"{splitted[3]}\" \"{splitted[4]}\"";

                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;


                using (var process = Process.Start(psi))
                {
                    // errors = process.StandardError.ReadToEnd();
                    result = process.StandardOutput.ReadToEnd();
                }

                // Создать FileStream для выходного ZIP-архива
                using (FileStream zipFile = System.IO.File.Open("output.zip", FileMode.Create))
                {
                    // Файл для добавления в архив
                    using (FileStream source1 = System.IO.File.Open(webroot + "\\BOPcoordinates\\heatmapData.txt", FileMode.Open, FileAccess.Read))
                    {

                        using (FileStream source2 = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
                        {

                            using (var archive = new Archive())
                            {
                                // Добавить файлы в архив
                                archive.CreateEntry("heatmapData.txt", source1);
                                archive.CreateEntry(splitted[3], source2);

                                // Заархивируйте файлы
                                archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII, ArchiveComment = "three files are compressed in this archive" });
                            }
                        }
                    }
                }
            }
            else if (splitted[0] == "2003")
            {
                var webroot = this.Environment.WebRootPath;

                var psi = new ProcessStartInfo();
                psi.FileName = @"python.exe";

                var scriptt = @"wwwroot\methods\radialUniformity.py";

                var path = webroot + "\\" + splitted[4] + "\\" + splitted[3];

                psi.Arguments = $"\"{scriptt}\" \"{webroot}\" \"{splitted[1]}\" \"{splitted[2]}\" \"{splitted[3]}\" \"{splitted[4]}\"";

                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;


                using (var process = Process.Start(psi))
                {
                    // errors = process.StandardError.ReadToEnd();
                    result = process.StandardOutput.ReadToEnd();
                }

                // Создать FileStream для выходного ZIP-архива
                using (FileStream zipFile = System.IO.File.Open("output.zip", FileMode.Create))
                {
                    // Файл для добавления в архив
                    using (FileStream source1 = System.IO.File.Open(webroot + "\\RUncoordinates\\heatmapData.txt", FileMode.Open, FileAccess.Read))
                    {

                        using (FileStream source2 = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
                        {

                            using (var archive = new Archive())
                            {
                                // Добавить файлы в архив
                                archive.CreateEntry("heatmapData.txt", source1);
                                archive.CreateEntry(splitted[3], source2);

                                // Заархивируйте файлы
                                archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII, ArchiveComment = "three files are compressed in this archive" });
                            }
                        }
                    }
                }
            }
            else if (splitted[0] == "3003")
            {
                var webroot = this.Environment.WebRootPath;

                var psi = new ProcessStartInfo();
                psi.FileName = @"python.exe";

                var scriptt = @"wwwroot\methods\lengthDeviation.py";

                var path = webroot + "\\" + splitted[4] + "\\" + splitted[3];

                psi.Arguments = $"\"{scriptt}\" \"{webroot}\" \"{splitted[1]}\" \"{splitted[2]}\" \"{splitted[3]}\" \"{splitted[4]}\"";

                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;


                using (var process = Process.Start(psi))
                {
                    // errors = process.StandardError.ReadToEnd();
                    result = process.StandardOutput.ReadToEnd();
                }

                // Создать FileStream для выходного ZIP-архива
                using (FileStream zipFile = System.IO.File.Open("output.zip", FileMode.Create))
                {
                    // Файл для добавления в архив
                    using (FileStream source1 = System.IO.File.Open(webroot + "\\LDcoordinates\\heatmapData.txt", FileMode.Open, FileAccess.Read))
                    {

                        using (FileStream source2 = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
                        {

                            using (var archive = new Archive())
                            {
                                // Добавить файлы в архив
                                archive.CreateEntry("heatmapData.txt", source1);
                                archive.CreateEntry(splitted[3], source2);

                                // Заархивируйте файлы
                                archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII, ArchiveComment = "three files are compressed in this archive" });
                            }
                        }
                    }
                }
            }
            else if (splitted[0] == "4003")
            {
                var webroot = this.Environment.WebRootPath;

                var psi = new ProcessStartInfo();
                psi.FileName = @"python.exe";

                var scriptt = @"wwwroot\methods\shapeFactor.py";

                var path = webroot + "\\" + splitted[4] + "\\" + splitted[3];

                psi.Arguments = $"\"{scriptt}\" \"{webroot}\" \"{splitted[1]}\" \"{splitted[2]}\" \"{splitted[3]}\" \"{splitted[4]}\"";

                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;


                using (var process = Process.Start(psi))
                {
                    // errors = process.StandardError.ReadToEnd();
                    result = process.StandardOutput.ReadToEnd();
                }

                // Создать FileStream для выходного ZIP-архива
                using (FileStream zipFile = System.IO.File.Open("output.zip", FileMode.Create))
                {
                    // Файл для добавления в архив
                    using (FileStream source1 = System.IO.File.Open(webroot + "\\SFcoordinates\\heatmapData.txt", FileMode.Open, FileAccess.Read))
                    {

                        using (FileStream source2 = System.IO.File.Open(path, FileMode.Open, FileAccess.Read))
                        {

                            using (var archive = new Archive())
                            {
                                // Добавить файлы в архив
                                archive.CreateEntry("heatmapData.txt", source1);
                                archive.CreateEntry(splitted[3], source2);

                                // Заархивируйте файлы
                                archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII, ArchiveComment = "three files are compressed in this archive" });
                            }
                        }
                    }
                }
            }
            else if (splitted[0] == "5003")
            {
                var cfg = splitted[2];
                var coords = splitted[1];
                var webroot = this.Environment.WebRootPath;

                var psi = new ProcessStartInfo();
                psi.FileName = @"python.exe";

                // ???????????????
                var scriptt = @"wwwroot\methods\voronoiDiagram.py";

                psi.Arguments = $"\"{scriptt}\" \"{webroot}\" \"{cfg}\" \"{coords}\"";

                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;

                var errors = "";
                //var res = ""; // название файла

                using (var process = Process.Start(psi))
                {
                    // errors = process.StandardError.ReadToEnd();
                    result = process.StandardOutput.ReadToEnd();
                }
            }
            else if (splitted[0] == "6003")
            {
                var filePath = splitted[1]; //"C:\\Users\\l.khusainova\\Desktop\\new_help.jpg";

                var psi = new ProcessStartInfo();
                psi.FileName = @"python.exe";

                //var scriptt = @"C:\Users\l.khusainova\Desktop\ringdetposled2_03_21.py";
                var scriptt = @"wwwroot\methods\ringdetposled2_03_21.py";

                psi.Arguments = $"\"{scriptt}\" \"{filePath}\"";

                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;


                using (var process = Process.Start(psi))
                {
                    //errors = process.StandardError.ReadToEnd(); слишком медленно
                    result = process.StandardOutput.ReadToEnd();
                }
            }
            else if (splitted[0] == "7003")
            {
                // РАСПАКОВКА АРХИВА В ПАПКУ
                var test = splitted[1].Substring(splitted[1].LastIndexOf("."));
                // генерируем рандомное название для папки
                Random rand = new Random();
                var folderName = GetRandomString();

                var targetFolder = @"wwwroot\" + folderName;

                if (test.Equals(".7z"))
                {
                    // РАЗАРХИВИРОВАЛИ В ПАПКУ
                    using (SevenZipArchive archive = new SevenZipArchive(splitted[1]))
                    {
                        archive.ExtractToDirectory(targetFolder);
                    }

                    var psi = new ProcessStartInfo();
                    psi.FileName = @"python.exe";
                    var scriptt = @"wwwroot\methods\newMSD.py";

                    psi.Arguments = $"\"{scriptt}\" \"{targetFolder}\" \"{splitted[2]}\" ";

                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;


                    using (var process = Process.Start(psi))
                    {
                        //errors = process.StandardError.ReadToEnd(); слишком медленно
                        result = process.StandardOutput.ReadToEnd();
                    }

                    // архивация полученных файлов
                    if (splitted[2] == "true") // если выбрано построение графика, то сохраняем два файла
                    {

                        // Создать FileStream для выходного ZIP-архива
                        using (FileStream zipFile = System.IO.File.Open("MSD_Files.zip", FileMode.Create))
                        {
                            // Файл для добавления в архив
                            using (FileStream source1 = System.IO.File.Open("MSD.txt", FileMode.Open, FileAccess.Read))
                            {
                                // Файл для добавления в архив
                                using (FileStream source2 = System.IO.File.Open("SEM.txt", FileMode.Open, FileAccess.Read))
                                {
                                    using (var archive = new Archive())
                                    {

                                        using (FileStream source3 = System.IO.File.Open("AverageMSD.txt", FileMode.Open, FileAccess.Read))
                                        {
                                            // Добавить файлы в архив
                                            archive.CreateEntry("MSD.txt", source1); // добавляем файл со смещениями
                                            archive.CreateEntry("SEM.txt", source2); // добавляем файл с ошибками 
                                            archive.CreateEntry("AverageMSD.txt", source3); // добавляем файл со средними значениями MSD

                                            // Заархивируйте файлы
                                            archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII, ArchiveComment = "three files are compressed in this archive" });
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else { result = "archiveerror"; } // в случае неправильного расширения архива с координатами
            }
            // ЕСЛИ МЕТОД ПОИСКА ПАРАМЕТРА ТЕТРАТИЧЕСКОГО ПОРЯДКА
            else if (splitted[0] == "8003")
            {

                string zip = splitted[1];
                string checkSEM = splitted[2];

                // генерируем рандомное название для папки
                Random rand = new Random();
                var folderName = GetRandomString();
                var targetFolder = @"wwwroot\" + folderName;
                // var targetFolder = @"C:\Users\l.khusainova\source\repos\diploma\diploma\wwwroot\" + folderName;

                // РАСПАКОВКА АРХИВА В ПАПКУ
                var test = zip.Substring(zip.LastIndexOf("."));

                if (test.Equals(".7z"))
                {
                    var left = splitted[3];
                    var right = splitted[4];
                    var Nring = splitted[5];
                    var webroot = this.Environment.WebRootPath;

                    // РАЗАРХИВИРОВАЛИ В ПАПКУ
                    using (SevenZipArchive archive = new SevenZipArchive(zip))
                    {
                        archive.ExtractToDirectory(targetFolder);
                    }

                    var psi = new ProcessStartInfo();
                    psi.FileName = @"python.exe";
                    var scriptt = @"wwwroot\methods\anisotropy.py";

                    psi.Arguments = $"\"{scriptt}\" \"{webroot + "\\" + folderName}\" \"{checkSEM}\" \"{left}\" \"{right}\" \"{Nring}\" \"{webroot}\" \"{splitted[6]}\"";

                    Console.WriteLine(psi.Arguments);

                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;

                    using (var process = Process.Start(psi))
                    {
                        //errors = process.StandardError.ReadToEnd(); слишком медленно
                        result = process.StandardOutput.ReadToEnd();
                    }

                    var fileName = result; // новое название файла rS

                    // архивация полученных файлов
                    if (checkSEM == "true") // если выбрано построение графика, то сохраняем два файла
                    {
                        Console.WriteLine(webroot);
                        
                        // Создать FileStream для выходного ZIP-архива
                        using (FileStream zipFile = System.IO.File.Open("outputS4.zip", FileMode.Create))
                        {
                            // Файл для добавления в архив
                            using (FileStream source1 = System.IO.File.Open("rS.txt", FileMode.Open, FileAccess.Read))
                            {
                                // Файл для добавления в архив
                                using (FileStream source2 = System.IO.File.Open(webroot + "\\" + folderName + "\\SEM.txt", FileMode.Open, FileAccess.Read))
                                {
                                    using (var archive = new Archive())
                                    {

                                        // Добавить файлы в архив
                                        archive.CreateEntry("rS.txt", source1); // добавляем файл со смещениями
                                        archive.CreateEntry("SEM.txt", source2); // добавляем файл с ошибками 

                                        // Заархивируйте файлы
                                        archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII, ArchiveComment = "two files are compressed in this archive" });
                                    }
                                }
                            }
                        }
                    }
                    var amount = System.IO.Directory.GetFiles(targetFolder).Length; // подсчитываем количество файлов папки для проверки на ошибку

                    if (amount == 1 && checkSEM == "true") { result = "oneFile"; };
                }
                else { result = "archiveerror"; } // в случае неправильного расширения архива с координатами

            }
            else if (splitted[0] == "9003")
            {
                string zip = splitted[1];
                string checkSEM = splitted[2];

                // генерируем рандомное название для папки
                Random rand = new Random();
                var folderName = GetRandomString();
                var targetFolder = @"wwwroot\" + folderName;
                // var targetFolder = @"C:\Users\l.khusainova\source\repos\diploma\diploma\wwwroot\" + folderName;

                // РАСПАКОВКА АРХИВА В ПАПКУ
                var test = zip.Substring(zip.LastIndexOf("."));

                if (test.Equals(".7z"))
                {
                    var left = splitted[3];
                    var right = splitted[4];
                    var Nring = splitted[5];
                    var webroot = this.Environment.WebRootPath;

                    // РАЗАРХИВИРОВАЛИ В ПАПКУ
                    using (SevenZipArchive archive = new SevenZipArchive(zip))
                    {
                        archive.ExtractToDirectory(targetFolder);
                    }

                    var psi = new ProcessStartInfo();
                    psi.FileName = @"python.exe";
                    var scriptt = @"wwwroot\methods\nematicParameter.py";

                    psi.Arguments = $"\"{scriptt}\" \"{webroot + "\\" + folderName}\" \"{checkSEM}\" \"{left}\" \"{right}\" \"{Nring}\" \"{webroot}\" \"{splitted[6]}\"";

                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;

                    using (var process = Process.Start(psi))
                    {
                        //errors = process.StandardError.ReadToEnd(); слишком медленно
                        result = process.StandardOutput.ReadToEnd();
                    }

                    var fileName = result; // новое название файла rS

                    /*
                    // архивация полученных файлов
                    if (checkSEM == "true") // если выбрано построение графика, то сохраняем два файла
                    {
                        // Создать FileStream для выходного ZIP-архива
                        using (FileStream zipFile = System.IO.File.Open("outputS.zip", FileMode.Create))
                        {
                            // Файл для добавления в архив
                            using (FileStream source1 = System.IO.File.Open("rS.txt", FileMode.Open, FileAccess.Read))
                            {
                                // Файл для добавления в архив
                                using (FileStream source2 = System.IO.File.Open(webroot + "\\" + folderName + "\\SEM.txt", FileMode.Open, FileAccess.Read))
                                {
                                    using (var archive = new Archive())
                                    {

                                        // Добавить файлы в архив
                                        archive.CreateEntry("rS.txt", source1); // добавляем файл со смещениями
                                        archive.CreateEntry("SEM.txt", source2); // добавляем файл с ошибками 

                                        // Заархивируйте файлы
                                        archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII, ArchiveComment = "two files are compressed in this archive" });
                                    }
                                }
                            }
                        }
                    }*/

                    var amount = System.IO.Directory.GetFiles(targetFolder).Length; // подсчитываем количество файлов папки для проверки на ошибку

                    if (amount == 1 && checkSEM == "true") { result = "oneFile"; };
                }
                else { result = "archiveerror"; } // в случае неправильного расширения архива с координатами
            }

            return Json(result);
        }
    }
}
