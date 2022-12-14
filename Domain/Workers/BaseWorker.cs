namespace Domain.Workers;

public abstract class BaseWorker
{
    public BaseWorker(string name, Guid id)
    {
        Name = name;
        Id = id;
    }

    public string Name { get; }
    public Guid Id { get; }
}
