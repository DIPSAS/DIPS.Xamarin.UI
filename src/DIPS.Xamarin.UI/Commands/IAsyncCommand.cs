using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.Xamarin.UI.Commands
{
    /// <summary>
    /// Converts command executes to tasks.
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <returns>Task to await</returns>
        Task ExecuteAsync();

        /// <summary>
        /// Raises the CanExecuteChanged event
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Converts command executes to tasks with a parameter of type T. Executions without the correct type will be ignored.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncCommand<T> : ICommand
    {
        /// <summary>
        /// Executes the task with parameter of type T.
        /// The task will not get executed if a wrong type is passed to it.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task ExecuteAsync(T value);

        /// <summary>
        /// Raises the CanExecuteChanged event
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
