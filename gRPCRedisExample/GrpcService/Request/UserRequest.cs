using LanguageExt;
using MediatR;
using Infrastructure.Grpc;

namespace GrpcService.Request
{
    public record class UserRequest : IRequest<Option<string>>
    {
        public UserInfo UserInfo { get; }

        public UserRequest(UserInfo userInfo)
        {
            UserInfo = userInfo;
        }
    }

}
