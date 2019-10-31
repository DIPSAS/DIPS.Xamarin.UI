using System.Threading.Tasks;
using System.Windows.Input;

namespace DIPS.Xamarin.UI.Commands
{
    /// <summary>
    /// Aync command without parameters. Converts command executes to task to simply ViewModel creation.
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <returns>Task to await</returns>
        Task ExecuteAsync();

        /// <summary>
        /// Raises the event CanExecuteChanged to refresh the view if CanExecute has changed.
        /// </summary>
        void RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Aync command with parameters. Converts command executes to task to simply ViewModel creation.
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
        /// Raises the event CanExecuteChanged to refresh the view if CanExecute has changed.
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
