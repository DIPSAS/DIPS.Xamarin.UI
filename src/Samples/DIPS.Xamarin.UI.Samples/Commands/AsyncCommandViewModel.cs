using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Extensions;

namespace DIPS.Xamarin.UI.Samples.Commands
{
    public class AsyncCommandViewModel : INotifyPropertyChanged
    {
        private int m_counter;
        private int m_onClickCounter;
        public AsyncCommandViewModel()
        {
            CounterCommand = new AsyncCommand(async () =>
            {
                await Task.Delay(2000);
                Counter++;
                OnCounterCommand.RaiseCanExecuteChanged();
                CounterCommand.RaiseCanExecuteChanged();
            }, () => Math.Abs(m_counter - m_onClickCounter) < 5);

            OnCounterCommand = new AsyncCommand<int>(async num =>
            {
                await Task.Delay(150);
                OnClickCounter = num;
                CounterCommand.RaiseCanExecuteChanged();
                OnCounterCommand.RaiseCanExecuteChanged();
            }, num => Math.Abs(num - OnClickCounter) > 2);
        }

        public IAsyncCommand CounterCommand { get; }
        public IAsyncCommand<int> OnCounterCommand { get; }

        public int Counter
        {
            get => m_counter;
            set => this.Set(ref m_counter, value, PropertyChanged);
        }

        public int OnClickCounter
        {
            get => m_onClickCounter;
            set => this.Set(ref m_onClickCounter, value, PropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
