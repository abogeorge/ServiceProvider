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
        }

        public DbSet<Customer> Customers
        {
            get;
            set;
        }

    }
}
