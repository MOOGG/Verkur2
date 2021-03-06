﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Veidibokin.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Netfang")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [Display(Name = "Mundu mig")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
		[Required]
		[Display(Name = "Fullt nafn")]
		public string fullName { get; set; }

		[Required(ErrorMessage = "Vinsamlega settu inn póstnúmer")]
        [Display(Name = "Póstnúmer")]
        public int postalCode { get; set; }

		[Required(ErrorMessage="Vinsamlega settu inn kyn")]
        [Display(Name = "Kyn")]
        public string gender { get; set; }

		[Required(ErrorMessage = "Vinsamlega settu inn netfang")]
        [EmailAddress]
        [Display(Name = "Netfang")]
        public string Email { get; set; }

        [Required] //The {0} must be at least {2} characters long.
        [StringLength(100, ErrorMessage = "{0}ið verður að vera amk {2} stafir.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Lykilorð aftur")]
        [Compare("Password", ErrorMessage = "Lykilorðin eru ekki eins, sláðu þau inn aftur")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
		[StringLength(100, ErrorMessage = "{0}ið verður að vera amk {2} stafir.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "Lykilorðin eru ekki eins, sláðu þau inn aftur")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
