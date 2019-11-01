using System;
using System.Threading.Tasks;

namespace DIPS.Xamarin.UI.Commands
{
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<Task> m_execute;
        private readonly Func<bool> m_canExecute;
        private readonly Action<Exception> m_onException;

        public AsyncCommand(Func<Task> execute, Action<Exception>? onExceptipon = null)
        {
            m_execute = execute;
            m_canExecute = () => true;
            m_onException = onExceptipon ?? (e => { });
        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute, Action<Exception>? onExceptipon = null)
        {
            m_execute = execute;
            m_canExecute = canExecute;
            m_onException = onExceptipon ?? (e => { });
        }

        public bool CanExecute(object parameter) => m_canExecute();

        public Task ExecuteAsync() => m_execute();

#pragma warning disable RECS0165
        public async void Execute(object parameter)
        {
            try
            {
                if (!CanExecute(parameter))
                {
                    return;
                }

                await ExecuteAsync();
            }
            catch (Exception exception)
            {
                m_onException(exception);
            }
        }
#pragma warning restore RECS0165

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        private readonly Func<T, Task> m_execute;
        private readonly Func<T, bool> m_canExecute;
        private readonly Action<Exception> m_onException;
        public AsyncCommand(Func<T, Task> execute, Action<Exception>? onExceptipon = null)
        {
            m_execute = execute;
            m_canExecute = value => true;
            m_onException = onExceptipon ?? (e => { });
        }

        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute, Action<Exception>? onExceptipon = null)
        {
            m_execute = execute;
            m_canExecute = canExecute;
            m_onException = onExceptipon ?? (e => { });
        }

        public bool CanExecute(object parameter)
        {
            if(!(parameter is T value))
            {
                return false;
            }

            return m_canExecute(value);
        }

        public Task ExecuteAsync(T value) => m_execute(value);

#pragma warning disable RECS0165
        public async void Execute(object parameter)
        {
            try
            {
                if(!(parameter is T value) || !m_canExecute(value))
                {
                    return;
                }

                await ExecuteAsync(value);
            }
            catch (Exception exception)
            {
                m_onException(exception);
            }
        }
#pragma warning restore RECS0165

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
