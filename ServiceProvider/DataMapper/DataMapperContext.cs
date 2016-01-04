using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DataMapper
{
    class DataMapperContext : DbContext
    {
        public DataMapperContext() : base("connection_string")
        {
            Database.SetInitializer<DataMapperContext>(new DropCreateDatabaseAlways<DataMapperContext>());
        }

        public DbSet<Customer> Customers
        {
            get;
            set;
        }

    }
}
