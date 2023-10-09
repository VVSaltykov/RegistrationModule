using Microsoft.AspNetCore.Components;
using RegistrationModule.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        protected IAuthService AuthService { get; set; }
        protected LoginViewModel loginViewModel { get; set; } = new LoginViewModel();
        public bool UserInDB { get; set; } 

        protected async Task LoginAsync()
        {
            UserInDB = await AuthService.GetUser(loginViewModel.Login, loginViewModel.Password);
        }
    }
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
