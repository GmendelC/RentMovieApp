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
        [Key, Column(Order = 0)]
        public virtual MovieCopy Copy { get; set; }

        [Key, Column(Order = 1)]
        public virtual User UserRent { get; set; }

        public DateTime RentDate { get; set; }
        
        public DateTime RentReturn { get; set; }
    }
}
