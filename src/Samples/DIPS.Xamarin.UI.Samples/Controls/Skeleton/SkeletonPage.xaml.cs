using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Skeleton
{
    public partial class SkeletonPage : ContentPage
    {
        private bool isLoading;

        public SkeletonPage()
        {
            InitializeComponent();
            BindingContext = new SkeletonViewModel();
        }
    }
}
