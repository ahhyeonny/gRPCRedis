using Grpc.Net.Client;
using System.Windows;
using System.Windows.Input;

namespace RedisClient
{
    public class MainViewModel : PropertyChangedHelper
    {
        public string InputName
        {
            get { return _name; }
            set { SetField(ref _name, value, "Name"); }
        }
        public string InputId
        {
            get { return _id; }
            set { SetField(ref _id, value, "Id"); }
        }
        public string InputEmail
        {
            get { return _email; }
            set { SetField(ref _email, value, "Email"); }
        }
        public string InputPassword
        {
            get { return _password; }
            set { SetField(ref _password, value, "Password"); }
        }
        public string SaveResult
        {
            get { return _saveResult; }
            set { SetField(ref _saveResult, value, "SaveResult"); }
        }

        public ICommand InputCommand { get; set; }
        private string _name = string.Empty;
        private string _id = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _saveResult = string.Empty;
        public MainViewModel()
        {
            InputCommand = new DelegateCommand(InputCommandAction);
        }
        private async void InputCommandAction()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5260");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.AddUserAsync(new UserInfo { Name = InputName, Email = InputEmail, Id = InputId, Password = InputPassword });
            DisplayResult(reply);
        }

        private void DisplayResult(HelloReply reply)
        {
            if (reply.Message == "Success")
                MessageBox.Show("Success to Save User Information.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Failed to Save User Information.", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}