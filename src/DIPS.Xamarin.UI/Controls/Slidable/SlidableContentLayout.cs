using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// Layout containing moving layout based on Id, which can be created with a Factory.
    /// </summary>
    [ContentProperty(nameof(ItemTemplate))]
    public class SlidableContentLayout : SlidableLayout
    {
        private readonly Dictionary<int, View> m_viewMapping = new Dictionary<int, View>();
        private readonly HashSet<View> m_currentChildren = new HashSet<View>();
        private readonly object m_lock = new object();
        private readonly AbsoluteLayout m_container = new AbsoluteLayout()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            Padding = 0,
            Margin = 0
        };

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public SlidableContentLayout()
        {
            Content = m_container;
            m_container.IsClippedToBounds = true;
        }

        /// <summary>
        ///     Signals to the control that its content should be redrawn.
        /// </summary>
        public void Redraw()
        {
            ResetAll();
        }
        
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="index"></param>
        protected override void OnScrolled(double index)
        {
            if (Config == null)
            {
                return;
            }
            
            lock (m_lock)
            {
                base.OnScrolled(index);
                if (Width < 0.1) return;
                var center = base.Center;
                var itemWidth = base.GetItemWidth();
                var selectedIndex = GetIndexFromValue(index);
                var itemCount = (center * 2) / itemWidth + 1;

                ClearViewCache(selectedIndex, (int)Math.Floor(itemCount));

                var toAdd = new HashSet<View>();
                for (var i = index - itemCount; i <= index + itemCount; i++)
                {
                    var iIndex = (int)Math.Floor(i);
                    if (iIndex < Config.MinValue || iIndex > Config.MaxValue) continue;
                    var view = CreateItem(iIndex);

                    UpdateSelected(view, selectedIndex == iIndex);
                    
                    if (ScaleDown)
                    {
                        var dist = (Math.Abs(index - iIndex) / itemCount);
                        var position = (itemWidth * (1 - dist * 0.33) * (iIndex - index));
                        AbsoluteLayout.SetLayoutBounds(view, new Rectangle(Center + position - itemWidth / 2, 0, ElementWidth, 1));
                        view.Scale = 1 - dist * 0.5;
                    }
                    else
                    {
                        AbsoluteLayout.SetLayoutBounds(view, new Rectangle(Center + (iIndex - index) *itemWidth - itemWidth / 2, 0, ElementWidth, 1));
                    }

                    toAdd.Add(view);
                }

                for (var i = m_container.Children.Count - 1; i >= 0; i--)
                {
                    var item = m_container.Children[i];
                    if (!toAdd.Contains(item))
                    {
                        m_currentChildren.Remove(item);
                        m_container.Children.RemoveAt(i);
                    }
                }

                foreach (var item in toAdd)
                {
                    if (!m_currentChildren.Add(item)) continue;
                    m_container.Children.Add(item);
                }
            }
        }

        private void UpdateSelected(View view, bool selected)
        {
            if (view is ISliderSelectable selectable) selectable.OnSelectionChanged(selected);
            if (view.BindingContext is ISliderSelectable bindingContextSelectable) bindingContextSelectable.OnSelectionChanged(selected);
        }

        private void ClearViewCache(int index, int itemCount)
        {
            if (m_viewMapping.Count > itemCount * 4)
            {
                foreach (var key in m_viewMapping.Select(d => d.Key).ToList())
                {
                    if (Math.Abs(index - key) > itemCount * 2)
                    {
                        var item = m_viewMapping[key];
                        m_currentChildren.Remove(item);
                        m_container.Children.Remove(item);
                        m_viewMapping.Remove(key);
                    }
                }
            }
        }

        private void ResetAll()
        {
            lock (m_lock)
            {
                m_currentChildren.Clear();
                m_container.Children.Clear();
                m_viewMapping.Clear();
                OnScrolled(SlideProperties.Position);
            }
        }

        private View CreateDefault() => new DefaultSliderView();

        private View CreateItem(int id)
        {
            if (m_viewMapping.TryGetValue(id, out var element)) return element;
            element = (View)(ItemTemplate?.CreateContent() ?? CreateDefault());
            element.BindingContext = BindingContextFactory?.Invoke(id) ?? id;
            AbsoluteLayout.SetLayoutFlags(element, WidthIsProportional ? AbsoluteLayoutFlags.SizeProportional : AbsoluteLayoutFlags.HeightProportional);
            m_viewMapping[id] = element;
            return element;
        }

        /// <summary>
        /// <see cref="BindingContextFactory"/>
        /// </summary>
        public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
            nameof(BindingContextFactory),
            typeof(Func<int, object>),
            typeof(SlidableLayout),
            propertyChanged: (s, e, n) => ((SlidableContentLayout)s).ResetAll());

        /// <summary>
        /// Factory used to create instaces of the viewmodels scrolled between. Takes an int and returns an object.
        /// </summary>
        public Func<int, object> BindingContextFactory
        {
            get { return (Func<int, object>)GetValue(BindingContextFactoryProperty); }
            set { SetValue(BindingContextFactoryProperty, value); }
        }

        /// <summary>
        /// <see cref="ItemTemplate"/>
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(SlidableLayout),
            propertyChanged: (s, e, n) => ((SlidableContentLayout)s).ResetAll());


        /// <summary>
        /// Template used in creating each item
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// Indicates if items should be scaled down when getting further away from the center.
        /// </summary>
        public bool ScaleDown { get; set; } = true;
    }
}
