using GrpcService.Services;
using MediatR;
using LanguageExt;
using GrpcService.Request;

namespace GrpcService.Handler
{
    public class AddUserHandler : IRequestHandler<UserRequest, Option<string>>
    {
        private readonly Services.RedisService _redisService;

        public AddUserHandler(Services.RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Option<string>> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            var entity = JoinUserInfo(request.UserInfo);
            return await _redisService.EnqueueAsync(entity) == true ? Option<string>.Some("Success") : Option<string>.None;
        }

        private string JoinUserInfo(UserInfo userInfo) //private로 해야됨
        {
            return $"ID : {userInfo.Id}, Name : {userInfo.Name}, Email : {userInfo.Email}, Password : {userInfo.Password}";
        }
    }
}
