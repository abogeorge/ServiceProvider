using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Exceptions
{
    public class DuplicateException : ServiceProviderException
    {
        public DuplicateException(String message) : base(message)
        {
        }
    }
}
