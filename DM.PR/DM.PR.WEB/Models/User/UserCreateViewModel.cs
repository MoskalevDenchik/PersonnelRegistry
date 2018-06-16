using System.ComponentModel.DataAnnotations;
using DM.PR.Common.Entities.Account;
using System.Collections.Generic;

namespace DM.PR.WEB.Models.User
{
    public class UserCreateViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароль и подтверждение не совпадают")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Добавьте роли")]
        [Display(Name = "Роли")]
        public List<Role> Roles { get; set; }
    }
}