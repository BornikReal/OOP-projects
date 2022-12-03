namespace Banks.Models;

public interface IPerson
{
    Guid Id { get; }
    PersonStatus Status { get; }
    string Name { get; set; }
    string Surname { get; set; }
    string? Adress { get; set; }
    string? PassportData { get; set; }
}
