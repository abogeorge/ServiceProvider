using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Minute
    {
        #region Fields

        public Minute()
        {
            this.MinuteType = new MinuteType();
        }

        public virtual MinuteType MinuteType
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int MinuteId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Number of included minutes cannot be null!")]
        [RangeValidator(0, RangeBoundaryType.Inclusive, 100000, RangeBoundaryType.Inclusive, ErrorMessage = "Number of included minutes must be a value between 0 and 100000")]
        public int IncludedMinutes
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Extra charge cannot be null!")]
        [RangeValidator(0.0, RangeBoundaryType.Inclusive, 1000.0, RangeBoundaryType.Inclusive, ErrorMessage = "Extra charge must be a double value between 0 and 1000")]
        public Double ExtraCharge
        {
            get;
            set;
        }

        #endregion
        #region Validation

        internal static void Validate(Minute minute, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", minute, "ValidateMethod", "error", null)
                    );
            }
        }

        #endregion
    }
}
