using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Common
{
    public class ServiceProviderLogger
    {
        private static ServiceProviderLogger instance;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ServiceProviderLogger()
        {
        }

        public static ServiceProviderLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new ServiceProviderLogger();
                return instance;
            }
            else
            {
                return instance;
            }
        }

        public void logInfo(String message)
        {
            log.Info(message);
        }

        public void logError(String message)
        {
            log.Error(message);
        }
    }
}
