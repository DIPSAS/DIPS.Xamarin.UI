﻿using System.Collections.Generic;
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
        private string m_testBackingStore = InitialValue;
        private string m_myProperty;

        public event PropertyChangedEventHandler PropertyChanged;

        [Fact]
        public void Raise_When_NewValueIsNotEqualToOldValue_NotifyingPropertyChanged()
        {
            var result = false;
            PropertyChanged += (sender, e) => result = true;

            this.Raise(PropertyChanged).When(ref m_testBackingStore, NewValue);

            result.Should().BeTrue(because:"Value is not the same as initial value of the property");
        }

        [Fact]
        public void Raise_When_NewValueIsEqualToOldValue_NotNotifyingPropertyChanged()
        {
            var result = false;
            PropertyChanged += (sender, e) => result = true;

            this.Raise(PropertyChanged).When(ref m_testBackingStore, InitialValue);

            result.Should().BeFalse(because:"Value was the same as initial value of the property");
        }

        [Fact]
        public void Raise_When_NewValue_BackingStoreHasSameValue()
        {
            this.Raise(PropertyChanged).When(ref m_testBackingStore, NewValue);

            m_testBackingStore.Should().Be(NewValue, because: "The set method will always set the value to a new value, or keep te same value");
        }

        [Fact]
        public void Raise_PropertyChanged_Now_NotifiesPropertyChanged()
        {
            var testPropertyName = "TestProperty";
            var result = false;
            PropertyChanged += (sender, e) => result = e.PropertyName.Equals(testPropertyName);

            this.Raise(PropertyChanged).Now(testPropertyName);

            result.Should().BeTrue(because: "PropertyChanged should always be raised with the correct property");
        }

        [Fact]
        public void Raise_On_MultipleProperties_AllPropertiesShouldBeNotified()
        {
            var firstTestPropertyName = "FirstTestPropertyName";
            var secondTestPropertyName = "SecondTestPropertyName";
            var thirdTestPropertyName = "ThirdTestPropertyName";
            var results = new List<bool>();
            PropertyChanged += (sender, e) => results.Add(e.PropertyName.Equals(firstTestPropertyName) 
                                                          || e.PropertyName.Equals(secondTestPropertyName) 
                                                          || e.PropertyName.Equals(thirdTestPropertyName));

            this.Raise(PropertyChanged).On(firstTestPropertyName, secondTestPropertyName, thirdTestPropertyName);

            results.Should().Equal(new List<bool>() { true, true, true });
        }

        [Fact]
        public void Raise_Now_ViaProperty()
        {
            bool result = false;    
            PropertyChanged += (sender, e) => result = e.PropertyName.Equals(nameof(MyProperty));
            MyProperty = "lol";

            result.Should().BeTrue();
        }

        public string MyProperty
        {
            get => m_myProperty;
            set => this.Raise(PropertyChanged).Now();
        }
    }
}
