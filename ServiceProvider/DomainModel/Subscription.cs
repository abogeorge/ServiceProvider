using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Subscription
    {

        #region Fields
        public Subscription()
        {
            this.SubType = new SubscriptionType();
            this.Currency = new Currency();
        }

        public virtual SubscriptionType SubType
        {
            get;
            set;
        }

        public virtual Currency Currency
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SubscriptionId
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

        [NotNullValidator(MessageTemplate = "Period cannot be null!")]
        [RangeValidator(0, RangeBoundaryType.Inclusive, 48, RangeBoundaryType.Inclusive, ErrorMessage = "Period should be a double value between 0 and 1000")]
        public int FixedPeriod
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Availability cannot be null!")]
        public bool Available
        {
            get;
            set;
        }

        #endregion
        #region Validation

        internal static void Validate(SubscriptionType subscription, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", subscription, "ValidateMethod", "error", null)
                    );
            }
        }

        #endregion

    }
}
