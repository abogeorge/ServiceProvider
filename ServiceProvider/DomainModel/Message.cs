using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Message
    {

        #region Fields

        public Message()
        {
            this.MessageType = new MessageType();
        }

        public virtual MessageType MessageType
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int MessageId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Number of included messages cannot be null!")]
        [RangeValidator(0, RangeBoundaryType.Inclusive, 100000, RangeBoundaryType.Inclusive, ErrorMessage = "Number of included messages must be a value between 0 and 100000")]
        public int IncludedMessages
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

        internal static void Validate(Message message, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", message, "ValidateMethod", "error", null)
                    );
            }
        }

        #endregion


    }
}
