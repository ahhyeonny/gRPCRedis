using Grpc.Net.Client;
using System.Windows;
using System.Windows.Input;
using Infrastructure.Grpc;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using RedisClient.model;

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

        public ObservableCollection<UserData> UserDatas
        {
            get { return _userDatas; }
            set
            {
                _userDatas = value;
                NotifyPropertyChanged();
            }
        }

        public UserData SelectUser
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
        public ICommand RefreshCommand { get; set; }

        private string _name = string.Empty;
        private string _id = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _saveResult = string.Empty;
        private ObservableCollection<UserData> _userDatas = new ObservableCollection<UserData>();
        private UserData _selectUser = new UserData();
        public MainViewModel()
        {
            InputCommand = new AsyncDelegateCommand(InputCommandAction);
            DeleteCommand = new AsyncDelegateCommand(DequeueCommandAction);
            RefreshCommand = new AsyncDelegateCommand(RefreshCommandAction);
        }

        private async Task InputCommandAction()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8080");
            var client = new GrpcCommunication.GrpcCommunicationClient(channel);
            var reply = await client.AddUserAsync(new UserInfo { Name = InputName, Email = InputEmail, Id = InputId, Password = InputPassword });
            DisplayAddResult(reply.Message);
            await InputClear();
            await RefreshCommandAction();
        }
        private async Task DequeueCommandAction()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8080");
            var client = new GrpcCommunication.GrpcCommunicationClient(channel);
            var reply = await client.DequeueUserAsync(new Empty());
            DisplayDeleteResult(reply.Message);
            await InputClear();
            await RefreshCommandAction();
        }
        private async Task RefreshCommandAction()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8080");
            var client = new GrpcCommunication.GrpcCommunicationClient(channel);
            var reply = await client.GetAllUsersAsync(new Empty());
            await GetAllList(reply.Message);
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

        private async Task GetAllList(string message)
        {
            UserDatas.Clear();
            if (string.IsNullOrEmpty(message))
                return;

            var users = message.Split('@');

            foreach(var user in users)
            {
                var values = user.Split(",");
                var name = values[0];
                var id = values[1];
                var email = values[2];
                var password = values[3];

                UserDatas.Add(new UserData(name, id, email, password));
            }
        }
    }

}