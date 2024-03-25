using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RedisClient
{
    public class AsyncDelegateCommand : ICommand
    {
        private readonly Func<Task<bool>> canExecute;
        private readonly Func<Task> execute;

        public AsyncDelegateCommand(Func<Task> execute) : this(execute, null)
        {
        }

        public AsyncDelegateCommand(Func<Task> execute, Func<Task<bool>> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;

            return canExecute().GetAwaiter().GetResult();
        }

        public async void Execute(object parameter)
        {
            await execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
