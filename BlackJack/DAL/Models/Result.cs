using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class Result
    {
        [Key, Column(Order = 0), ForeignKey("Player")]
        [MaxLength(255)]
        public string PlayerId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Game")]
        [MaxLength(255)]
        public string GameId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ResultValue { get; set; }

        public virtual Player Player { get; set; }
        public virtual Game Game { get; set; }
    }

}
