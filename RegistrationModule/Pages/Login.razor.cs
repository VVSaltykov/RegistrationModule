using Microsoft.AspNetCore.Components;
using RegistrationModule.Interfaces;
using RegistrationModule.Other;
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
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        protected LoginViewModel loginViewModel { get; set; } = new LoginViewModel();
        protected AlarmStatus AlarmStatus { get; set; } 
        protected int Count { get; set; }

        protected async Task LoginAsync()
        {
            AlarmStatus = await AuthService.GetUser(loginViewModel.Login, loginViewModel.Password);
            if (AlarmStatus == AlarmStatus.CorrectData)
            {
                NavigationManager.NavigateTo("Index");
            }
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
