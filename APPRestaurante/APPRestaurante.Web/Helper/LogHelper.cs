using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace APPRestaurante.Web.Helper
{
    public class LogHelper
    {
        public static log4net.ILog GetLogger([CallerFilePath]string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}