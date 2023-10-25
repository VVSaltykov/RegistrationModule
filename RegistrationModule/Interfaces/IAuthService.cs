using RegistrationModule.Models;
using RegistrationModule.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.AppBroadcasting;

namespace RegistrationModule.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Add user to db
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> AddUser(string login, string password, string name, string phone, string address);
        /// <summary>
        /// Get user from db
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AlarmStatus> GetUser(string login, string password);
        /// <summary>
        /// Get UUID
        /// </summary>
        /// <returns></returns>
        string GetUUID();
    }
}
