using Grpc.Core;
using GrpcService.Handler;
using GrpcService.Request;
using LanguageExt;
using MediatR;

namespace GrpcService.Services
{
    public class UserService : Greeter.GreeterBase
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<HelloReply> AddUser(UserInfo request, ServerCallContext context)
        {
            //queue에 request 저장
            var result = await _mediator.Send(new UserRequest(request));
            if (result == Option<string>.None)
                return new HelloReply { Message = "Fail" };

            return new HelloReply { Message = "Success" };
        }
    }
}
