using System;
using System.ComponentModel;
using DIPS.Xamarin.UI.Extensions;


namespace DIPS.Xamarin.UI.Samples.Controls.Skeleton
{
    public class SkeletonViewModel : INotifyPropertyChanged
    {
        private Random rnd = new Random();
        private bool isLoading;
        private string[] Headers = new[] { "This is a header", "Other headers might be longer", "Trying something new!" };

        public SkeletonViewModel()
        {
            isLoading = true;
        }

        public string Title { get; set; } = "Initial header is here";
        public string SubTitle { get; set; } = "Smaller content. Might be a much longer text. Be aware of line shifts";
        public string Initials { get; set; } = "EK";
        public bool IsLoading { get => isLoading; set => PropertyChanged.RaiseWhenSet(ref isLoading, value); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
