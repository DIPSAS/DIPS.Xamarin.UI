using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    [ContentProperty(nameof(ItemTemplate))]
    public class SlidableContentLayout : SlidableLayout
    {
        private readonly Dictionary<int, View> m_viewMapping = new Dictionary<int, View>();
        private readonly AbsoluteLayout m_container = new AbsoluteLayout();
        public SlidableContentLayout()
        {
            Content = m_container;
            m_container.IsClippedToBounds = true;
        }

        protected override void OnScrolled(double index, double offset, int selectedIndex)
        {
            if (Width < 0.1) return;
            if (m_viewMapping.Count > 1000)
            {
                m_viewMapping.Clear(); // Simple cache clearing
            }

            base.OnScrolled(index, offset, selectedIndex);
            var itemWidth = GetItemWidth();
            var totalWidth = (Width/ itemWidth);
            m_container.Children.Clear();

            for (var i = selectedIndex - totalWidth/2-1; i <= selectedIndex + totalWidth/2+1; i++)
            {
                var pos = (int)Math.Floor(i);
                if (pos < Config.MinValue || pos > Config.MaxValue) continue;
                var isSelected = selectedIndex == pos;
                var view = CreateItem(pos);
                if (view is ISliderSelectable selectable) selectable.OnSelectionChanged(isSelected);
                AbsoluteLayout.SetLayoutBounds(view, new Rectangle(offset  + itemWidth * (i-index), 0, ElementWidth, 1));
                m_container.Children.Add(view);
            }
        }

        private View CreateDefault() => new DefaultSliderView();

        private View CreateItem(int id)
        {
            //TODO: Reuse elements? Try without first.
            if (m_viewMapping.TryGetValue(id, out var element)) return element;
            element = (View)(ItemTemplate?.CreateContent() ?? CreateDefault());
            element.Parent = this;
            element.BindingContext = BindingContextFactory?.Invoke(id) ?? id;
            AbsoluteLayout.SetLayoutFlags(element, WidthIsProportional ? AbsoluteLayoutFlags.SizeProportional : AbsoluteLayoutFlags.HeightProportional);
            m_viewMapping[id] = element;
            return element;
        }


        public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
            nameof(BindingContextFactory),
            typeof(Func<int, object>),
            typeof(SlidableLayout));

        public Func<int, object> BindingContextFactory
        {
            get { return (Func<int, object>)GetValue(BindingContextFactoryProperty); }
            set { SetValue(BindingContextFactoryProperty, value); }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(SlidableLayout));

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
    }
}
