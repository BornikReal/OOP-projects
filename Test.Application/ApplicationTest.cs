using Application.Abstractions.DataAccess;
using Application.Contracts.Workers;
using Application.Workers;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace Test.Application;

public class ApplicationTest
{
    private readonly IDatabaseContext _db;

    public ApplicationTest()
    {
        File.Delete("db/mydb.db");
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        DbContextOptions<DatabaseContext> options = optionsBuilder.UseSqlite("Data Source=db/mydb.db").Options;
        _db = new DatabaseContext(options);
    }

    [Fact]
    public void CreateTwoRootMasters()
    {
        var command = new CreateRootMaster.Command("maksim", "maksim", "maksim");
        var t = new CreateRootMasterHandler(_db);
        Task<CreateRootMaster.Response> s = t.Handle(command, new CancellationToken());
        s.Start();
        CreateRootMaster.Response ss = s.Result;
        Assert.Equal(ss.managerId, _db.Workers.First().Id);
    }
}