
using diploma.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diploma.ViewModels
{
    public class RegisterModel
    {
        public string Surname { get; set; }
        
        public string Name { get; set; }
       
        public string Patronymic { get; set; }
       
        public Role Role { get; set; }
        
        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
