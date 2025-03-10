using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using User.Management.API.Models;
using User.Management.API.Repositories;
using User.Management.API;

namespace User.Management.UnitTesting.RepositoryTests
{
    [Collection("Non-Parallel Collection")]

    public class StayRepositoryTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public StayRepositoryTest()
        {
            // Create a new set of options for each test
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
        .Options;
        }
        [Fact]
        public async Task GetAllStays_ReturnsAllStays()
        {
            using var context = new ApplicationDbContext(_options);

            context.Stays.Add(new Stay {
                StayId = 1,
                Name = "Test Stay 1",
                Country = "Test Country",
                City = "Test City",
                ImageUrl = "www.gentiphoto.com",
                MaxGuests = 2,
                ApplicationUserId = "user1"
            });
            context.Stays.Add(new Stay {
                StayId = 2,
                Name = "Test Stay 2",
                Country = "Test Country",
                City = "Test City",
                ImageUrl = "www.gentiphoto.com",
                MaxGuests = 4,
                ApplicationUserId = "user2"
            });
            await context.SaveChangesAsync();

            var mockMapper = new Mock<IMapper>();
            var repository = new StayRepository(context, mockMapper.Object);

            var result = await repository.GetAllStays();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count()); // we aded 2 stays
        }

        [Fact]
        public async Task GetStayById_ReturnsStay()
        {
            using var context = new ApplicationDbContext(_options);

            //add stays
            var stay = new Stay { 
                StayId = 1,
                Name = "Test Stay 1",
                Country = "Test Country",
                City = "Test City",
                ImageUrl = "www.gentiphoto.com",
                MaxGuests = 2,
                ApplicationUserId = "user1"
            };
            context.Stays.Add(stay);
            await context.SaveChangesAsync();

            var mockMapper = new Mock<IMapper>();
            var repository = new StayRepository(context, mockMapper.Object);

            var result = await repository.GetStayById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.StayId);
        }

        [Fact]
public async Task GetStaysByUserId_ReturnsStaysForUser()
{
    using var context = new ApplicationDbContext(_options);
    var userId = "user123"; 

    // add stays to db
    var stays = new List<Stay>
    {
        new Stay { StayId = 1, ApplicationUserId = userId,Name = "Test Stay 1",
                Country = "Test Country",
                City = "Test City",
                ImageUrl = "www.gentiphoto.com",
                MaxGuests = 2},
        new Stay { StayId = 2, ApplicationUserId = userId, Name = "Test Stay 1",
                Country = "Test Country",
                City = "Test City",
                ImageUrl = "www.gentiphoto.com",
                MaxGuests = 2, },
        new Stay { StayId = 3, ApplicationUserId = "otherUser", Name = "Test Stay 1",
                Country = "Test Country",
                City = "Test City",
                ImageUrl = "www.gentiphoto.com",
                MaxGuests = 2, } 
    };

    context.Stays.AddRange(stays);
    await context.SaveChangesAsync();

    var mockMapper = new Mock<IMapper>();
    var repository = new StayRepository(context, mockMapper.Object);

    var result = await repository.GetStaysByUserId(userId);

    Assert.NotNull(result);
    var resultList = result.ToList();
    Assert.Equal(2, resultList.Count); // there should be 2 stays returned sicne they have the userId
    Assert.All(resultList, s => Assert.Equal(userId, s.ApplicationUserId)); // all stays should have the correct userId
}


    }
}
