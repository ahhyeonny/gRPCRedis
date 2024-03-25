using GrpcService.Command;
using GrpcService.Services;
using MediatR;

namespace GrpcService.Handler
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, string>
    {
        private readonly RedisService _redisService;

        public GetAllUsersHandler(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<string> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
        {
            var allUsers = await _redisService.GetAllItems();
            return MergeAllLists(allUsers);
        }

        private string MergeAllLists(IList<string> allUsers) 
        {
            if(allUsers.Count == 0) 
                return string.Empty;
            return string.Join("@", allUsers);
        }
    }

}
