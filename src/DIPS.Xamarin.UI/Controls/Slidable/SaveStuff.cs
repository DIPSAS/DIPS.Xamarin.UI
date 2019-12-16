//using System;
//namespace DIPS.Xamarin.UI.Controls.Slidable
//{
//    public class SaveStuff
//    {
//        public SaveStuff()
//        {
//private readonly AbsoluteLayout m_container;
//private readonly Dictionary<int, Element> m_viewMapping = new Dictionary<int, Element>();
//private readonly List<Element> m_viewport = new List<Element>();
//m_container = new AbsoluteLayout();
//Content = m_container;

//private Element CreateDefault()
//{
//    return new Frame
//    {
//        VerticalOptions = LayoutOptions.FillAndExpand,
//        HorizontalOptions = LayoutOptions.Center,
//        WidthRequest = 5,
//        BackgroundColor = Color.Black,
//        BorderColor = Color.Black,
//        HasShadow = false,
//    };
//}

//private Element CreateItem(int id)
//{
//    //TODO: Reuse elements? Try without first.
//    if (m_viewMapping.TryGetValue(id, out var element)) return element;
//    element = (Element)(ItemTemplate?.CreateContent() ?? CreateDefault());
//    element.Parent = this;
//    element.BindingContext = BindingContextFactory?.Invoke(id);
//    AbsoluteLayout.SetLayoutFlags(element, Config.WidthIsProportional ? AbsoluteLayoutFlags.SizeProportional : AbsoluteLayoutFlags.None);
//    m_viewMapping[id] = element;
//    return element;
//}


//public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
//    nameof(BindingContextFactory),
//    typeof(Func<int, object>),
//    typeof(SlidableLayout));

//public Func<int, object> BindingContextFactory
//{
//    get { return (Func<int, object>)GetValue(BindingContextFactoryProperty); }
//    set { SetValue(BindingContextFactoryProperty, value); }
//}


//public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
//    nameof(ItemTemplate),
//    typeof(DataTemplate),
//    typeof(SlidableLayout));

//public DataTemplate ItemTemplate
//{
//    get => (DataTemplate)GetValue(ItemTemplateProperty);
//    set => SetValue(ItemTemplateProperty, value);
//}
