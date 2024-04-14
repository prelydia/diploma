using diploma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diploma.ViewModels
{
    public class FileViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public User Author { get; set; }
        public DateTime Date { get; set; }
    }
}
