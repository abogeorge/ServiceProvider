using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class MinuteType
    {
        #region Fields

        public MinuteType()
        {
            this.Minutes = new List<Minute>();
        }

        public virtual ICollection<Minute> Minutes
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int MinuteTypeId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Minute Type Name cannot be null!")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "Minute Type Name should have between {3} and {30} letters!")]
        public String MinuteTypeName
        {
            get;
            set;
        }

        #endregion
        #region Validation

        internal static void Validate(SubscriptionType minuteType, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", minuteType, "ValidateMethod", "error", null)
                    );
            }
        }

        #endregion
    }
}
