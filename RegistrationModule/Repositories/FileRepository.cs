using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using RegistrationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using File = RegistrationModule.Models.File;
using Hash = RegistrationModule.Models.Hash;

namespace RegistrationModule.Repositories
{
    public class FileRepository
    {
        public static async Task AddFileInfoToDB(string filePath, string fileName)
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
        public static async Task<File> GetFileAsync(string fileName)
        {
            using AppDbContext appDbContext = new AppDbContext();
            File file = await appDbContext.Files.FirstOrDefaultAsync(f => f.Name == fileName);
            if (file == null)
            {
                return null;
            }
            return file;
        }
        public static async Task UpdateFileInfo(string fileName, string password)
        {
            using AppDbContext appDbContext = new AppDbContext();
            Random random = new Random();
            Hash hash = new Hash();

            hash.HashSalt = new byte[10];
            random.NextBytes(hash.HashSalt);

            var file = await appDbContext.Files.FirstOrDefaultAsync(f => f.Name == fileName);

            file.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: password,
                                salt: hash.HashSalt,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 100000,
                                numBytesRequested: 256 / 8));
            file.HashSalt = hash.HashSalt;
            appDbContext.Update(file);
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
        public static async Task<string> GetFilePassword(string fileName)
        {
            using AppDbContext appDbContext = new AppDbContext();
            var file = await appDbContext.Files.FirstOrDefaultAsync(f => f.Name == fileName);
            if (file == null)
            {
                return null;
            }
            return file.Password;
        }
    }
}
