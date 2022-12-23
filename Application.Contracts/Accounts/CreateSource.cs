using Application.СhainOfResponsibilities.MessageSourceModels;
using MediatR;

namespace Application.Contracts.Accounts;

public static class CreateSource
{
    public record struct Command(Guid accountId, BaseMessageSourceModel model) : IRequest<Response>;
    public record struct Response();
}
