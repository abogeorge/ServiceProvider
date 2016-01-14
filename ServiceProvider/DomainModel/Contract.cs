using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Contract
    {
        #region Fields
        public Contract()
        {
            this.Customer = new Customer();
            this.Subscription = new Subscription();
        }

        public virtual Customer Customer
        {
            get;
            set;
        }

        public virtual Subscription Subscription
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ContractId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Start date cannot be null!")]
        public DateTime StarDate
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "End date cannot be null!")]
        public DateTime EndDate
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Subscription Name cannot be null!")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "Subscription Name should have between {3} and {30} letters!")]
        public String SubscriptionName
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Price cannot be null!")]
        [RangeValidator(0.0, RangeBoundaryType.Inclusive, 1000.0, RangeBoundaryType.Inclusive, ErrorMessage = "Price should be a double value between 0 and 1000")]
        public Double Price
        {
            get;
            set;
        }

        #endregion
        #region Validation

        internal static void Validate(Contract contract, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", contract, "ValidateMethod", "error", null)
                    );
            }
        }

        // TODO : ADD VALIDATION
        #endregion
    }
}
