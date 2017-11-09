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

        [Required]
        public string Password { get; set; }

        [NotMapped()]
        public string PasswordConfirm { get; set; }

        [NotMapped()]
        public string OldPassord { get; set; }

        public virtual ICollection<MovieCopy> MovieCopiesRent { get; set; }
    }
}
