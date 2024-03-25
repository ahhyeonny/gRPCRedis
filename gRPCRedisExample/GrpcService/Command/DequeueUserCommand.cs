using GrpcService.Request;
using Infrastructure.Grpc;
using LanguageExt;
using MediatR;

namespace GrpcService.Command
{
    public record DequeueUserCommand() : IRequest<string>;
}
