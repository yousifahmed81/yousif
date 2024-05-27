
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.domian
{
    public class Service

    {
        public int id { get; set; }

        [MaxLength(length: 50)]
        public string name { get; set; }

        [MaxLength(length: 100)]
        
        public string Price { get; set; }
        [MaxLength(length: 100)]
        public Service barber { get; set; }

        public int barberId { get; set; }


    }
}
