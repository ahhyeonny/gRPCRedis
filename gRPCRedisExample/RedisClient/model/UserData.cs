using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClient.model
{
    public class UserData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserData() { }

        public UserData(string id, string name, string email, string password) 
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

    }
}
