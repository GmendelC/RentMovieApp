using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class MovieCopy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime RentDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        // this field infor if this copy was removed from rent,
        // Cant to remove copy or movie from data base because the history.
        
        // In future you can do clean data base to rmove all old movies
        // Not aplicate
        public bool Removed { get; set; }

        public virtual Movie ForMovie { get; set; }

        [ForeignKey("UserRentId")]
        public virtual User UserRent { get; set; }

        public int UserRentId { get; set; }
    }
}
