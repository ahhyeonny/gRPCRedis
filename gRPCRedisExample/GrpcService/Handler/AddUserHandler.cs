using GrpcService.Services;
using MediatR;
using LanguageExt;
using GrpcService.Request;
using Infrastructure.Grpc;
using GrpcService.Command;
using GrpcService.Model;
using AutoMapper;

namespace GrpcService.Handler
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Option<string>>
    {
        private readonly RedisService _redisService;
        private readonly IMapper _mapper;

        public AddUserHandler(RedisService redisService, IMapper mapper)
        {
            _redisService = redisService;
            _mapper = mapper;
        }

        public async Task<Option<string>> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UserData>(command.request);
            return await _redisService.EnqueueAsync(entity) == true ? Option<string>.Some("Success") : Option<string>.None;
        }
    }
}
