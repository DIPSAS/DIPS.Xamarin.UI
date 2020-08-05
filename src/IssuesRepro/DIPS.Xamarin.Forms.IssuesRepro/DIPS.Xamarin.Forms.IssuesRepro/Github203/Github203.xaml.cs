using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github203
{
    [Issue(203)]
    public partial class Github203 : ContentPage
    {
        public Github203()
        {
            InitializeComponent();
        }

        public void DateChanged(object sender, DateChangedEventArgs eventArgs)
        {
            text.Text = eventArgs.NewDate.ToString();
        }
    }
}
