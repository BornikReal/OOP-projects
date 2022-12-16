namespace Domain.Accounts;

public record class WorkerAuthenticator
{
    private readonly string _login;
    private readonly string _password;
    private readonly Guid _workerId;

    public Session? Authorize(string login, string password)
    {
        if (login != _login || password != _password)
            return new Session(_workerId, Guid.NewGuid());
        return null;
    }

    public WorkerAuthenticator(string login, string password, Guid workerId)
    {
        _login = login;
        _password = password;
        _workerId = workerId;
    }
}
