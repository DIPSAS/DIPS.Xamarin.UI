using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// To be added
    /// </summary>
    [ContentProperty(nameof(ItemTemplate))]
    public class SlidableContentLayout : SlidableLayout
    {
        private readonly Dictionary<int, View> m_viewMapping = new Dictionary<int, View>();
        private readonly AbsoluteLayout m_container = new AbsoluteLayout()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Padding = 0,
            Margin = 0
        };

        /// <summary>
        /// To be added
        /// </summary>
        public SlidableContentLayout()
        {
            Content = m_container;
            m_container.IsClippedToBounds = true;
        }
        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="index"></param>
        protected override void OnScrolled(double index)
        {
            base.OnScrolled(index);
            if (Width < 0.1) return;
            var center = base.Center;
            var itemWidth = base.GetItemWidth();
            var selectedIndex = GetIndexFromValue(index);
            var itemCount = (center * 2) / itemWidth;

            if (m_viewMapping.Count > itemCount*20)
            {
                foreach(var key in m_viewMapping.Select(d => d.Key).ToList())
                {
                    if(Math.Abs(index - key) > itemCount * 2)
                    {
                        m_viewMapping.Remove(key);
                    }
                }
            }

            var toAdd = new HashSet<View>();
            for (var i = index - itemCount; i <= index + itemCount; i++)
            {
                var iIndex = (int)Math.Round(i);
                if (iIndex < Config.MinValue || iIndex > Config.MaxValue) continue;
                var view = CreateItem(iIndex);
                if (view is ISliderSelectable selectable) selectable.OnSelectionChanged(selectedIndex == iIndex);
                var dist = (Math.Abs(index - iIndex) / itemCount);
                var position = (itemWidth * (1 - dist * 0.33) * (iIndex - index));
                AbsoluteLayout.SetLayoutBounds(view, new Rectangle(Center + position-itemWidth/2, 0, ElementWidth, 1));
                toAdd.Add(view);
                view.Scale = 1-dist*0.5;
            }

            for (var i = m_container.Children.Count-1; i >= 0; i--)
            {
                var item = m_container.Children[i];
                if (!toAdd.Contains(item)) m_container.Children.RemoveAt(i);
            }

            foreach(var item in toAdd)
            {
                if (m_container.Children.Contains(item)) continue;
                m_container.Children.Add(item);
            }
        }

        private View CreateDefault() => new DefaultSliderView();

        private View CreateItem(int id)
        {
            if (m_viewMapping.TryGetValue(id, out var element)) return element;
            element = (View)(ItemTemplate?.CreateContent() ?? CreateDefault());
            element.Parent = this;
            element.BindingContext = BindingContextFactory?.Invoke(id) ?? id;
            AbsoluteLayout.SetLayoutFlags(element, WidthIsProportional ? AbsoluteLayoutFlags.SizeProportional : AbsoluteLayoutFlags.HeightProportional);
            m_viewMapping[id] = element;
            return element;
        }

        /// <summary>
        /// To be added
        /// </summary>
        public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
            nameof(BindingContextFactory),
            typeof(Func<int, object>),
            typeof(SlidableLayout));

        /// <summary>
        /// To be added
        /// </summary>
        public Func<int, object> BindingContextFactory
        {
            get { return (Func<int, object>)GetValue(BindingContextFactoryProperty); }
            set { SetValue(BindingContextFactoryProperty, value); }
        }

        /// <summary>
        /// To be added
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(SlidableLayout));

        /// <summary>
        /// Indicates if items should be scaled down when getting further away from the center.
        /// </summary>
        public bool ScaleDown { get; set; } = true;

        /// <summary>
        /// To be added
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
    }
}
