using Domain.Accounts;
using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.DataAccess;

public interface IDatabaseContext
{
    DbSet<BaseMessage> Messages { get; }
    DbSet<BaseMessageSource> MessageSources { get; }
    DbSet<Account> Accounts { get; }
    DbSet<BaseWorker> Workers { get; }
    DbSet<WorkerAuthenticator> WorkerAuthenticators { get; }
    DbSet<Session> ActiveSessions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}