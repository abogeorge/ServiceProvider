using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Currency
    {
        #region Fields

        public Currency()
        {

        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int CurrencyId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Currency Name cannot be null!")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "Currency Name should have between {3} and {30} letters!")]
        public String CurrencyName
        {
            get;
            set;
        }

        #endregion
        #region Validation

        internal static void Validate(Currency currencyType, ValidationResults results)
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
