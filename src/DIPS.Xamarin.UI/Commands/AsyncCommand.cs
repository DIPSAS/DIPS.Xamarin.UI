using System;
using System.Threading.Tasks;

namespace DIPS.Xamarin.UI.Commands
{
    /// <inheritdoc />
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<Task> m_execute;
        private readonly Func<bool> m_canExecute;
        private readonly Action<Exception> m_onException;

        /// <summary>
        /// Constructs an <see cref="AsyncCommand"/>
        /// </summary>
        /// <param name="execute">The task to execute</param>
        /// <param name="onException">The action to run if the async command throws outside of the task</param>
        public AsyncCommand(Func<Task> execute, Action<Exception>? onException = null)
        {
            m_execute = execute;
            m_canExecute = () => true;
            m_onException = onException ?? (e => { });
        }

        /// <summary>
        /// Constructs an <see cref="AsyncCommand"/>
        /// </summary>
        /// <param name="execute">The task to execute</param>
        /// <param name="canExecute">An func to determine if the command can execute</param>
        /// <param name="onException">The action to run if the async command throws outside of the task</param>
        public AsyncCommand(Func<Task> execute, Func<bool> canExecute, Action<Exception>? onException = null)
        {
            m_execute = execute;
            m_canExecute = canExecute;
            m_onException = onException ?? (e => { });
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter) => m_canExecute();

        /// <inheritdoc />
        public Task ExecuteAsync() => m_execute();

#pragma warning disable RECS0165
        /// <inheritdoc />
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

        /// <inheritdoc />
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public event EventHandler? CanExecuteChanged;
    }

    /// <inheritdoc />
    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        private readonly Func<T, Task> m_execute;
        private readonly Func<T, bool> m_canExecute;
        private readonly Action<Exception> m_onException;
        /// <summary>
        /// Constructs an <see cref="AsyncCommand{T}"/>
        /// </summary>
        /// <param name="execute">The task to execute</param>
        /// <param name="onException">The action to run if the async command throws outside of the task</param>
        public AsyncCommand(Func<T, Task> execute, Action<Exception>? onException = null)
        {
            m_execute = execute;
            m_canExecute = value => true;
            m_onException = onException ?? (e => { });
        }

        /// <summary>
        /// Constructs an <see cref="AsyncCommand{T}"/>
        /// </summary>
        /// <param name="execute">The task to execute</param>
        /// <param name="canExecute">An func to determine if the command can execute</param>
        /// <param name="onException">The action to run if the async command throws outside of the task</param>
        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute, Action<Exception>? onException = null)
        {
            m_execute = execute;
            m_canExecute = canExecute;
            m_onException = onException ?? (e => { });
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            if(!(parameter is T value))
            {
                return false;
            }

            return m_canExecute(value);
        }

        /// <inheritdoc />
        public Task ExecuteAsync(T value) => m_execute(value);

#pragma warning disable RECS0165
        /// <inheritdoc />
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

        /// <inheritdoc />
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public event EventHandler? CanExecuteChanged;
    }
}
