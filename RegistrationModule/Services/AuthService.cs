using Microsoft.EntityFrameworkCore;
using RegistrationModule.Interfaces;
using RegistrationModule.Models;
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
                User user = new User
                {
                    Login = login,
                    Password = password,
                    Name = name,
                    Phone = phone,
                    Address = address
                };
                user.Id = GetUUID();
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> GetUser(string login, string password)
        {
            using AppDbContext db = new AppDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == GetUUID());
            if (user != null && user.Login == login && user.Password == password)
            {
                return true;
            }
            return false;
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
