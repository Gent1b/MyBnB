using User.Management.API.Models;
using User.Management.API.Models.DTO;

namespace User.Management.API.Repositories
{
    public interface IStayRepository
    {
        Task<IEnumerable<Stay>> GetAllStays();
        Task<Stay> GetStayById(int stayId);
        Task<IEnumerable<Stay>> GetStaysByUserId(string userId);
        Task<IEnumerable<Stay>> GetStaysByCountry(string country);
        Task<IEnumerable<Stay>> GetStaysByCity(string city);
        Task<Stay> CreateStay(StayDTO stayDTO);
        Task<Stay> DeleteStayById(int stayId);
        Task<IEnumerable<string>> GetAllCityNames();

    }
}
