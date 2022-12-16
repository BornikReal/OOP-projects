using Application.Contracts.SourceFactory;
using MediatR;

namespace Application.Contracts.Accounts;

public static class CreateSource
{
    public record struct Command(Guid account, BaseMessageSourceFactory factory) : IRequest<Response>;
    public record struct Response();
}
