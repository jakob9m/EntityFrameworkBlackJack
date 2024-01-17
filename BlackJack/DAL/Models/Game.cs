using Elsa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Game
    {
        [Key]
        [MaxLength(255)]
        public string GameId { get; set; }

        [Required]
        public int NoOfPlayers { get; set; }

        [Required]
        public int DeckSize { get; set; }

        // Navigation property
        public virtual ICollection<Result> Results { get; set; }
    }

}
