using System;
namespace DIPS.Xamarin.Forms.IssuesRepro
{
    public class Issue : System.Attribute  
    {
        public int Id;
        public Issue(int id)
        {
            Id = id;
        }
    }
}
