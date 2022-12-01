namespace Banks.Models;

public interface IPerson
{
    PersonStatus Status { get; }
    string Name { get; }
    string Surname { get; }
    string? Adress { get; }
    string? PassportData { get; }
}
