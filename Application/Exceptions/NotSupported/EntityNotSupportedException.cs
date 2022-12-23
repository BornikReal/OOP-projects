namespace Application.Exceptions.NotSupported;

public class EntityNotSupportedException<T> : NotFoundException
{
    private EntityNotSupportedException(string? message) : base(message) { }
    
    public static EntityNotSupportedException<T> Create()
        => new EntityNotSupportedException<T>($"{typeof(T).Name} have unsupported type.");
}