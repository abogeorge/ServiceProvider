using DataMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DataMapper.Exceptions;

namespace DataMapper.Implementation
{
    class EFCurrencyRateFactory : ICurrencyRateFactory
    {
        public void AddCurrencyRate(CurrencyRate currencyRate, Currency currency)
        {
            using (var context = new DataMapperContext())
            {
                context.Currencies.Attach(currency);
                context.CurrencieRates.Attach(currencyRate);
                context.Entry(currency).Collection(c => c.CurrencyRates).Load();
                context.CurrencieRates.Add(currencyRate);
                context.SaveChanges();
            }
        }

        public void DropCurrencyRate(string currencyRateMonth, Currency currency)
        {
            CurrencyRate currencyRate = GetCurrencyRateByMonth(currencyRateMonth, currency);
            if (currencyRate == null)
            {
                throw new EntityDoesNotExistException("Invalid Currency Rate.");
            }

            using (var context = new DataMapperContext())
            {
                var currencyVar = context.Currencies.Find(currency.CurrencyId);
                var currencyRateVar = context.CurrencieRates.Find(currencyRate.CurrencyRateId);
                context.Entry(currencyVar).Collection("CurrencyRates").Load();
                //context.CurrencieRates.Attach(currencyRate);
                currencyVar.CurrencyRates.Remove(currencyRateVar);
                context.CurrencieRates.Attach(currencyRateVar);
                context.CurrencieRates.Remove(currencyRateVar);
                // context.CurrencieRates.Remove(currencyRate);
                context.SaveChanges();
            }
        }

        public CurrencyRate GetCurrencyRateByMonth(string currencyRateMonth, Currency currency)
        {
            using (var context = new DataMapperContext())
            {
                var currencyRateVar = (from currencyRate in context.CurrencieRates
                                       where currencyRate.Valability.Equals(currencyRateMonth) &&
                                       currencyRate.Currency.CurrencyId == currency.CurrencyId                                                                     
                                       select currencyRate).FirstOrDefault();
                return currencyRateVar;
            }
        }
    }
}
