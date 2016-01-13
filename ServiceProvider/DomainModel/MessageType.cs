using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class MessageType
    {

        #region Fields

        public MessageType()
        {
            this.Messages = new List<Message>();
        }

        public virtual ICollection<Message> Messages
        {
            get;
            set;
        }

        [System.ComponentModel.DataAnnotations.Key,
            System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int MessageTypeId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Message Type Name cannot be null!")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "Message Type Name should have between {3} and {30} letters!")]
        public String MessageTypeName
        {
            get;
            set;
        }

        #endregion
        #region Validation

        internal static void Validate(MessageType messageType, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new ValidationResult("some reason from SelfValidation method", messageType, "ValidateMethod", "error", null)
                    );
            }
        }

        #endregion

    }
}
