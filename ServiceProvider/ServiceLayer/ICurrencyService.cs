using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ICurrencyService
    {
        void AddCurrency(Currency currency);
        Currency GetCurrencyByName(String currencyName);
        void DropCurrency(String currencyName);
    }
}
