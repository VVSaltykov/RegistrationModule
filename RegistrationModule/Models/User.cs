using RegistrationModule.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Hash Salt { get; set; }
        public Role Role { get; set; } = Role.User;
    }
}
