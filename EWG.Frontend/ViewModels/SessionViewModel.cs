﻿using System.ComponentModel.DataAnnotations;

namespace EWG.Frontend.ViewModels
{
    public class SessionViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}