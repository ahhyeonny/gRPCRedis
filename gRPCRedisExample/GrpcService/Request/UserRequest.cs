using LanguageExt;
using MediatR;

namespace GrpcService.Request
{
    public class UserRequest : IRequest<Option<string>> //Record
    {
        public UserInfo UserInfo { get; }

        public UserRequest(UserInfo userInfo)
        {
            UserInfo = userInfo;
        }
    }

}
