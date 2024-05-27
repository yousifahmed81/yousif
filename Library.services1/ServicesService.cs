using ClassLibrary.domian;
using ClassLibrary2;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class ServicesService : IServiceService
    {
        private readonly IDbContextFactory<BarberDbContext> _contextFactory;

        public ServicesService(IDbContextFactory<BarberDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }


        public void Save(Service service)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Barbers.FirstOrDefault(x => x.id == service.id);

            if (tmp == null)
            {
                db.Services.Add(service);
                db.SaveChanges();
            }

        }

        public void Update(Service service)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.Services.FirstOrDefault(x => x.id == service.id);

            if (tmp != null)
            {
                tmp.name = service.name;
                tmp.Price = service.Price;

                db.SaveChanges();
            }
        }

        public void Delete(Service services)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Services.FirstOrDefault(x => x.id == services.id);

            if (tmp != null)
            {
                db.Services.Remove(tmp);
                db.SaveChanges();
            }
        }

        public Service Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var Service = db.Services.FirstOrDefault(x => x.id == id);
            return Service;
        }

        public Service Get(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var Service = db.Services.FirstOrDefault(x => x.name.ToUpper() == x.name.Trim().ToUpper());
            return Service;
        }

        public List<Service> GetList(string price)
        {
            using var db = _contextFactory.CreateDbContext();

            var Service = db.Services.Where(x => x.Price.Contains(price));
            return [.. Service];
        }

        public List<Service> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return [.. db.Services];
        }
    }
    }
