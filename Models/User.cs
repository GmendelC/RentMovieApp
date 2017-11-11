using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [EmailAddress]
        [Required]
        // have to be unique in data base.
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Cofirm Password")]
        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [NotMapped()]
        public string PasswordConfirm { get; set; }

        [DataType(DataType.Password)]
        [NotMapped()]
        public string OldPassord { get; set; }

        [InverseProperty("UserRent")]
        public virtual ICollection<MovieCopy> MovieCopiesRent { get; set; }
    }
}
