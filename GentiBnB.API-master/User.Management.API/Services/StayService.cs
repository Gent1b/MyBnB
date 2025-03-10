using User.Management.API.Repositories;
using User.Management.API.Models;
using User.Management.API.Models.DTO;
using AutoMapper;

namespace User.Management.API.Services
{
    public class StayService : IStayService
    {
        private readonly IStayRepository _stayRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StayService(IStayRepository stayRepository, ApplicationDbContext context,IMapper mapper)
        {
            _stayRepository = stayRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<StayDTO>>> GetAllStays()
        {
            try
            {
                var stays = await _stayRepository.GetAllStays();
                var stayDTOs = _mapper.Map<List<StayDTO>>(stays);
                return new ApiResponse<List<StayDTO>>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Stays retrieved successfully",
                    Data = stayDTOs
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<StayDTO>>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving stays",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<StayDTO>> GetStayById(int stayId)
        {
            try
            {
                var stay = await _stayRepository.GetStayById(stayId);
                if (stay != null)
                {
                    var stayDTO = _mapper.Map<StayDTO>(stay);
                    return new ApiResponse<StayDTO>
                    {
                        Success = true,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Stay retrieved successfully",
                        Data = stayDTO
                    };
                }
                else
                {
                    return new ApiResponse<StayDTO>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Stay not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<StayDTO>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving stay",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<List<StayDTO>>> GetStaysByUserId(string userId)
        {
            try
            {
                var stays = await _stayRepository.GetStaysByUserId(userId);
                var stayDTOs = _mapper.Map<List<StayDTO>>(stays);
                return new ApiResponse<List<StayDTO>>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Stays retrieved successfully",
                    Data = stayDTOs
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<StayDTO>>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving stays",
                    Error = ex.Message
                };
            }
        }


        public async Task<ApiResponse<List<StayDTO>>> GetStaysByCountry(string country)
        {
            try
            {
                var stays = await _stayRepository.GetStaysByCountry(country);
                var stayDTOs = _mapper.Map<List<StayDTO>>(stays);
                return new ApiResponse<List<StayDTO>>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Stays retrieved successfully",
                    Data = stayDTOs
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<StayDTO>>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving stays",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<List<StayDTO>>> GetStaysByCity(string city)
        {
            try
            {
                var stays = await _stayRepository.GetStaysByCity(city);
                var stayDTOs = _mapper.Map<List<StayDTO>>(stays);
                return new ApiResponse<List<StayDTO>>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Stays retrieved successfully",
                    Data = stayDTOs
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<StayDTO>>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving stays",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<StayDTO>> CreateStay(StayDTO stayDTO)
        {
            try
            {
                // Map StayDTO to Stay after accepting the stayDTO
                var stay = _mapper.Map<Stay>(stayDTO);

                _context.Stays.Add(stay);
                await _context.SaveChangesAsync();

                // Maping back the Stay to StayDTO for returning
                var createdStayDTO = _mapper.Map<StayDTO>(stay);

                return new ApiResponse<StayDTO>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Stay created successfully",
                    Data = createdStayDTO 
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<StayDTO>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error creating stay",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<StayDTO>> DeleteStayById(int stayId)
        {
            try
            {
                var stay = await _context.Stays.FindAsync(stayId);
                if (stay != null)
                {
                    _context.Stays.Remove(stay);
                    await _context.SaveChangesAsync();

                    var deletedStayDTO = _mapper.Map<StayDTO>(stay);

                    return new ApiResponse<StayDTO>
                    {
                        Success = true,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Stay deleted successfully",
                        Data = deletedStayDTO 
                    };
                }
                else
                {
                    return new ApiResponse<StayDTO>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Stay not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<StayDTO>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error deleting stay",
                    Error = ex.Message
                };
            }
        }
        public async Task<ApiResponse<List<string>>> GetAllCityNames()
        {
            try
            {
                var cityNames = await _stayRepository.GetAllCityNames();
                return new ApiResponse<List<string>>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "City names retrieved successfully",
                    Data = cityNames.ToList()
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<string>>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving city names",
                    Error = ex.Message
                };
            }
        }

    }

}
