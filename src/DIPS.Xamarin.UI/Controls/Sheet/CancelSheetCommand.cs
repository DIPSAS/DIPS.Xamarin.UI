using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CancelSheetCommand<T> : CancelSheetCommand
    {

        /// <summary>
        /// </summary>
        /// <param name="execute"></param>
        public CancelSheetCommand(Action<T> execute)
            : base(o =>
            {
                if (IsValidParameter(o))
                {
                    execute((T)o);
                }
            })
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public CancelSheetCommand(Action<T> execute, Func<T, bool> canExecute)
            : base(o =>
            {
                if (IsValidParameter(o))
                {
                    execute((T)o);
                }
            }, o => IsValidParameter(o) && canExecute((T)o))
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <param name="canCloseSheet"></param>
        public CancelSheetCommand(Action<T> execute, Func<T, bool> canExecute, Func<T, bool> canCloseSheet) 
            : base(o =>
            {
                if (IsValidParameter(o))
                {
                    execute((T)o);
                }
            }, o => IsValidParameter(o) && canExecute((T)o), async o => IsValidParameter(o) && canCloseSheet((T)o))
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <param name="canCloseSheet"></param>
        public CancelSheetCommand(Action<T> execute, Func<T, bool> canExecute, Func<T, Task<bool>> canCloseSheet)
            : base(o =>
            {
                if (IsValidParameter(o))
                {
                    execute((T)o);
                }
            }, o => IsValidParameter(o) && canExecute((T)o), async o => IsValidParameter(o) && await canCloseSheet((T)o))
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        static bool IsValidParameter(object o)
        {
            if (o != null)
            {
                // The parameter isn't null, so we don't have to worry whether null is a valid option
                return o is T;
            }

            var t = typeof(T);

            // The parameter is null. Is T Nullable?
            if (Nullable.GetUnderlyingType(t) != null)
            {
                return true;
            }

            // Not a Nullable, if it's a value type then null is not valid
            return !t.GetTypeInfo().IsValueType;
        }
	}

    /// <summary>
    /// <inheritdoc cref="Command"/>
    /// Extended with a function to determine if a <see cref="SheetBehavior"/> should be closed when action is invoked.
    /// </summary>
    public class CancelSheetCommand : Command, ICancelSheetCommand
    {
        private readonly Func<object, Task<bool>>? m_canCloseSheet;

        /// <summary>
        /// </summary>
        public CancelSheetCommand(Action execute) : base(execute)
        {
        }

        /// <summary>
        /// </summary>
        public CancelSheetCommand(Action<object> execute) : base(execute)
        {

        }

        /// <summary>
        /// </summary>
        public CancelSheetCommand(Action execute, Func<bool> canExecute) : base(execute, canExecute)
        {
        }

        /// <summary>
        /// </summary>
        public CancelSheetCommand(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        /// <summary>
        /// </summary>
        public CancelSheetCommand(Action execute, Func<bool> canExecute, Func<Task<bool>> canCloseSheet) : this(o => execute(), o => canExecute(), o => canCloseSheet())
        {
        }

        /// <summary>
        /// </summary>
        public CancelSheetCommand(Action execute, Func<bool> canExecute, Func<bool> canCloseSheet) : this(o => execute(), o => canExecute(), o => Task.FromResult(canCloseSheet()))
        {
        }
        /// <summary>
        /// </summary>
        public CancelSheetCommand(Action<object> execute, Func<object, bool> canExecute, Func<object, bool> canCloseSheet) : this(execute, canExecute, o => Task.FromResult(canCloseSheet(o)))
        {
        }
        /// <summary>
        /// </summary>
        public CancelSheetCommand(Action<object> execute, Func<object, bool> canExecute, Func<object, Task<bool>> canCloseSheet) : base(execute, canExecute)
        {
            m_canCloseSheet = canCloseSheet;
        }

        /// <summary>
        /// </summary>
        public async Task<bool> CanCloseSheet(object parameter) => m_canCloseSheet == null || await m_canCloseSheet(parameter);
    }

    /// <summary>
    /// <inheritdoc cref="Command"/>
    /// Extended with a function to determine if a <see cref="SheetBehavior"/> should be closed when action is invoked.
    /// </summary>
    public interface ICancelSheetCommand : ICommand
    {
        /// <summary>
        /// Determines if the sheet should be closed if cancel button is pressed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<bool> CanCloseSheet(object parameter);
    }
}
