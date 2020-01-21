using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingPong.API.Database.Models
{
    [Table("Player")]
    public class Player
    {
        [Key]
        public int    Id        { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName  { get; set; }
        
        public int    Wins      { get; set; }
        
        public int    Losses    { get; set; }
        
        public int    Total     { get; set; }
        
        [Required]
        public DateTime Created { get; set; }
        
        [Required]
        public DateTime Modified { get; set; }
    }
}