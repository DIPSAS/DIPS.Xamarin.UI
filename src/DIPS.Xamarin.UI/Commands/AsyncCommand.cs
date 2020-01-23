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
        public bool CanExecute(object? parameter) => m_canExecute();

        /// <inheritdoc />
        public async Task ExecuteAsync()
        {
            try
            {
                if (!CanExecute(null))
                {
                    return;
                }

                await m_execute();
            }
            catch (Exception exception)
            {
                m_onException(exception);
            }
        }

#pragma warning disable RECS0165
        /// <inheritdoc />
        public async void Execute(object parameter)
        {
            await ExecuteAsync();
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
        public async Task ExecuteAsync(T value)
        {
            if (value == null) return;
            try
            {
                if (!(value is T actualValue) || !m_canExecute(actualValue))
                {
                    return;
                }

                await m_execute(actualValue);
            }
            catch (Exception exception)
            {
                m_onException(exception);
            }
        }

#pragma warning disable RECS0165
        /// <inheritdoc />
        public void Execute(object parameter)
        {
            if (!(parameter is T value)) return;
            ExecuteAsync(value);
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
