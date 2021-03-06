﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.ValueConverters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvertedBoolConverterPage : ContentPage
    {
        public InvertedBoolConverterPage()
        {
            InitializeComponent();
        }
    }

    public class InvertedBoolConverterViewModel : INotifyPropertyChanged
    {
        private bool m_someLogicalProperty;

        public bool SomeLogicalProperty
        {
            get => m_someLogicalProperty;
            set => this.Set(ref m_someLogicalProperty, value, PropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}