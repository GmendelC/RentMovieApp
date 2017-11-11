using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    
    public class RentHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual MovieCopy Copy { get; set; }

        [Required]
        public virtual User UserRent { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RentDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RentReturn { get; set; }
    }
}
