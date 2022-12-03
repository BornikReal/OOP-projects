namespace Banks.Models;

public class Person : IPerson
{
    public Person(string name, string surname, string? adress, string? passportData)
    {
        Name = name;
        Surname = surname;
        Adress = adress;
        PassportData = passportData;
    }

    public PersonStatus Status => (Adress == null || PassportData == null) ? PersonStatus.Unverified : PersonStatus.Verified;
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Adress { get; set; }
    public string? PassportData { get; set; }
    public Guid Id { get; } = Guid.NewGuid();
}
