using MediatR;

namespace GrpcService.Command
{
    public record GetAllUsersCommand() : IRequest<string>;
}
