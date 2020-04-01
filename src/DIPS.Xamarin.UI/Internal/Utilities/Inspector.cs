using System;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal.Utilities
{
    internal class Inspector
    {
        internal static IInspector? Instance { get; set; }
    }

    internal interface IInspector
    {
        void Inspect(View view);
    }
}