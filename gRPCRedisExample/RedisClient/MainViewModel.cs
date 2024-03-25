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
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        public string InputId
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        public string InputEmail
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged();
            }
        }
        public string InputPassword
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyPropertyChanged();
            }
        }
        public string SaveResult
        {
            get { return _saveResult; }
            set
            {
                _saveResult = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand InputCommand { get; set; }
        private string _name = string.Empty;
        private string _id = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _saveResult = string.Empty;
        public MainViewModel()
        {
            InputCommand = new AsyncDelegateCommand(InputCommandAction);
        }

        private async Task InputCommandAction()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5260");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.AddUserAsync(new UserInfo { Name = InputName, Email = InputEmail, Id = InputId, Password = InputPassword });
            DisplayResult(reply);
            await InputClear();
        }

        private void DisplayResult(HelloReply reply)
        {
            if (reply.Message == "Success")
                MessageBox.Show("Success to Save User Information.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Failed to Save User Information.", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async Task InputClear()
        {
            InputName = string.Empty;
            InputId = string.Empty;
            InputPassword = string.Empty;
            InputEmail = string.Empty;
            await Task.CompletedTask;
        }
    }

}