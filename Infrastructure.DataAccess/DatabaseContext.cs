﻿using Application.Abstractions.DataAccess;
using Domain.Accounts;
using Domain.Messages;
using Domain.MessageSource;
using Domain.Workers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<BaseMessage> Messages { get; private init; } = null!;
    public DbSet<BaseMessageSource> MessageSources { get; private init; } = null!;
    public DbSet<Account> Accounts { get; private init; } = null!;
    public DbSet<BaseWorker> Workers { get; private init; } = null!;
    public DbSet<WorkerAuthenticator> WorkerAuthenticators { get; private init; } = null!;
    public DbSet<Session> ActiveSessions { get; private init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseMessage>()
            .ToTable("Messages")
            .HasDiscriminator<int>("ContractType")
            .HasValue<EmailMessage>(1)
            .HasValue<PhoneMessage>(2)
            .HasValue<MessengerMessage>(3);

        modelBuilder.Entity<BaseMessageSource>()
            .ToTable("MessageSources")
            .HasDiscriminator<int>("ContractType")
            .HasValue<EmailMessageSource>(1)
            .HasValue<PhoneMessageSource>(2)
            .HasValue<MessengerMessageSource>(3);

        modelBuilder.Entity<BaseWorker>()
            .ToTable("Employees")
            .HasDiscriminator<int>("ContractType")
            .HasValue<SlaveWorker>(1)
            .HasValue<MasterWorker>(2);
    }
}