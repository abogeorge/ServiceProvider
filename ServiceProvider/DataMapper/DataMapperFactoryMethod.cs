using DataMapper.Implementation;
using DataMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public static class DataMapperFactoryMethod
    {
        public static IDataMapperFactory GetCurrentFactory()
        {
            return new EFDataMapperFactory();
        }
    }
}
