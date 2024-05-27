using ClassLibrary.domian;
namespace Library.ServicesInterfaces
{
    public interface IBarberService
    {

        void Save(barber barber);
        void Update(barber barber);
        void Delete(barber barber);
        barber Get(int id);
        barber Get(string name);
        List<barber> GetList(string email);
        List<barber> GetAll();
      

    }
}
