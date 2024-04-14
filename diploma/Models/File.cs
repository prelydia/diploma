using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diploma.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Path { get; set; }
        public User Author { get; set; }

        public int AuthorId { get; set; }
        public DateTime Date { get; set; }
    }
}
