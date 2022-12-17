namespace Domain.Accounts;

public record class Session
{
    public Guid WorkerId { get; }
    public Guid SessionId { get; }

    public Session(Guid workerId, Guid sessionId)
    {
        WorkerId = workerId;
        SessionId = sessionId;
    }

    protected Session() { }
}
