using ClassLibrary.domian;
using ClassLibrary2;
using Library.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class ServicesService : IServiceService
    {
        private readonly IDbContextFactory<BarberDbContext> _contextFactory;

        public ServicesService(IDbContextFactory<BarberDbContext> ContextFactory)
        {
            _contextFactory = ContextFactory;
        }


        public async Task Save(Service service)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Barbers.FirstOrDefault(x => x.id == service.id);

            if (tmp == null)
            {
               db.Services.Add(service);
               await db.SaveChangesAsync();
            }

        }

        public async Task Update(Service service)
        {
            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.Services.FirstOrDefault(y => y.id == service.id);

            if (tmp != null)
            {
                tmp.name = service.name;
                tmp.Price = service.Price;

               await db.SaveChangesAsync();
            }
        }

        public async void Delete(Service service)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Services.FirstOrDefault(x => x.id == service.id);

            if (tmp != null)
            {
                db.Services.Remove(tmp);
              await  db.SaveChangesAsync();
            }
        }

        public async Task<Service> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var service = await db.Services.FirstOrDefaultAsync(x => x.id == id);
            return service;
        }

        public Service Get(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var service = db.Services.FirstOrDefault(x => x.name.ToUpper() == x.name.Trim().ToUpper());
            return service;
        }

        public async Task<List<Service>> GetList(string price)
        {
            using var db = _contextFactory.CreateDbContext();

            var service = db.Services.Where(x => x.Price.Contains(price));
            return [..await service.ToListAsync()];
        }

        public async Task<List<Service>> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return await db.Services.ToListAsync();
        }
    }
    }
