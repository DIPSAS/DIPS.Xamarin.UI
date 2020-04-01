using System;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Extensions
{
    /// <summary>
    /// An extensions class for <see cref="View"/>
    /// </summary>
    public static class ViewExtensions
    {
        /// <summary>
        /// Will send the native implementation of the <see cref="View"/> to a callback method that you need to define in each platform.
        /// This makes it possible to inspect the native view at run time. <see href="https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Inspector">(wiki doc)</see>
        /// </summary>
        /// <remarks>This can not be called unless <see cref="Library.Initialize"/> has been called on the platform</remarks>
        /// <param name="view">The <see cref="View"/> to inspect</param>
        public static void Inspect(this View view)
        {
            if (Inspector.Instance == null)
            {
                throw new Exception($"Library.Initialize needs to be called in the platform before inspecting. Please read the getting started: https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Getting-Started#initializing");
            }
            Inspector.Instance.Inspect(view);
        }
    }
}
