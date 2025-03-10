using AutoMapper;
using Microsoft.EntityFrameworkCore;
using User.Management.API.Models;
using User.Management.API.Models.DTO;

namespace User.Management.API.Repositories
{
    public class StayRepository : IStayRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public StayRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Stay>> GetAllStays()
        {
            return await _context.Stays.ToListAsync();
        }

        public async Task<Stay> GetStayById(int stayId)
        {
            return await _context.Stays.FindAsync(stayId);
        }

        public async Task<IEnumerable<Stay>> GetStaysByUserId(string userId)
        {
            return await _context.Stays.Where(s => s.ApplicationUserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Stay>> GetStaysByCountry(string country)
        {
            return await _context.Stays.Where(s => s.Country == country).ToListAsync();
        }

        public async Task<IEnumerable<Stay>> GetStaysByCity(string city)
        {
            return await _context.Stays.Where(s => s.City == city).ToListAsync();
        }

        public async Task<Stay> CreateStay(StayDTO stayDTO)
        {
            var stay = _mapper.Map<Stay>(stayDTO);

            _context.Stays.Add(stay);
            await _context.SaveChangesAsync();

            return stay;
        }
        public async Task<IEnumerable<string?>> GetAllCityNames()
        {
            return await _context.Stays
                             .Select(s => s.City)
                             .Distinct()
                             .ToListAsync();
        }


        public async Task<Stay> DeleteStayById(int stayId)
        {
            var stay = await _context.Stays.FindAsync(stayId);
            if (stay != null)
            {
                _context.Stays.Remove(stay);
                await _context.SaveChangesAsync();
            }
            return stay;
        }



    }
}
