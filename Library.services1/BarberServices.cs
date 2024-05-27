using ClassLibrary.domian;
using ClassLibrary2;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BarberService : IBarberService
    {
        private readonly IDbContextFactory<BarberDbContext> _contextFactory;

        public BarberService(IDbContextFactory<BarberDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        public void Save(Barber barber)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Barbers.FirstOrDefault(x => x.id == barber.id);

            if (tmp == null)
            {
                db.Barbers.Add(barber);
                db.SaveChanges();
            }

        }

        public void Update(Barber barber)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.Barbers.FirstOrDefault(x => x.id == barber.id);

            if (tmp != null)
            {
                tmp.name = barber.name;
                tmp.Phone = barber.Phone;
                tmp.Email = barber.Email;


                db.SaveChanges();
            }
        }

        public void Delete(Barber barber)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Barbers.FirstOrDefault(x => x.id == barber.id);

            if (tmp != null)
            {
                db.Barbers.Remove(tmp);
                db.SaveChanges();
            }
        }

        public Barber Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = db.Barbers.FirstOrDefault(x => x.id == id);
            return barber;
        }

        public Barber Get(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = db.Barbers.FirstOrDefault(x => x.name.ToUpper() == name.Trim().ToUpper());
            return barber;
        }

        public List<Barber> GetList(string email)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = db.Barbers.Where(x => x.Email.Contains(email));
            return [.. barber];
        }

        public List<Barber> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return [.. db.Barbers];
        }
    }
}
