namespace Domain.Accounts;

public record class WorkerAuthenticator
{
    public string Login { get; }
    public string Password { get; }
    public WorkerAuthenticator(string login, string password)
    {
        Login = login;
        Password = password;
    }
}
