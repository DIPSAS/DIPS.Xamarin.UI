using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    public class CancelSheetCommand<T> : CancelSheetCommand
    {

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

        public CancelSheetCommand(Action<T> execute, Func<T, bool> canExecute, Func<T, bool> canCloseSheet) 
            : base(o =>
            {
                if (IsValidParameter(o))
                {
                    execute((T)o);
                }
            }, o => IsValidParameter(o) && canExecute((T)o), o => IsValidParameter(o) && canCloseSheet((T)o))
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

    public class CancelSheetCommand : Command, ICancelSheetCommand
    {
        private readonly Func<object, bool> m_canCloseSheet;

        public CancelSheetCommand(Action execute) : base(execute)
        {
        }

        public CancelSheetCommand(Action<object> execute) : base(execute)
        {

        }

        public CancelSheetCommand(Action execute, Func<bool> canExecute) : base(execute, canExecute)
        {
        }
            
        public CancelSheetCommand(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {
        }

        public CancelSheetCommand(Action execute, Func<bool> canExecute, Func<bool> canCloseSheet) : this(o => execute(), o => canExecute(), o => canCloseSheet())
        {
        }

        public CancelSheetCommand(Action<object> execute, Func<object, bool> canExecute, Func<object, bool> canCloseSheet) : base(execute, canExecute)
        {
            m_canCloseSheet = canCloseSheet;
        }

        public bool CanCloseSheet(object parameter) => m_canCloseSheet(parameter);
    }

    public interface ICancelSheetCommand : ICommand
    {
        bool CanCloseSheet(object parameter);
    }
}
