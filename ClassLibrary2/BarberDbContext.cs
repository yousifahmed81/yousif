using ClassLibrary.domian;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class BarberDbContext : DbContext
    {
        public BarberDbContext(DbContextOptions<BarberDbContext> options) :
            base(options)
        {
            
        }
        public  DbSet<barber> Barbers { get; set; }
        public DbSet<Service> Services { get; set; }

      
        }
    }

