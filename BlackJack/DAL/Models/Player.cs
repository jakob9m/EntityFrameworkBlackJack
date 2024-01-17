using Elsa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Player
    {
        [Key]
        [MaxLength(255)]
        public string PlayerId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

       
                                     
        public virtual ICollection<Result> Results { get; set; }
    }
}

