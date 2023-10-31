using Microsoft.EntityFrameworkCore;
using RegistrationModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule.Repositories
{
    public class UserRepository
    {
        public static async Task<User> GetUserByLoginAsync(string login)
        {
            using AppDbContext appDbContext = new AppDbContext();
            User user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user;
        }
        public static async Task<User> GetUserByIdAsync(string userId)
        {
            using AppDbContext appDbContext = new AppDbContext();
            User user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
        public static async Task<List<User>> GetUsersAsync()
        {
            using AppDbContext appDbContext = new AppDbContext();
            var users = await appDbContext.Users.ToListAsync();
            return users;
        }
        public static async Task UpdateUserAsync(User user)
        {
            using AppDbContext appDbContext = new AppDbContext();
            appDbContext.Update(user);
            await appDbContext.SaveChangesAsync();
        }
    }
}
