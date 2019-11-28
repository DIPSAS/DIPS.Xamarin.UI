using System;
using System.Collections.Generic;
using System.Reflection;
using DIPS.Xamarin.UI.Resources;
using DIPS.Xamarin.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Resources.Colors
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorsPage : ContentPage
    {
        public ColorsPage()
        {
            InitializeComponent();
            var allColorsCategories = new List<ColorCategory>()
            {
                GetColorCategory(typeof(Theme)), GetColorCategory(typeof(ColorPalette)), GetColorCategory(typeof(StatusColorPalette)),
            };


            foreach (var colorCategory in allColorsCategories)
            {
                colorCategories.Children.Add(new Label(){Text = colorCategory.Name, FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))});
                foreach (var colorInfo in colorCategory.ColorInfos)
                {
                    var colorStackLayout = new StackLayout();
                    colorStackLayout.Children.Add(new Label(){Text =  colorInfo.Name});
                    colorStackLayout.Children.Add(new BoxView(){Color = colorInfo.Color});
                    colorCategories.Children.Add(colorStackLayout);
                }
            }
            
        }

        private ColorCategory GetColorCategory(Type type)
        {
            var listOfColorInfos = new List<ColorInfo>();
            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            var categoryName = type.Name;
            foreach (var fieldInfo in fields)
            {
                var colorInfo = new ColorInfo(fieldInfo.Name, (Color)fieldInfo.GetValue(null));

                listOfColorInfos.Add(colorInfo);
            }
            return new ColorCategory(categoryName, listOfColorInfos);
        }

        public class ColorInfo
        {
            public ColorInfo(string name, Color color)
            {
                Name = name;
                Color = color;
            }
            public string Name { get;  }
            public Color Color { get;  }
        }

        public class ColorCategory
        {
            public string Name { get; }
            public IEnumerable<ColorInfo> ColorInfos { get; }

            public ColorCategory(string name, IEnumerable<ColorInfo> colorInfos)
            {
                Name = name;
                ColorInfos = colorInfos;
            }
        }
    }
}