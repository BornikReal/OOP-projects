using Banks.Models;

namespace Banks.PersonBuilder;

public interface IPersonBuilder
{
    IPersonBuilder SetName(string name);
    IPersonBuilder SetSurname(string surname);
    IPersonBuilder SetAdress(string adress);
    IPersonBuilder SetPassportData(string phone);
    IPerson Build();
}
