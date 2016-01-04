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
        //private static readonly string factoryType;
        //static DataMapperFactoryMethod()
        //{
        //    factoryType = ConfigurationManager.AppSettings["factoryType"];
        //}

        public static IDataMapperFactory GetCurrentFactory()
        {
            //switch (factoryType.Trim().ToLower())
            //{
            //    case "ef": return new EFDataMapperFactory();
            //   default:
            //        throw new NotImplementedException("cannot find mapper: " + factoryType);
            //}
            return new EFDataMapperFactory();
        }
    }
}
