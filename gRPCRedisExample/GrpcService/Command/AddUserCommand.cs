using GrpcService.Request;
using Infrastructure.Grpc;
using LanguageExt;
using MediatR;

namespace GrpcService.Command
{
    public record AddUserCommand(UserInfo request) : IRequest<Option<string>>;
}
