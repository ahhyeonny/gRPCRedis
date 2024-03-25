using Grpc.Net.Client;
using System.Windows;
using System.Windows.Input;
using Infrastructure.Grpc;
using System.Collections.ObjectModel;

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

        public ObservableCollection<UserInfo> UserInfos
        {
            get { return _userInfos; }
            set
            {
                _userInfos = value;
                NotifyPropertyChanged();
            }
        }

        public UserInfo SelectUser
        {
            get { return _selectUser; }
            set
            {
                _selectUser = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand InputCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private string _name = string.Empty;
        private string _id = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _saveResult = string.Empty;
        private ObservableCollection<UserInfo> _userInfos = new ObservableCollection<UserInfo>();
        private UserInfo _selectUser = new UserInfo();
        public MainViewModel()
        {
            InputCommand = new AsyncDelegateCommand(InputCommandAction);
            DeleteCommand = new AsyncDelegateCommand(DeleteCommandAction);
        }

        private async Task InputCommandAction()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5260");
            var client = new GrpcCommunication.GrpcCommunicationClient(channel);
            var reply = await client.AddUserAsync(new UserInfo { Name = InputName, Email = InputEmail, Id = InputId, Password = InputPassword });
            DisplayAddResult(reply.Message);
            await InputClear();
        }
        private async Task DeleteCommandAction()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5260");
            var client = new GrpcCommunication.GrpcCommunicationClient(channel);
            var reply = await client.DequeueUserAsync(new UserInfo { Name = InputName, Email = InputEmail, Id = InputId, Password = InputPassword });
            DisplayDeleteResult(reply.Message);
            await InputClear();
        }

        private void DisplayAddResult(string replyMessage)
        {
            if (replyMessage == "Success")
                MessageBox.Show("Success", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Fail", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DisplayDeleteResult(string replyMessage)
        {
            
            MessageBox.Show($"The deleted queue:\n{replyMessage}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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