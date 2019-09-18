using System.Collections.Generic;
using System.ComponentModel;
using DIPS.Xamarin.UI.Extensions;
using FluentAssertions;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Extensions
{
    public class PropertyChangedExtensionsTests : INotifyPropertyChanged
    {
        private const string InitialValue = "Initial Value";
        private const string NewValue = "New Value";
        private string m_myFirstProperty;

        public PropertyChangedExtensionsTests()
        {
            MyFirstProperty = InitialValue;
        }

        public string MyFirstProperty
        {
            get => m_myFirstProperty;
            set => this.Set(ref m_myFirstProperty, value, PropertyChanged);
        }

        public string MySecondProperty { get; set; }
        public string MyThirdProperty { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [Fact]
        public void Set_NewValueIsNotEqualToOldValue_NotifyingPropertyChanged()
        {
            var result = false;
            PropertyChanged += (sender, e) => result = true;

            MyFirstProperty = NewValue;

            result.Should().BeTrue(because:"Value is not the same as initial value of the property");
        }

        [Fact]
        public void Set_NewValueIsEqualToOldValue_NotNotifyingPropertyChanged()
        {
            var result = false;
            PropertyChanged += (sender, e) => result = true;

            MyFirstProperty = InitialValue;

            result.Should().BeFalse(because:"Value was the same as initial value of the property");
        }

        [Fact]
        public void Set_NewValue_BackingStoreHasSameValue()
        {
            MyFirstProperty = NewValue;

            m_myFirstProperty.Should().Be(NewValue, because: "The set method will always set the value to a new value, or keep te same value");
        }

        [Fact]
        public void OnPropertyChanged_NotifiesPropertyChanged()
        {
            var result = false;
            PropertyChanged += (sender, e) => result = e.PropertyName.Equals(nameof(MyFirstProperty));

            this.OnPropertyChanged(PropertyChanged, nameof(MyFirstProperty));

            result.Should().BeTrue(because: "PropertyChanged should always be raised with the correct property");
        }
        [Fact]
        public void OnPropertyChanged_MySecondProperty_DoesNotNotifyMyFirstProperty()
        {
            var result = false;
            PropertyChanged += (sender, e) => result = e.PropertyName.Equals(nameof(MyFirstProperty));

            this.OnPropertyChanged(PropertyChanged, nameof(MySecondProperty));

            result.Should().BeFalse(because: "The correct property change should be notified");
        }

        [Fact]
        public void OnMultiplePropertyChanged_AllPropertiesShouldBeNotified()
        {
            var results = new List<bool>();
            PropertyChanged += (sender, e) => results.Add(e.PropertyName.Equals(nameof(MyFirstProperty)) 
                                                          || e.PropertyName.Equals(nameof(MySecondProperty)) 
                                                          || e.PropertyName.Equals(nameof(MyThirdProperty)));

            this.OnMultiplePropertiesChanged(PropertyChanged, nameof(MyFirstProperty), nameof(MySecondProperty), nameof(MyThirdProperty));

            results.Should().Equal(new List<bool>() { true, true, true });
        }
    }
}
