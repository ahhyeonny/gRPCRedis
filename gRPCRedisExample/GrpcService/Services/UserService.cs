using Grpc.Core;
using GrpcService.Handler;
using GrpcService.Request;
using LanguageExt;
using MediatR;
using Infrastructure.Grpc;
using GrpcService.Command;

namespace GrpcService.Services
{
    public class UserService : GrpcCommunication.GrpcCommunicationBase
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<GrpcCommunicationResult> AddUser(UserInfo request, ServerCallContext context)
        {
            //queue에 request 저장
            var result = await _mediator.Send(new AddUserCommand(request));
            if (result == Option<string>.None)
                return new GrpcCommunicationResult { Message = "Fail" };

            return new GrpcCommunicationResult { Message = "Success" };
        }
        public override async Task<RedisDeleteResult> DeleteUser(UserInfo request, ServerCallContext context)
        {
            //queue에 request 삭제
            var result = await _mediator.Send(new DeleteUserCommand(request));
            if (result == Option<string>.None)
                return new RedisDeleteResult { Message = "Fail" };

            return new RedisDeleteResult { Message = "Success" };
        }
    }
}
