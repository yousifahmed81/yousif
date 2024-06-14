
using ClassLibrary.domian;
namespace Library.ServicesInterfaces
{
    public interface IServiceService
    {
        Task Delete(Service service);
        Task<Service> Get(int id);
        Task<Service> Get(string name);
        Task<List<Service>> GetList(string Price);
        Task Save(Service service);
        Task Update(Service service);
        Task<List<Service>> GetAll();
    }
}