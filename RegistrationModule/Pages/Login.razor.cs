using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RegistrationModule.Interfaces;
using RegistrationModule.Other;
using RegistrationModule.Repositories;
using RegistrationModule.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RegistrationModule.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        protected IAuthService AuthService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected Other.Condition Condition { get; set; }
        protected LoginViewModel loginViewModel { get; set; } = new LoginViewModel();
        protected AlarmStatus AlarmStatus { get; set; } 
        protected int Count { get; set; }

        protected async Task LoginAsync()
        {
            AlarmStatus = await AuthService.GetUser(loginViewModel.Login, loginViewModel.Password);
            if (AlarmStatus == AlarmStatus.CorrectData)
            {
                var user = await UserRepository.GetUserByLoginAsync(loginViewModel.Login);
                string userJson = JsonSerializer.Serialize(user);
                await JSRuntime.InvokeVoidAsync("sessionStorage.setItem", "user", userJson);
                Condition.IsAuthenticated = true;
                NavigationManager.NavigateTo("Index", forceLoad: true);
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
