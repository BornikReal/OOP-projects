using Banks.Models;

namespace Banks.PersonBuilder;

public class DefaultPersonBuilder : IPersonBuilder
{
    private string? _name;
    private string? _surname;
    private string? _adress;
    private string? _passportData;

    public IPersonBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public IPersonBuilder SetSurname(string surname)
    {
        _surname = surname;
        return this;
    }

    public IPersonBuilder SetAdress(string adress)
    {
        _adress = adress;
        return this;
    }

    public IPersonBuilder SetPassportData(string phone)
    {
        _passportData = phone;
        return this;
    }

    public IPerson Build()
    {
        if (_name == null || _surname == null)
            throw new Exception();
        return new Person(_name, _surname, _adress, _passportData);
    }
}
