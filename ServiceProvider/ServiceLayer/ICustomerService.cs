using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ICustomerService
    {
        bool AddCustomer(Customer customer);

        Customer GetCustomerByCNP(String cnp);
    }
}
