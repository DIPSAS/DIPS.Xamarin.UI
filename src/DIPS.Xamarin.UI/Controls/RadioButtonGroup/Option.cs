namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup
{
    public class Option
    {
        public object Identifier { get; }
        public string Name { get; }

        public Option(object identifier, string name)
        {
            Identifier = identifier;
            Name = name;
        }
    }
}