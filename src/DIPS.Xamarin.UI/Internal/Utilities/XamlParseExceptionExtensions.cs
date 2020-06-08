using System;
using System.Xml;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.Utilities
{
    internal static class XamlParseExceptionExtension
    {

        /// <summary>
        /// Internal XamlParseException that automatically adds xml line info when passed a service provider
        /// </summary>
        public static XamlParseException WithXmlLineInfo(this XamlParseException xamlParseException, IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                var lineInfo = GetLineInfo(serviceProvider);
                var message = FormatMessage(xamlParseException.Message, lineInfo);
                return new XamlParseException(message, xamlParseException.InnerException);
            }
            else
            {
                return xamlParseException;
            }
        }

        private static string FormatMessage(string message, IXmlLineInfo xmlinfo)
        {
            if (xmlinfo == null || !xmlinfo.HasLineInfo())
            {
                return message;
            }

            return string.Format("Position {0}:{1}. {2}", xmlinfo.LineNumber, xmlinfo.LinePosition, message);
        }

        private static IXmlLineInfo GetLineInfo(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(typeof(IXmlLineInfoProvider)) is IXmlLineInfoProvider lineInfoProvider ? lineInfoProvider.XmlLineInfo : new XmlLineInfo();
        }
    }
}
