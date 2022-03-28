using ResourceAPI.Entities;

namespace ResourceAPI.Services
{
    public interface IRepository
    {
        Task<Employee?> getEmployee(int id);

    }
}
