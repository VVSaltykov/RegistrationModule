using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule.Models
{
    public class FileHash
    {
        [Key]
        public string Path { get; set; }
        public byte[] Salt { get; set; }
        public DateTime DateTime { get; set; }
    }
}
