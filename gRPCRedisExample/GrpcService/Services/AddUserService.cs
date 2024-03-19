using Grpc.Core;
using GrpcService.Handler;
using LanguageExt;
using MediatR;

namespace GrpcService.Services
{
    public class AddUserService : Greeter.GreeterBase
    {
        private readonly IMediator _mediator;

        public AddUserService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<HelloReply> AddUser(UserInfo request, ServerCallContext context)
        {
            //queue에 request 저장
            var result = await _mediator.Send(new AddUserRequest(request));
            if (result == Option<string>.None)
                return new HelloReply { Message = "Fail" };

            return new HelloReply { Message = "Success" };
        }
    }
}
