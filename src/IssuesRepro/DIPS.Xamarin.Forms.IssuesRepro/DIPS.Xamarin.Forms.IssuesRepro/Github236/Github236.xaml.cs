using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github236
{
    [Issue(236)]
    public partial class Github236 : ContentPage
    {
        public Github236()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = new Github236VM();
            vm.Init();
            BindingContext = vm;
        }
    }

    public class Github236VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public async void Init()
        {
            var d = new string[]
            {
                "T1", "YTEPWNPOYMEW", "TT$Mt32", "Tgeageawgew1", "T1geagewagdsagewagewag ewag ewg eawg ase"
            };
            var rnd = new Random();
            Items.Add(new ItemVM() { Data = "T1" });
            Items.Add(new ItemVM() { Data = "YTEPWNPOYMEW" });
            Items.Add(new ItemVM() { Data = "TT$Mt32" });
            Items.Add(new ItemVM() { Data = "Tgeageawgew1" });
            Items.Add(new ItemVM() { Data = "T1geagewagdsagewagewag ewag ewg eawg ase" });
            Items.Add(new ItemVM() { Data = "5315321532ewag egwadg eawg ase" });
            Items.Add(new ItemVM() { Data = "T1geagewagdsagewagewag t234qtqtewg eawg ase" });

            while (true)
            {
                await Task.Delay(100 + rnd.Next(0, 1200));
                IsLoading = false;
                PropertyChanged.Raise(nameof(IsLoading));
                foreach (var item in Items)
                {
                    item.Data = d[rnd.Next(d.Length)];
                }
                await Task.Delay(400 + rnd.Next(0, 1200));

                IsLoading = true;
                PropertyChanged.Raise(nameof(IsLoading));
            }
        }
        public bool IsLoading { get; set; } = true;

        public List<ItemVM> Items { get; set; } = new List<ItemVM>();
    }


    public class ItemVM : INotifyPropertyChanged
    {
        private string m_data;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Data { get => m_data; set => PropertyChanged.RaiseWhenSet(ref m_data, value); }
    }
}
