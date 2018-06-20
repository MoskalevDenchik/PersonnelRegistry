using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DM.PR.WEB.Models.User
{
    public class UserUpdateViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароль и подтверждение не совпадают")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Добавьте роли")]
        [Display(Name = "Роли")]
        public int[] Roles { get; set; }
    }
}