using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.TrendGraph
{
    public partial class TrendGraph : ContentView
    {
        public TrendGraph()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  <see cref="GraphColor" />
        /// </summary>
        public static readonly BindableProperty GraphColorProperty =
            BindableProperty.Create(nameof(GraphColor), typeof(Color), typeof(TrendGraph), Color.Purple);

        /// <summary>
        /// Color of the filled graph
        /// </summary>
        public Color GraphColor
        {
            get { return (Color)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        ///  <see cref="GraphBackgroundColor" />
        /// </summary>
        public static readonly BindableProperty GraphBackgroundColorProperty =
            BindableProperty.Create(nameof(GraphBackgroundColor), typeof(Color), typeof(TrendGraph), Color.LightGray);

        /// <summary>
        /// Color behind the graph
        /// </summary>
        public Color GraphBackgroundColor
        {
            get { return (Color)GetValue(GraphBackgroundColorProperty); }
            set { SetValue(GraphBackgroundColorProperty, value); }
        }

        /// <summary>
        ///  <see cref="MaxValue" />
        /// </summary>
        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(TrendGraph), 100.0);

        /// <summary>
        /// Max value of the trend
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        /// <summary>
        ///  <see cref="MinValue" />
        /// </summary>
        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(nameof(MinValue), typeof(double), typeof(TrendGraph), 0.0);

        /// <summary>
        /// Min value of the trend
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        /// <summary>
        ///  <see cref="ItemsSource" />
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(TrendGraph));

        /// <summary>
        /// Items used to draw the trends. Use <see cref="ValueMemberPath" /> to expose path to item values
        /// </summary>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        ///  <see cref="ValueMemberPath" />
        /// </summary>
        public static readonly BindableProperty ValueMemberPathProperty =
            BindableProperty.Create(nameof(ValueMemberPath), typeof(string), typeof(TrendGraph), string.Empty);

        /// <summary>
        /// To expose path to item values. Set this if the items are not of type int, double or float.
        /// </summary>
        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }


    }
}
