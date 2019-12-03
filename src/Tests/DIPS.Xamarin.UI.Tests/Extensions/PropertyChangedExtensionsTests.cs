using System;
using System.Collections.Generic;
using System.ComponentModel;
using DIPS.Xamarin.UI.Extensions;
using FluentAssertions;
using Xamarin.Forms.Internals;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Extensions
{
    public class PropertyChangedExtensionsTests : INotifyPropertyChanged
    {
        private const string InitialValue = "Initial Value";
        private const string NewValue = "New Value";
        private string m_testBackingStore;

        public PropertyChangedExtensionsTests()
        {
            m_testBackingStore = InitialValue;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        [Fact]
        public void Raise_MultiplePropertyChanges_AllPropertiesShouldBeNotified()
        {
            var firstTestPropertyName = "FirstTestPropertyName";
            var secondTestPropertyName = "SecondTestPropertyName";
            var thirdTestPropertyName = "ThirdTestPropertyName";
            var acts = new Action[]
            {
                () => this.OnMultiplePropertiesChanged(PropertyChanged, firstTestPropertyName, secondTestPropertyName, thirdTestPropertyName),
                () => PropertyChanged.RaiseForEach(firstTestPropertyName, secondTestPropertyName, thirdTestPropertyName)
            };

            foreach (var act in acts)
            {
                var results = new List<bool>();
                PropertyChanged += (sender, e) => results.Add(
                    e.PropertyName.Equals(firstTestPropertyName) || e.PropertyName.Equals(secondTestPropertyName) ||
                    e.PropertyName.Equals(thirdTestPropertyName));

                act.Invoke();
                results.Should().Equal(new List<bool>() { true, true, true }, because:$"All properties should always notify  when running act number {acts.IndexOf(act)}");
            }
        }

        [Fact]
        public void Raise_PropertyChanged_NotifiesPropertyChanged()
        {
            var testPropertyName = "TestProperty";
            var acts = new Action[]
            {
                () => this.OnPropertyChanged(PropertyChanged, testPropertyName), () => PropertyChanged.Raise(testPropertyName)
            };
            foreach (var act in acts)
            {
                var result = false;
                PropertyChanged += (sender, e) => result = e.PropertyName.Equals(testPropertyName);

                act.Invoke();
                result.Should()
                    .BeTrue($"PropertyChanged should always be raised with the correct property when running act number {acts.IndexOf(act)}");
            }
        }

        [Fact]
        public void TrySetBackingStore_ToNewValue_AndTryRaisePropertyChanged_NotifyingPropertyChanged()
        {
            var acts = new Action[]
            {
                () => this.Set(ref m_testBackingStore, NewValue, PropertyChanged),
                () => PropertyChanged.RaiseAfter(ref m_testBackingStore, NewValue)
            };

            foreach (var act in acts)
            {
                m_testBackingStore = InitialValue;
                var result = false;
                PropertyChanged += (sender, e) => result = true;

                act.Invoke();
                result.Should().BeTrue($"Value is not the same as initial value of the property when running act number {acts.IndexOf(act)}");
                
            }
        }

        [Fact]
        public void TrySetBackingStore_ToNewValue_BackingStoreShouldHaveSameValue()
        {
            var acts = new Action[]
            {
                () => this.Set(ref m_testBackingStore, NewValue, PropertyChanged),
                () => PropertyChanged.RaiseAfter(ref m_testBackingStore, NewValue)
            };

            foreach (var act in acts)
            {
                m_testBackingStore = InitialValue;
                
                act.Invoke();
                m_testBackingStore.Should().Be(
                    NewValue,
                    $"The backing store should always set the value to the new value, or keep te same value when running act number {acts.IndexOf(act)}");
            }
        }

        [Fact]
        public void TrySetBackingStore_ToSameValue_AndTryRaisePropertyChanged_NotNotifyingPropertyChanged()
        {
            var acts = new Action[]
            {
                () => this.Set(ref m_testBackingStore, InitialValue, PropertyChanged),
                () => PropertyChanged.RaiseAfter(ref m_testBackingStore, InitialValue)
            };
            foreach (var act in acts)
            {
                var result = false;
                PropertyChanged += (sender, e) => result = true;

                act.Invoke();
                result.Should().BeFalse("Value was the same as initial value of the property when running act number {acts.IndexOf(act)}");
            }
        }
    }
}