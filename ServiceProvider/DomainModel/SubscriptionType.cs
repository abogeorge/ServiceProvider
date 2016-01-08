using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class SubscriptionType
    {
        #region Fields

        public SubscriptionType()
        {

        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int SubscriptionTypeId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Subscription Type Name cannot be null!")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "Subscription Type Name should have between {3} and {30} letters!")]
        public String SubscriptionTypeName
        {
            get;
            set;
        }

        #endregion
        #region Validation

        internal static void Validate(SubscriptionType subscriptionType, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", subscriptionType, "ValidateMethod", "error", null)
                    );
            }
        }

        #endregion
    }
}
