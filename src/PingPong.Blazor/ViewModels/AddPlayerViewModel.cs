﻿using System.ComponentModel.DataAnnotations;

namespace PingPong.Blazor.ViewModels
{
    public class AddPlayerViewModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}