using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.domian
{
    public class Barber
    {
        public int id { get; set; }
        
        [MaxLength( length : 50)]
        public string name { get; set; }

        [MaxLength( length : 50)]
        [Phone]
        public string Phone { get; set; }
        
        [MaxLength( length : 60)]
        [EmailAddress]
        public string Email { get; set; }

        public List<Service> services { get; set; }
    }

}
