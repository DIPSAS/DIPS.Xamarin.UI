using System;
using System.Collections.Generic;
using System.Text;
using DIPS.Xamarin.UI.Internal.Utilities;
using DIPS.Xamarin.UI.Resources.Geometries;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Extensions.Markup
{
    [ContentProperty(nameof(Data))]
    public class DIPSGeometryExtension : IMarkupExtension<PathGeometry>
    {
        public Resources.Geometries.GeometryData.Identifier? Data { get; set; }

        public PathGeometry ProvideValue(IServiceProvider serviceProvider)
        {
            if(Data == null)
            {
                throw new XamlParseException($"{Data} should not be null").WithXmlLineInfo(serviceProvider);

            }
            return GeometryData.FromIdentifier((GeometryData.Identifier)Data);
        }


        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
