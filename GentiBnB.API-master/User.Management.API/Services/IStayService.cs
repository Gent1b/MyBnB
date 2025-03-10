using System.Collections.Generic;
using System.Threading.Tasks;
using User.Management.API.Models;
using User.Management.API.Models.DTO;

namespace User.Management.API.Services
{
    public interface IStayService
    {
        Task<ApiResponse<List<StayDTO>>> GetAllStays();
        Task<ApiResponse<StayDTO>> GetStayById(int stayId);
        Task<ApiResponse<List<StayDTO>>> GetStaysByUserId(string userId);
        Task<ApiResponse<List<StayDTO>>> GetStaysByCountry(string country);
        Task<ApiResponse<List<StayDTO>>> GetStaysByCity(string city);
        Task<ApiResponse<StayDTO>> CreateStay(StayDTO stayDTO);
        Task<ApiResponse<StayDTO>> DeleteStayById(int stayId);
        Task<ApiResponse<List<string>>> GetAllCityNames();

    }
}
