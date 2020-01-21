using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingPong.API.Database.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        
        public int Player1ResultId { get; set; }

        public int Player2ResultId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        
        [ForeignKey(nameof(Player1ResultId))]
        public virtual PlayerResult Player1Result { get; set; }
        
        [ForeignKey(nameof(Player2ResultId))]
        public virtual PlayerResult Player2Result { get; set; }
    }
}