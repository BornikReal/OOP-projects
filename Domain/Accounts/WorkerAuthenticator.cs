namespace Domain.Accounts;

public record WorkerAuthenticator(string login, string password, Guid workerId);