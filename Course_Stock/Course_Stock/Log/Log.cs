using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;

namespace Course_Stock.Log
{
    public class Log
    {
        static Log()
        {
            XmlConfigurator.Configure();
        }

        public static ILog For(object LoggedObject)
        {
            if (LoggedObject != null)
                return For(LoggedObject.GetType());
            else
                return For(null);
        }

        public static ILog For(Type ObjectType)
        {
            if (ObjectType != null)
                return LogManager.GetLogger(ObjectType.Name);
            else
                return LogManager.GetLogger(string.Empty);
        }
        public static ILog Add(string s)
        {
            return LogManager.GetLogger(s);
        }
        

    }

}