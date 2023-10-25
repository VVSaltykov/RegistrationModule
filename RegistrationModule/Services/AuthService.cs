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
        public async Task<AlarmStatus> GetUser(string login, string password)
        {
            using AppDbContext db = new AppDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user != null && user.Id == GetUUID() && user.Login == login && user.Password == password)
            {
                return AlarmStatus.CorrectData;
            }
            if (user.Id != GetUUID())
            {
                return AlarmStatus.IncorrectUUID;
            }
            return AlarmStatus.IncorrectData;
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
