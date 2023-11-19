using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using RegistrationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule.Repositories
{
    public class FileRepository
    {
        public static async Task HashPath(string filePath, string fileName)
        {
            using AppDbContext appDbContext = new AppDbContext();
            Models.File file = new Models.File();
            Random random = new Random();

            file.Salt = new byte[10];
            random.NextBytes(file.Salt);

            filePath = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: filePath,
                                salt: file.Salt,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 100000,
                                numBytesRequested: 256 / 8));
            file.Path = filePath;
            file.Name = fileName;
            file.DateTime = DateTime.Now.ToUniversalTime().ToString("dd-MM-yyyy HH:mm");


            appDbContext.Files.Add(file);
            await appDbContext.SaveChangesAsync();
        }
        public static async Task<bool> FileBeenModified(string dateTime)
        {
            using AppDbContext appDbContext = new AppDbContext();
            var file = await appDbContext.Files.FirstOrDefaultAsync(f => f.DateTime == dateTime);
            if (file == null)
            {
                return true;
            }
            return false;
        }
    }
}
