using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PingPong.API.Database.Models
{
    public class PlayerResult
    {
        [Key]
        public int Id { get; set; }
        
        public int PlayerId { get; set; }

        public int Score { get; set; }

        public bool IsWinner { get; set; }

        
        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; }
    }
}