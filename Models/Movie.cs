using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string MovieName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public EMovieCategory Category { get; set; }

        [InverseProperty("ForMovie")]
        public virtual ICollection<MovieCopy> Copies { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string ImageMime { get; set; }

        public byte[] ImageBits { get; set; }
    }
}
