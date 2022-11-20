using Nordic_Door.Shared.Models.API;
using Nordic_Door.Server.Data;
using NordicDoor.Server.Controllers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Nordic_Door.Shared.Models.Database;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace NordicDoor.Tests;

public class AuthControllerTests
{
    // setup av inmemory dbcontext
    private async Task<NordicDoorsDbContext> setup()
    {
        var option = new DbContextOptionsBuilder<NordicDoorsDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;
        var dbContext = new NordicDoorsDbContext(option);
        await seed(dbContext);
        return dbContext;
    }

    // setup av inmemory data i dbcontext
    private async Task seed(NordicDoorsDbContext databaseContext)
    {
            databaseContext.Employees.Add(
                new Employee()
                {
                    Id = 1,
                    Name = "TestUser",
                    Email =  "TestUser@TestEmail.no",
                    Password = "AMA8L1hXYPGoNsmWBMNAD4rH/cyx+C+0PrmUw2Ly6UbtCrrgkXUp52rRYRYGY3feQA==",
                    IsAdmin = 0,
                });
            databaseContext.Teams.Add(
                new Team()
                {
                    Id = 1,
                    Name = "TestTeam"
                });
            databaseContext.UserTeams.Add(
                new UserTeam()
                {
                    EmployeeId = 1,
                    Role = "MedArbeider",
                    TeamId = 1,
                });
        await databaseContext.SaveChangesAsync();
    }
  

    [Fact]
    public async Task AuthController_AuthUsernameAndPassword_ReturnOkObjectResult()
    {
        // Arrange
        var dbContext = await setup();
        var authControll = new AuthController(dbContext);

        var login = new LoginRequest()
        {
            Email = "TestUser@TestEmail.no",
            Password = "123",
        };

        // Act
        var result = await authControll.AuthUsernameAndPassword(login);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }


    [Fact]
    public async Task AuthController_CreateUser_ReturnOkResult()
    {
        //Arrange
        var dbContext = await setup();
        var authControll = new AuthController(dbContext);

        var newUser = new AddUserRequest()
        {
            Name = "TestUser2",
            Email = "TestUser2@Test.no",
            Password = "123",
            IsAdmin = false,
            TeamNames = new List<string>{ "TestTeam" },
        };
        
        //Act
        var result = await authControll.CreateUser(newUser);

        //Assert
        result.Should().BeOfType<OkResult>();
    }

}
