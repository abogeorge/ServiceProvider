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
    class EFCurrencyFactory : ICurrencyFactory
    {
        public void AddCurrency(Currency currency)
        {
            using (var context = new DataMapperContext())
            {
                context.Currencies.Add(currency);
                context.SaveChanges();
            }
        }

        public void DropCurrency(string currencyName)
        {
            Currency currency = GetCurrencyByName(currencyName);
            if (currency == null)
            {
                throw new EntityDoesNotExistException("Invalid Currency Name.");
            }

            using (var context = new DataMapperContext())
            {
                context.Currencies.Attach(currency);
                context.Currencies.Remove(currency);
                context.SaveChanges();
            }
        }

        public Currency GetCurrencyByName(string currencyName)
        {
            using (var context = new DataMapperContext())
            {
                var currencyVar = (from currency in context.Currencies
                                  where currency.CurrencyName.Equals(currencyName)
                                  select currency).FirstOrDefault();
                return currencyVar;
            }
        }
    }
}
