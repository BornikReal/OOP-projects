namespace Domain.Accounts;

public record class Session
{
    public Guid WorkerId { get; protected init; }
    public Guid Id { get; protected init; }

    public Session(Guid workerId, Guid sessionId)
    {
        WorkerId = workerId;
        Id = sessionId;
    }

    protected Session() { }
}
