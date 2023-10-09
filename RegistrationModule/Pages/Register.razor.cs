using Microsoft.AspNetCore.Components;
using RegistrationModule.Interfaces;
using RegistrationModule.Models;
using RegistrationModule.Services;
using System.ComponentModel.DataAnnotations;

namespace RegistrationModule.Pages
{
    public partial class Register : ComponentBase
    {
        [Inject]
        protected IAuthService AuthService { get; set; }

        protected RegisterViewModel registerViewModel { get; set; } = new RegisterViewModel();
        public bool UserRegistered { get; set; }

        protected async Task RegisterAsync()
        {
            UserRegistered = await AuthService.AddUser(registerViewModel.Login, registerViewModel.Password,
                registerViewModel.Name, registerViewModel.Phone, registerViewModel.Address);
        }
    }
    public class RegisterViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
