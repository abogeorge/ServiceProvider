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
        void AddCustomer(Customer customer);
        Customer GetCustomerByCNP(String cnp);
        void DropCustomerByCNP(String cnp);
        void UpdateLastName(String cnp, String lastName);
        void UpdateFirstName(String cnp, String firstName);
        void UpdateAdress(String cnp, String adress);
        void UpdateEmail(String cnp, String email);
        void UpdatePhone(String cnp, String phone);
    }
}
