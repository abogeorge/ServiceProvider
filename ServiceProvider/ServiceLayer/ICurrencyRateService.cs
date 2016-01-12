using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ICurrencyRateService
    {
        void AddCurrencyRate(CurrencyRate currencyRate, Currency currency);
        CurrencyRate GetCurrencyRateByMonth(String currencyRateMonth, Currency currency);
        void DropCurrencyRate(String currencyRateMonth, Currency currency);
    }
}
