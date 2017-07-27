using System;
using System.Reflection;

namespace BeerShop.Logging
{
    public static class GlobalLogger
    {
        public static ILogger GetLogger(string name) => new Log4netLogger(name);
        public static ILogger GetLogger(Type type) => new Log4netLogger(type);
        public static ILogger GetLogger(string repository, string name) => new Log4netLogger(repository, name);
        public static ILogger GetLogger(string repository, Type type) => new Log4netLogger(repository, type);
        public static ILogger GetLogger(Assembly assembly, string name) => new Log4netLogger(assembly, name);
        public static ILogger GetLogger(Assembly assembly, Type type) => new Log4netLogger(assembly, type);
        public static ILogger DefaultLogger => new Log4netLogger(typeof(GlobalLogger));
    }
}
