using System;
using System.Reflection;
using BeerShop.Logging;

namespace BeerShop.Logging
{
    public static class GlobalLogger
    {
        public static ILogger GetLogger(string name) => new Logger(name);
        public static ILogger GetLogger(Type type) => new Logger(type);
        public static ILogger GetLogger(string repository, string name) => new Logger(repository, name);
        public static ILogger GetLogger(string repository, Type type) => new Logger(repository, type);
        public static ILogger GetLogger(Assembly assembly, string name) => new Logger(assembly, name);
        public static ILogger GetLogger(Assembly assembly, Type type) => new Logger(assembly, type);
        public static ILogger DefaultLogger => new Logger("BeerShop.Logging.GlobalLogger");
    }
}
