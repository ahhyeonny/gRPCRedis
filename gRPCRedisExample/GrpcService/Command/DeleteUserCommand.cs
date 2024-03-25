using GrpcService.Request;
using Infrastructure.Grpc;
using LanguageExt;
using MediatR;

namespace GrpcService.Command
{
    public record DeleteUserCommand(UserInfo request) : IRequest<Option<string>>;
}
