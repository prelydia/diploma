using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diploma.ViewModels
{
    public class ShowViewModel
    {
        public IEnumerable<diploma.Models.File> Files { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
