using ClassLibrary.domian;
using ClassLibrary2;
using Library.ServicesInterface;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Services
{
    public class BarberService : IBarberService
    {
        private readonly IDbContextFactory<BarberDbContext> _contextFactory;

        public BarberService(IDbContextFactory<BarberDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public void Save(barber barber)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.barber.FirstOrDefault(x => x.id == barber.id);

            if (tmp == null)
            {
                db.barber.Add(barber);
                db.SaveChanges();
            }

        }

        public void Update(barber barber)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.barber.FirstOrDefault(x => x.id == barber.id);

            if (tmp != null)
            {
                tmp.name = barber.name;
                tmp.Phone = barber.Phone;
                tmp.Email = barber.Email;


                db.SaveChanges();
            }
        }

        public void Delete(barber barber)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.barber.FirstOrDefault(x => x.id == barber.id);

            if (tmp != null)
            {
                db.barber.Remove(tmp);
                db.SaveChanges();
            }
        }

        public barber Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = db.barber.FirstOrDefault(x => x.id == id);
            return barber;
        }

        public barber Get(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = db.barber.FirstOrDefault(x => x.name.ToUpper() == name.Trim().ToUpper());
            return barber;
        }

        public List<barber> GetList(string email)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = db.barber.Where(x => x.Email.Contains(email));
            return [.. barber];
        }

        public List<barber> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return [.. db.barber];
        }
    }
}
