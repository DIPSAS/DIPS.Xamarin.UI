﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.RadioButtonGroup {
    public class RadioButtonGroupPageViewModel : INotifyPropertyChanged
    {
        private string m_deSelectedColor = "#047F89";
        private ObservableCollection<ItemViewModel> m_items;
        private string m_selectedColor = "#047F89";
        private ItemViewModel? m_selectedItem;
        private string m_separatorColor = "lightgrey";
        

        public RadioButtonGroupPageViewModel()
        {
            m_items = new ObservableCollection<ItemViewModel>();
            AddNewCommand = new Command(() => Items.Add(new ItemViewModel($"{Items.Count+1}th option")));
            RemoveLastCommand = new Command(() => Items.Remove(Items.LastOrDefault()));
            RemoveSecondCommand = new Command(() => Items.RemoveAt(Items.Count > 1 ? 1 : 0));
            InsertSecondCommand = new Command(() => Items.Insert(Items.Count > 0 ? 1 : 0, new ItemViewModel("Inserted item")));
            ResetListCommand = new Command(() => Items = new ObservableCollection<ItemViewModel>(){new ItemViewModel("New with resetted item")});
        }

        public void Initialize()
        {
            var firstItem = new ItemViewModel("1st option");
            var secondItem = new ItemViewModel("2nd option");
            var thirdItem = new ItemViewModel("3rd option");

            Items.Add(firstItem);
            Items.Add(secondItem);
            Items.Add(thirdItem);
            SelectedItem = secondItem;
        }

        public ICommand AddNewCommand { get; }
        public ICommand RemoveLastCommand { get; }
        public ICommand InsertSecondCommand { get; }
        public ICommand ResetListCommand { get; }

        public string DeSelectedColor
        {
            get => m_deSelectedColor;
            set
            {
                try
                {
                    new ColorTypeConverter().ConvertFromInvariantString(value);
                    m_deSelectedColor = value;
                    this.OnPropertyChanged(PropertyChanged);
                }
                catch (Exception e)
                {
                    //Swallow it.
                }
            }
        }

        public ObservableCollection<ItemViewModel> Items
        {
            get => m_items;
            set => this.Set(ref m_items, value, PropertyChanged);
        }

        public string SelectedColor
        {
            get => m_selectedColor;
            set
            {
                try
                {
                    new ColorTypeConverter().ConvertFromInvariantString(value);
                    m_selectedColor = value;
                    this.OnPropertyChanged(PropertyChanged);
                }
                catch (Exception e)
                {
                    //Swallow it.
                }
            }
        }

        public ItemViewModel SelectedItem
        {
            get => m_selectedItem;
            set => this.Set(ref m_selectedItem, value, PropertyChanged);
        }

        public string SeparatorColor
        {
            get => m_separatorColor;
            set
            {
                try
                {
                    new ColorTypeConverter().ConvertFromInvariantString(value);
                    m_separatorColor = value;
                    this.OnPropertyChanged(PropertyChanged);
                }
                catch (Exception e)
                {
                    //Swallow it.
                }
            }
        }

        public Command<ItemViewModel> SelectedItemChangedCommand { get; }

        public ICommand RemoveSecondCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}