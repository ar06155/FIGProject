﻿using System.ComponentModel.DataAnnotations;

namespace FIGProject.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? TeamId { get; set; }

        //public Team? Team { get; set; } 
    }
}
