using System.Web.Http;

namespace BeerShop.Api
{
    public static class FormattersConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);
        }
    }
}