using MediatR;

namespace Application.Contracts.Workers;

public static class CreateRootMaster
{
    public record struct Command(string name, string login, string password) : IRequest<Response>;

    public record struct Response();
}
