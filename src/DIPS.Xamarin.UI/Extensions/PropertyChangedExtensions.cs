using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DIPS.Xamarin.UI.Extensions
{
    /// <summary>
    /// An extensions class that holds extensions regarding property changed
    /// </summary>
    public static class PropertyChangedExtensions
    {
        /// <summary>
        /// Sets a value to a backing field if it passes a equality check and notifies property changed.
        /// </summary>
        /// <typeparam name="S">The type of the property</typeparam>
        /// <param name="propertyChangedImplementation">The property changed implementation, this is normally a view model</param>
        /// <param name="backingStore">The backing store that will hold the value of the property</param>
        /// <param name="value">The new value that should be set</param>
        /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
        /// <param name="propertyName">A nullable property name, if left empty it will pick the caller member name</param>
        /// <remarks>This method does a equality check to see if the value has changed, if you need to notify property changed when the value has not changed, please use <see cref="OnPropertyChanged"/></remarks>
        /// <returns>A boolean value to indicate that the property changed has been invoked</returns>
        public static bool Set<S>(this INotifyPropertyChanged propertyChangedImplementation, ref S backingStore, S value, PropertyChangedEventHandler? propertyChanged, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<S>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            propertyChangedImplementation.OnPropertyChanged(propertyChanged, propertyName);
            return true;
        }

        /// <summary>
        /// Notifies property changed.
        /// </summary>
        /// <param name="propertyChangedImplementation">The property changed implementation, this is normally a view model </param>
        /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
        /// <param name="propertyName">A nullable property name, if left empty it will pick the caller member name</param>
        public static void OnPropertyChanged(this INotifyPropertyChanged propertyChangedImplementation, PropertyChangedEventHandler? propertyChanged, [CallerMemberName] string propertyName = "")
        {
            propertyChanged?.Invoke(propertyChangedImplementation, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies multiple property changed
        /// </summary>
        /// <param name="propertyChangedImplementation">The property changed implementation, this is normally a view model</param>
        /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
        /// <param name="properties"></param>
        public static void OnMultiplePropertiesChanged(this INotifyPropertyChanged propertyChangedImplementation, PropertyChangedEventHandler? propertyChanged, params string[] properties)
        {
            foreach (var property in properties)
            {
                propertyChangedImplementation.OnPropertyChanged(propertyChanged, property);
            }
        }

        public static INotifyPropertyChangedBuilder On(
            this INotifyPropertyChanged propertyChangedImplementation,
            PropertyChangedEventHandler? propertyChanged,
            [CallerMemberName] string propertyName = "")
        {
            return new NotifyPropertyChangedBuilder(propertyChangedImplementation, propertyChanged, propertyName);
        }

        public static void On(
            this INotifyPropertyChanged propertyChangedImplementation, PropertyChangedEventHandler? propertyChanged, params string[] properties)
        {
            propertyChangedImplementation.OnMultiplePropertiesChanged(propertyChanged, properties);
        }
    }

    internal class NotifyPropertyChangedBuilder : INotifyPropertyChangedBuilder
    {
        private readonly INotifyPropertyChanged m_PropertyChangedImplementation;
        private readonly PropertyChangedEventHandler m_propertyChanged;
        private readonly string m_propertyName;

        internal NotifyPropertyChangedBuilder(INotifyPropertyChanged propertyChangedImplementation, PropertyChangedEventHandler? propertyChanged, string propertyName = "")
        {
            m_PropertyChangedImplementation = propertyChangedImplementation;
            m_propertyChanged = propertyChanged;
            m_propertyName = propertyName;
        }

        bool INotifyPropertyChangedBuilder.After<S>(ref S backingStore,S newValue)
        {
            return m_PropertyChangedImplementation.Set(ref backingStore, newValue, m_propertyChanged, m_propertyName);
        }
    }

    /// <summary>
    /// Notify property changed builder that is used to obtain an fluent API for property changed
    /// </summary>
    public interface INotifyPropertyChangedBuilder {
        /// <summary>
        /// Sets a value to a backing field if it passes a equality check and notifies property changed.
        /// </summary>
        /// <typeparam name="S">The type of the property</typeparam>
        /// <param name="backingStore">The backing store that will hold the value of the property</param>
        /// <param name="newValue">The new value that should be set</param>
        bool After<S>(ref S backingStore,S newValue);
    }
}