using Polly;
using StackExchange.Redis;

namespace RedisLibrary
{
    public interface IConnectionFactory
    {
        public IConfiguration Configuration { get; set; }
        public IConnectionMultiplexer CreateConnection();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        public IConfiguration Configuration { get; set; }

        public ConnectionFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConnectionMultiplexer CreateConnection()
        {
            var retryPolicy = Policy
                .Handle<RedisConnectionException>()
                .Retry(3, (exception, retryCount) =>
                {
                    Console.WriteLine($"Redis 연결 시도 {retryCount}번 실패: {exception.Message}");
                });

            return retryPolicy.Execute(() => ConnectionMultiplexer.Connect(Configuration.GetAddress()));
        }
    }
}
