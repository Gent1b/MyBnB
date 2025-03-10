using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Management.API;
using User.Management.API.Models;
using User.Management.API.Models.DTO;
using User.Management.API.Repositories;
using User.Management.API.Services;

namespace User.Management.UnitTesting.ServiceTests
{
    [Collection("Non-Parallel Collection")]

    public class StayServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public StayServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;
        }

        private ApplicationDbContext CreateContext() => new ApplicationDbContext(_options);

        [Fact]
        public async Task GetAllStays_ReturnsAllStaysDto()
        {
            // Arrange
            var options = _options;

            // Insert seed data into the in-memory database
            using (var context = new ApplicationDbContext(options))
            {
                context.Stays.Add(new Stay { StayId = 1, Name = "Beach House", Country = "USA", City = "Miami", ImageUrl = "image1.jpg", MaxGuests = 4, ApplicationUserId = "abc123" });
                context.Stays.Add(new Stay { StayId = 2, Name = "Mountain Cabin", Country = "Canada", City = "Banff", ImageUrl = "image2.jpg", MaxGuests = 5, ApplicationUserId = "def456" });
                await context.SaveChangesAsync();
            }

            // Mock the IMapper interface and setup the map from Stay to StayDTO
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<List<StayDTO>>(It.IsAny<List<Stay>>()))
                .Returns((List<Stay> source) =>
                {
                    return source.Select(s => new StayDTO
                    {
                        // Assuming you have similar fields in your DTO
                        Name = s.Name,
                        Country = s.Country,
                        City = s.City,
                        ImageUrl = s.ImageUrl,
                        MaxGuests = s.MaxGuests
                    }).ToList();
                });

            // Use the context with the seeded data for the repository
            using (var context = new ApplicationDbContext(options))
            {
                var stayRepository = new StayRepository(context, mockMapper.Object);
                var stayService = new StayService(stayRepository, context, mockMapper.Object);

                // Act
                var result = await stayService.GetAllStays();

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
                Assert.Equal("Stays retrieved successfully", result.Message);
                Assert.Equal(2, result.Data.Count); // We added two Stays above
                Assert.Equal("Beach House", result.Data.First().Name); // Checking the first item as an example
            }
        }
        [Fact]
        public async Task CreateStay_ReturnsCreatedStayDto()
        {
            // Arrange
            var options = _options;

            var mockMapper = new Mock<IMapper>();

            // Setup the mapper to simulate the mapping from StayDTO to Stay and vice versa
            mockMapper.Setup(mapper => mapper.Map<Stay>(It.IsAny<StayDTO>()))
                .Returns((StayDTO stayDTO) => new Stay
                {
                    // Assign properties from stayDTO to Stay
                    Name = stayDTO.Name,
                    Country = stayDTO.Country,
                    City = stayDTO.City,
                    ImageUrl = stayDTO.ImageUrl,
                    MaxGuests = stayDTO.MaxGuests.HasValue ? stayDTO.MaxGuests.Value : 0,
                    ApplicationUserId = stayDTO.ApplicationUserId
                });

            mockMapper.Setup(mapper => mapper.Map<StayDTO>(It.IsAny<Stay>()))
                .Returns((Stay stay) => new StayDTO
                {
                    // Assign properties from Stay to StayDTO
                    Name = stay.Name,
                    Country = stay.Country,
                    City = stay.City,
                    ImageUrl = stay.ImageUrl,
                    MaxGuests = stay.MaxGuests,
                    ApplicationUserId = stay.ApplicationUserId
                });

            using (var context = new ApplicationDbContext(options))
            {
                var stayService = new StayService(new StayRepository(context, mockMapper.Object), context, mockMapper.Object);

                var newStayDto = new StayDTO
                {
                    // Initialize the StayDTO with test data
                    Name = "New Stay",
                    Country = "New Country",
                    City = "New City",
                    ImageUrl = "newimage.jpg",
                    MaxGuests = 3,
                    ApplicationUserId = "newuser123"
                };

                // Act
                var result = await stayService.CreateStay(newStayDto);

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Success);
                Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
                Assert.Equal("Stay created successfully", result.Message);
                Assert.NotNull(result.Data);
                Assert.Equal(newStayDto.Name, result.Data.Name); // Ensure the data matches what was sent
                                                                 // Additional assertions to verify other properties
            }
        }


    }
}
