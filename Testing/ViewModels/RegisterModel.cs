﻿using System.ComponentModel.DataAnnotations;

namespace Testing.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Напишите Никнейм")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }
    }
}
