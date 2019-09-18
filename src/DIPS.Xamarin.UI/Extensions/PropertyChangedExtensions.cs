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
        public static bool Set<S>(this INotifyPropertyChanged propertyChangedImplementation, ref S backingStore, S value, PropertyChangedEventHandler propertyChanged, [CallerMemberName] string propertyName = "")
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
        public static void OnPropertyChanged(this INotifyPropertyChanged propertyChangedImplementation, PropertyChangedEventHandler propertyChanged, [CallerMemberName] string propertyName = "")
        {
            propertyChanged?.Invoke(propertyChangedImplementation, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies multiple property changed
        /// </summary>
        /// <param name="propertyChangedImplementation">The property changed implementation, this is normally a view model</param>
        /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
        /// <param name="properties"></param>
        public static void OnMultiplePropertiesChanged(this INotifyPropertyChanged propertyChangedImplementation, PropertyChangedEventHandler propertyChanged, params string[] properties)
        {
            foreach (var property in properties)
            {
                propertyChangedImplementation.OnPropertyChanged(propertyChanged, property);
            }
        }
    }
}