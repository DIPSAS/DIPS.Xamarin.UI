using System.ComponentModel;
using DIPS.Xamarin.UI.Extensions;

namespace DIPS.Xamarin.UI.Samples.Extensions
{
    /// <summary>
    ///     This sample shows how we can use the <see cref="PropertyChangedExtensions" /> in order to simplify how to notify
    ///     property changed
    /// </summary>
    internal class PropertyChangedViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string m_myFirstProperty;
        private string m_mySecondProperty;
        private string m_myThirdProperty;

        /// <summary>
        /// Notifies with a single Set method
        /// </summary>
        public string MyFirstProperty
        {
            get => m_myFirstProperty;
            set
            {
                this.Set(ref m_myFirstProperty, value, PropertyChanged);

                //Alternate fluent version
                PropertyChanged?.RaiseAfter(ref m_myFirstProperty, value);
            }
        }

        /// <summary>
        /// Notifies with a OnPropertyChanged method
        /// </summary>
        public string MySecondProperty
        {
            get => m_mySecondProperty;
            set
            {
                m_mySecondProperty = value;

                this.OnPropertyChanged(PropertyChanged);

                //Alternate fluent version
                PropertyChanged?.Raise();
            }
        }

        /// <summary>
        /// Notifies that multiple properties has changed
        /// </summary>
        public string MyThirdProperty
        {
            get => m_myThirdProperty;
            set
            {
                m_myThirdProperty = value;

                this.OnMultiplePropertiesChanged(PropertyChanged, 
                    nameof(MyFirstProperty), 
                    nameof(MySecondProperty), 
                    nameof(MyThirdProperty));

                //Alternate fluent version
                PropertyChanged?.RaiseForEach(nameof(MyFirstProperty), nameof(MySecondProperty), nameof(MyThirdProperty));
            }
        }
    }
}