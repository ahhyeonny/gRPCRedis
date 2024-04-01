using Infrastructure.Grpc;

namespace GrpcService.Model
{
    public class UserData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserData(string id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public string JoinUserInfo()
        {
            return $"{Id},{Name},{Email},{Password}";
        }

    }
}
