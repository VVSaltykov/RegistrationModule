using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
        public static async Task HashPath(string filePath)
        {
            using AppDbContext appDbContext = new AppDbContext();
            FileHash fileHash = new FileHash();
            Random random = new Random();

            fileHash.Salt = new byte[10];
            random.NextBytes(fileHash.Salt);

            filePath = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: filePath,
                                salt: fileHash.Salt,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 100000,
                                numBytesRequested: 256 / 8));
            fileHash.Path = filePath;
            fileHash.DateTime = DateTime.Now;

            appDbContext.FileHashes.Add(fileHash);
            await appDbContext.SaveChangesAsync();
        }
    }
}
