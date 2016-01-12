using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class CurrencyRate
    {

        #region Fields
        public CurrencyRate()
        {
            this.Currency = new Currency();
        }

        public virtual Currency Currency
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int CurrencyRateId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Currency Rate cannot be null!")]
        [RangeValidator(0.0, RangeBoundaryType.Inclusive, 10000.0, RangeBoundaryType.Inclusive, ErrorMessage = "Currency Rate must be a double value between 0 and 10000")]
        public Double RateToRON
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Currency Rate valability cannot be null!")]
        [RegexValidator(@"\(?\d{2}\)?-?.? *\d{4}$", ErrorMessage = "Invalid currency valability!")]
        public String Valability
        {
            get;
            set;
        }

        #endregion

        #region Validation

        internal static void Validate(CurrencyRate currencyType, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", currencyType, "ValidateMethod", "error", null)
                    );
            }
        }

        #endregion

    }
}
