using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class ServiceProviderException : ApplicationException
    {
        public ServiceProviderException(String message):base(message)
        {
        }
    }
}
