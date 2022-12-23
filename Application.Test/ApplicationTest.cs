using Application.Abstractions.DataAccess;
using Application.Accounts;
using Application.Contracts.Accounts;
using Application.Contracts.Messages;
using Application.Contracts.Sessions;
using Application.Contracts.Workers;
using Application.Messages;
using Application.Sessions;
using Application.Workers;
using Application.ÑhainOfResponsibilities.MessageSourceModels;
using Application.ÑhainOfResponsibilities.MessegeModels;
using Application.ÑhainOfResponsibilities.WorkerModels;
using Domain.Messages;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
namespace Application.Test;

public class ApplicationTest
{
    private readonly IDatabaseContext _db;

    public ApplicationTest()
    {
        while (File.Exists("db/mydb.db"))
        {
            File.Delete("db/mydb.db");
        }
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
        s.Wait();
        Assert.Throws<AggregateException>(() => t.Handle(command, new CancellationToken()).Wait());
    }

    [Fact]
    public void HandleMessage()
    {
        var command = new CreateRootMaster.Command("maksim", "maksim", "maksim");
        var t = new CreateRootMasterHandler(_db);
        Task<CreateRootMaster.Response> s = t.Handle(command, new CancellationToken());
        s.Wait();
        var t1 = new CreateAccountHandler(_db);
        Task<CreateAccount.Response> s1 = t1.Handle(new CreateAccount.Command(0), new CancellationToken());
        s1.Wait();
        CreateAccount.Response res1 = s1.Result;
        var t2 = new CreateSourceHandler(_db);
        Task<CreateSource.Response> s2 = t2.Handle(new CreateSource.Command(res1.accountId, new PhoneMessageSourceModel("+79094417985")), new CancellationToken());
        s2.Wait();
        var t3 = new CreateMessageHandler(_db);
        Task<CreateMessage.Response> s3 = t3.Handle(new CreateMessage.Command(new PhoneMessageModel("+79094417985", "ss", "+79094417985")), new CancellationToken());
        s3.Wait();
        var t4 = new LogInHandler(_db);
        Task<LogIn.Response> s4 = t4.Handle(new LogIn.Command("maksim", "maksim"), new CancellationToken());
        s4.Wait();
        LogIn.Response res4 = s4.Result;
        var t5 = new CreateWorkerHandler(_db);
        Task<CreateWorker.Response> s5 = t5.Handle(new CreateWorker.Command(res4.sessionId, new SlaveWorkerModel("maksim2", 0), "maksim2", "maksim2"), new CancellationToken());
        s5.Wait();
        var t6 = new LogOutHandler(_db);
        Task<LogOut.Response> s6 = t6.Handle(new LogOut.Command(res4.sessionId), new CancellationToken());
        s6.Wait();
        Task<LogIn.Response> s7 = t4.Handle(new LogIn.Command("maksim2", "maksim2"), new CancellationToken());
        s7.Wait();
        LogIn.Response res7 = s7.Result;
        var t8 = new LoadMessagesHandler(_db);
        Task<LoadMessages.Response> s8 = t8.Handle(new LoadMessages.Command(res7.sessionId), new CancellationToken());
        s8.Wait();
        LoadMessages.Response res8 = s8.Result;
        var t9 = new HandleMessageHandler(_db);
        Task<HandleMessage.Response> s9 = t9.Handle(new HandleMessage.Command(res7.sessionId, res8.messages.messageIds.First()), new CancellationToken());
        s9.Wait();
        Assert.Equal(MessageState.Processed, _db.Messages.First().State);
    }
}