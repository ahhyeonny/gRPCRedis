using GrpcService.Services;
using MediatR;
using LanguageExt;

namespace GrpcService.Handler
{
    public class AddUserRequest : IRequest<Option<string>>
    {
        public UserInfo UserInfo { get; }

        public AddUserRequest(UserInfo userInfo)
        {
            UserInfo = userInfo;
        }
    }

    public class AddUserHandler : IRequestHandler<AddUserRequest, Option<string>>
    {
        private readonly GreeterService _greeterService;

        public AddUserHandler(GreeterService greeterService)
        {
            _greeterService = greeterService;
        }

        public async Task<Option<string>> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var entity = JoinUserInfo(request.UserInfo);
            return await _greeterService.EnqueueAsync(entity) == true ? Option<string>.Some("Success") : Option<string>.None;
        }

        public static string JoinUserInfo(UserInfo userInfo)
        {
            return $"ID : {userInfo.Id}, Name : {userInfo.Name}, Email : {userInfo.Email}, Password : {userInfo.Password}";
        }
    }
}
