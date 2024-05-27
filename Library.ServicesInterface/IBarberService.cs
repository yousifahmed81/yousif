using ClassLibrary.domian;
namespace Library.ServicesInterfaces
{
    public interface IBarberService
    {

        void Save(Barber barber);
        void Update(Barber barber);
        void Delete(Barber barber);
        Barber Get(int id);
        Barber Get(string name);
        List<Barber> GetList(string email);
        List<Barber> GetAll();
      

    }
}
