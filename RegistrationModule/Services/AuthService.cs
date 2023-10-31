using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using RegistrationModule.Interfaces;
using RegistrationModule.Models;
using RegistrationModule.Other;
using System.Diagnostics;
using System.Windows.Forms;

namespace RegistrationModule.Services
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {
        }

        public async Task<bool> AddUser(string login, string password, string name, string phone, string address)
        {
            try
            {
                using AppDbContext db = new AppDbContext();
                Random random = new Random();
                Hash hash = new Hash();

                hash.HashSalt = new byte[10];
                random.NextBytes(hash.HashSalt);

                User user = new User
                {
                    Login = login,
                    Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: password,
                                salt: hash.HashSalt,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 100000,
                                numBytesRequested: 256 / 8)),
                    Name = name,
                    Phone = phone,
                    Address = address
                };
                user.Id = GetUUID();
                hash.Password = user.Password;
                db.Hashes.Add(hash);
                await db.SaveChangesAsync();
                user.Salt = hash;
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<AlarmStatus> GetUser(string login, string password)
        {
            using AppDbContext db = new AppDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Login == login);
            var userSalt = await GetUserSalt(user);
            var userPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                password: password,
                                salt: userSalt,
                                prf: KeyDerivationPrf.HMACSHA256,
                                iterationCount: 100000,
                                numBytesRequested: 256 / 8));
            if (user != null && user.Id == GetUUID() && user.Login == login && user.Password == userPassword)
            {
                return AlarmStatus.CorrectData;
            }
            if (user.Id != GetUUID())
            {
                return AlarmStatus.IncorrectUUID;
            }
            return AlarmStatus.IncorrectData;
        }
        public async Task<byte[]> GetUserSalt(User user)
        {
            using AppDbContext appDbContext = new AppDbContext();
            var salt = await appDbContext.Hashes.FirstOrDefaultAsync(s => s.Password == user.Password);
            return salt.HashSalt;
        }
        public string GetUUID()
        {
            var procStartInfo = new ProcessStartInfo("cmd", "/c " + "wmic csproduct get UUID")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var proc = new Process() { StartInfo = procStartInfo };
            proc.Start();

            return proc.StandardOutput.ReadToEnd().Replace("UUID", string.Empty).Trim().ToUpper();
        }
    }
}
