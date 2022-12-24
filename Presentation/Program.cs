//using Application.Accounts;
//using Application.Contracts.Accounts;
//using Application.ChainOfResponsibilities.MessageSourceModels;
//using Infrastructure.DataAccess;
//using Microsoft.EntityFrameworkCore;

//var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
//DbContextOptions<DatabaseContext> options = optionsBuilder.UseSqlite("Data Source=C:\\Users\\cooln\\source\\repos\\OOP\\Presentation\\bin\\Debug\\net6.0\\db\\mydb.db").Options;
//var db = new DatabaseContext(options);

//var t1 = new CreateAccountHandler(db);
//Task<CreateAccount.Response> s1 = t1.Handle(new CreateAccount.Command(0), new CancellationToken());
//s1.Wait();
//CreateAccount.Response res1 = s1.Result;
//var t2 = new CreateSourceHandler(db);
//Task<CreateSource.Response> s2 = t2.Handle(new CreateSource.Command(res1.accountId, new EmailMessageSourceModel("cool.nikolay@gmail.com")), new CancellationToken());
//s2.Wait();

using Application.Extensions;
using Infrastructure.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddDataAccess(x => x.UseLazyLoadingProxies().UseSqlite("Data Source=C:\\Users\\cooln\\source\\repos\\OOP\\Presentation\\bin\\Debug\\net6.0\\db\\mydb.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();