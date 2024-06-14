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

        /// <summary>
        /// method to save barber
        /// </summary>
        /// <param name="barber"></param>
        public async Task Save(Barber barber)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Barbers.FirstOrDefault(x => x.id == barber.id);

            if (tmp == null)
            {
                db.Barbers.Add(barber);
                await db.SaveChangesAsync();
            }

        }

        public async Task Update(Barber barber)
        {

            using var db = (_contextFactory.CreateDbContext());

            var tmp = db.Barbers.FirstOrDefault(x => x.id == barber.id);

            if (tmp != null)
            {
                tmp.name = barber.name;
                tmp.Phone = barber.Phone;
                tmp.Email = barber.Email;


                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(Barber barber)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Barbers.FirstOrDefault(x => x.id == barber.id);

            if (tmp != null)
            {
                db.Barbers.Remove(tmp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Barber> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = await db.Barbers.FirstOrDefaultAsync(x => x.id == id);
            return barber;
        }

        public async Task<Barber> Get(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = await db.Barbers.FirstOrDefaultAsync(x => x.name.ToUpper() == name.Trim().ToUpper());
            return barber;
        }

        public async Task<List<Barber>> GetList(string email)
        {
            using var db = _contextFactory.CreateDbContext();

            var barber = await db.Barbers.Where(x => x.Email.Contains(email)).ToListAsync();
            return [.. barber];
        }

        public async Task<List<Barber>> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return await db.Barbers.ToListAsync();
        }
        public async Task AddServiceToBarber(Barber barber, Service service)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmpBar = db.Barbers.Include(x => x.services).FirstOrDefault(x => x.id == barber.id);
            if (tmpBar != null)
            {
                var tmpSer = db.Services.FirstOrDefault(x => x.id == service.id);
                if (tmpSer != null)
                {
                    tmpBar.services.Add(tmpSer);
                }
                else
                {
                    db.Services.Add(service);
                    await db.SaveChangesAsync();
                    tmpBar.services.Add(service);
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveServiceFromBarber(Barber barber, Service service)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmpBar = db.Barbers.Include(x => x.services).FirstOrDefault(x => x.id == barber.id);
            if (tmpBar != null)
            {
                var barberserver = tmpBar.services.FirstOrDefault(x => x.id == service.id);
                if (barberserver != null)
                {
                    tmpBar.services.Remove(barberserver);
                    await db.SaveChangesAsync();
                }
            }

        }
    }
}
