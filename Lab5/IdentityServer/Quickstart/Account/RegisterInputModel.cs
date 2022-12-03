// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace IdentityServerHost.Quickstart.UI
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Користувач обов'язково")]
        [StringLength(maximumLength: 50)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Пароль обов'язково")]
        [StringLength(maximumLength: 16, MinimumLength = 8)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароль не співпадає")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "ПІБ обов'язково")]
        [StringLength(maximumLength: 500)]
        public string NameSurname { get; set; }
         
        [Phone(ErrorMessage = "Телефон обов'язково у форматі України")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email обов'язковий")]
        [EmailAddress] 
        public string Email { get; set; }

        public string ReturnUrl { get; set; }
    }
}