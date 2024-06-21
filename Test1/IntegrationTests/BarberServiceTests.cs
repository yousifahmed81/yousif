using ClassLibrary.domian;
using ClassLibrary2;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests.IntegrationTests
{
    public class BarberServiceTests
    {
        private DbContextOptions<BarberDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BarberDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private IDbContextFactory<BarberDbContext> GetDbContextFactory(DbContextOptions<BarberDbContext> options)
        {
            var mockFactory = new Mock<IDbContextFactory<BarberDbContext>>();
            mockFactory.Setup(f => f.CreateDbContext()).Returns(() => new BarberDbContext(options));
            return mockFactory.Object;
        }

        [Fact]
        public async Task Save_AddBarber()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            var barber = new Barber { name = "Barber1", Phone = "0911785484", Email="yousifahmed1819@gmail.com" };

            // Act
            await service.Save(barber);

            // Assert
            using var context = new BarberDbContext(options);
            var savedBarber = await context.Barbers.FirstOrDefaultAsync(b => b.name == "Barber1");
            Assert.NotNull(savedBarber);
        }

        [Fact]
        public async Task GetByid_ShouldReturnBarberByid()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            var barber = new Barber { name = "Barber1", Phone = "0911785484", Email = "yousifahmed1819@gmail.com" };
            await service.Save(barber);

            // Act
            var fetchedBarber = await service.Get(barber.id);

            // Assert
            Assert.NotNull(fetchedBarber);
            Assert.Equal(barber.name, fetchedBarber.name);
        }

        [Fact]
        public async Task GetByname_ShouldReturnBookByname()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            var barber = new Barber { name = "Barber1", Phone = "0911785484", Email = "yousifahmed1819@gmail.com" };
            await service.Save(barber);

            // Act
            var fetchedBarber = await service.Get("0911785484");

            // Assert
            Assert.NotNull(fetchedBarber);
            Assert.Equal(barber.name, fetchedBarber.name);
        }

        [Fact]
        public async Task GetList_ShouldReturnBarberByEmail()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            await service.Save(new Barber { name = "Barber1", Phone = "0911785484", Email = "yousifahmed1819@gmail.com" });
            await service.Save(new Barber { name = "Barber4", Phone = "0927731527", Email = "youifahmad8191@gmail.com" });

            // Act
            var barbers = await service.GetList("Barber");

            // Assert
            Assert.Equal(2, barbers.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllBarberss()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            await service.Save(new Barber { name = "Barber1", Phone = "0911785484", Email = "yousifahmed1819@gmail.com" });
            await service.Save(new Barber { name = "Barber2", Phone = "0927731527", Email = "youifahmad8191@gmail.com" });

            // Act
            var barbers = await service.GetAll();

            // Assert
            Assert.Equal(2, barbers.Count);
        }

        [Fact]
        public async Task Delete_ShouldRemoveBarber()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            var barber = new Barber { name = "Barber1", Phone = "0911785484", Email = "yousifahmed1819@gmail.com" };
            await service.Save(barber);

            // Act
            await service.Delete(barber);

            // Assert
            using var context = new BarberDbContext(options);
            var deletedBarber = await context.Barbers.FindAsync(barber.id);
            Assert.Null(deletedBarber);
        }

        [Fact]
        public async Task Update_ShouldModifyBarber()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            var barber = new Barber { name = "Barber1", Phone = "0911785484", Email = "yousifahmed1819@gmail.com" };
            await service.Save(barber);

            // Act
            barber.Email = "ahmed1212@gmail.com";
            barber.Phone = "1234567890";
            await service.Update(barber);

            // Assert
            using var context = new BarberDbContext(options);
            var updatedBarber = await context.Barbers.FindAsync(barber.id);
            Assert.Equal("ahmed1212@gmail.com", updatedBarber.Email);
            Assert.Equal("1234567890", updatedBarber.Phone);
        }

        [Fact]
        public async Task AddServiceToBarber_ShouldAddAuthor()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            var barber = new Barber {name = "Barber1", Phone = "0911785484", Email = "yousifahmed1819@gmail.com" };
            var service1 = new Service { name = "service1",  Price = "123456789"};
            await service.Save(barber);

            // Act
            await service.AddServiceToBarber(barber, service1);

            // Assert
            using var context = new BarberDbContext(options);
            var savedBarber = await context.Barbers.Include(b => b.services).FirstOrDefaultAsync(b => b.id == barber.id);
            Assert.NotNull(savedBarber);
            Assert.Contains(savedBarber.services, a => a.name == "service1");
        }

        [Fact]
        public async Task RemoveServiceFromBarbe_ShouldRemoveService()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactory(options);
            var service = new BarberService(factory);
            var barber = new Barber { name = "Barber1", Phone = "0911785484", Email = "yousifahmed1819@gmail.com" };
            var service1 = new Service { name = "service1", Price = "123456789" };
            await service.Save(barber);
            await service.AddServiceToBarber(barber, service1);

            // Act
            await service.AddServiceToBarber(barber, service1);

            // Assert
            using var context = new BarberDbContext(options);
            var savedBarber = await context.Barbers.Include(b => b.services).FirstOrDefaultAsync(b => b.id == barber.id);
            Assert.NotNull(savedBarber);
            Assert.DoesNotContain(savedBarber.services, a => a.name == "service1");
        }
    }
}
