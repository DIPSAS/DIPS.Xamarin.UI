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
#pragma warning disable CS8604 // Disabled to be able to test RaiseAfter by calling Set, PropertyChanged should never actually be null in a Xamarin.Forms app and we do a null propagation check in RaiseAfter, so its safe to ignore
            return propertyChanged.RaiseAfter(ref backingStore, value, propertyChangedImplementation, propertyName);
#pragma warning restore CS8604
        }

        /// <summary>
            /// Notifies property changed.
            /// </summary>
            /// <param name="propertyChangedImplementation">The property changed implementation, this is normally a view model </param>
            /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
            /// <param name="propertyName">A nullable property name, if left empty it will pick the caller member name</param>
            public static void OnPropertyChanged(this INotifyPropertyChanged propertyChangedImplementation, PropertyChangedEventHandler? propertyChanged, [CallerMemberName] string propertyName = "")
        {
            propertyChanged?.Raise(propertyName, propertyChangedImplementation);
        }

        /// <summary>
        /// Notifies multiple property changed
        /// </summary>
        /// <param name="propertyChangedImplementation">The property changed implementation, this is normally a view model</param>
        /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
        /// <param name="properties"></param>
        public static void OnMultiplePropertiesChanged(this INotifyPropertyChanged propertyChangedImplementation, PropertyChangedEventHandler? propertyChanged, params string[] properties)
        {
            propertyChanged?.RaiseForEach(propertyChangedImplementation, properties);

        }

        /// <summary>
        /// Notifies property changed.
        /// </summary>
        /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
        /// <param name="propertyName">A nullable property name, if left empty it will pick the caller member name</param>
        /// <param name="sender">The event sender, optional because it is not required for the Xamarin.Forms view to react to a property changed</param>
        /// <remarks>This extension method does not set the event `sender` when notifying property changed. This has not proven to a problem when we created the extension, but be aware of it if you end up with something strange. Set <see cref="sender"/> if you need to make sure that we send the event sender</remarks>
        public static void Raise(this PropertyChangedEventHandler propertyChanged, [CallerMemberName] string propertyName = "", object? sender = null)
        {
            propertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies multiple property changed
        /// </summary>
        /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
        /// <param name="sender">The event sender, optional because it is not required for the Xamarin.Forms view to react to a property changed</param>
        /// <param name="properties"></param>
        /// <remarks>This extension method does not set the event `sender` when notifying property changed. This has not proven to a problem when we created the extension, but be aware of it if you end up with something strange. Set <see cref="sender"/> if you need to make sure that we send the event sender</remarks>
        public static void RaiseForEach(this PropertyChangedEventHandler propertyChanged, object? sender = null, params string[] properties)
        {
            foreach (var property in properties)
            {
                propertyChanged.Raise(property, sender);
            }
        }

        /// <summary>
        /// Sets a value to a backing field if it passes a equality check and notifies property changed.
        /// </summary>
        /// <typeparam name="S">The type of the property</typeparam>
        /// <param name="backingStore">The backing store that will hold the value of the property</param>
        /// <param name="value">The new value that should be set</param>
        /// <param name="propertyChanged">The property changed event handler that the propertyChangedImplementation holds</param>
        /// <param name="sender">The event sender, optional because it is not required for the Xamarin.Forms view to react to a property changed</param>
        /// <param name="propertyName">A nullable property name, if left empty it will pick the caller member name</param>
        /// <remarks>This method does a equality check to see if the value has changed, if you need to notify property changed when the value has not changed, please use <see cref="Raise"/></remarks>
        /// <remarks>This extension method does not set the event `sender` when notifying property changed. This has not proven to a problem when we created the extension, but be aware of it if you end up with something strange. Set <see cref="sender"/> if you need to make sure that we send the event sender</remarks>
        /// <returns>A boolean value to indicate that the property changed has been invoked</returns>
        public static bool RaiseAfter<S>(this PropertyChangedEventHandler propertyChanged, ref S backingStore, S value, object? sender = null, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<S>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            propertyChanged?.Raise(propertyName,sender);
            return true;
        }
    }
}