using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface ICustomerFactory
    {
        void AddCustomer(Customer customer);
        Customer GetCustomerByCNP(String cnp);
        void DropCustomerByCNP(String cnp);
        void UpdateLastName(Customer customer);
        void UpdateFirstName(Customer customer);
        void UpdateAdress(Customer customer);
        void UpdateEmail(Customer customer);
        void UpdatePhone(Customer customer);

    }
}
