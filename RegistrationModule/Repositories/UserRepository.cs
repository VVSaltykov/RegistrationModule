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
    }
}
