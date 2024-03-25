using Grpc.Core;
using GrpcService.Handler;
using GrpcService.Request;
using LanguageExt;
using MediatR;
using Infrastructure.Grpc;

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
            var result = await _mediator.Send(new UserRequest(request));
            if (result == Option<string>.None)
                return new GrpcCommunicationResult { Message = "Fail" };

            return new GrpcCommunicationResult { Message = "Success" };
        }
    }
}
