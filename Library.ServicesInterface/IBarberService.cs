using ClassLibrary.domian;
using ClassLibrary2;
namespace Library.ServicesInterfaces
{
    public interface IBarberService
    {

        Task Save(Barber barber);
        Task Update(Barber barber);
        Task Delete(Barber barber);
        Task <Barber> Get(int id);
        Task <Barber> Get(string name);
        Task<List<Barber>> GetList(string Email);
        Task<List<Barber>>GetAll();
        Task<Barber> GetByChairNum(string chairNum);

    }
}
