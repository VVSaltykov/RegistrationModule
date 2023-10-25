using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule.Models
{
    public class Hash
    {
        [Key]
        public string Password { get; set; }
        public byte[] HashSalt { get; set; }
    }
}
