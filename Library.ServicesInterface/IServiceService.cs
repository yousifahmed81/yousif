
using ClassLibrary.domian;
namespace Library.ServicesInterfaces
{
    public interface IServiceService
    {
        void Delete(Service service);
        Service Get(int id);
        Service Get(string name);
        List<Service> GetList(string Price);
        void Save(Service service);
        void Update(Service service);
        List<Service> GetAll();
    }
}