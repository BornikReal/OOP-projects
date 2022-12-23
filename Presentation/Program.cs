using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
DbContextOptions<DatabaseContext> options = optionsBuilder.UseSqlite("Data Source=db/mydb.db").Options;
var db = new DatabaseContext(options);

db.WorkerAuthenticators.Add(new Domain.Accounts.WorkerAuthenticator("maskim", "maksim", Guid.NewGuid()));
db.SaveChanges();

Console.WriteLine(db.WorkerAuthenticators.FirstOrDefault(x => x.login == "maskim" && x.password == "maksim")!.login);

//BaseMessageSource mes = db.MessageSources.First();
//Console.WriteLine(mes.Messages.Count);

//var message = new EmailMessage("cool.nikolay@gmail.com", "dsada", "vvdd", Guid.NewGuid(), "dsa");
//var source = new EmailMessageSource(Guid.NewGuid(), "cool.nikolay@gmail.com");
//source.AddMessage(message);
//db.Messages.Add(message);
//db.MessageSources.Add(source);
//db.SaveChanges();

//MasterWorker master = db.Workers.OfType<MasterWorker>().First();
//db.Entry(master).Collection(e => e.Slaves).Query().Load();
//Console.WriteLine(((MasterWorker)master.Slaves.First()).Slaves.Count);

//var manager = new MasterWorker("maksim", Guid.NewGuid(), 1);
//var manager2 = new MasterWorker("maksim2", Guid.NewGuid(), 1);
//var slave = new SlaveWorker("asdsa", Guid.NewGuid(), 2);
//manager2.AddWorker(slave);
//manager.AddWorker(manager2);
//db.Workers.Add(slave);
//db.Workers.Add(manager2);
//db.Workers.Add(manager);
//db.SaveChanges();