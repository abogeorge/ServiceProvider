using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    class Customer
    {

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "First name of the customer can not be null")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "First name should have between {3} and {30} letters")]
        public String FirstName
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Last name of the customer can not be null")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "Last name should have between {3} and {30} letters")]
        public String LastName
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Adress of the customer can not be null")]
        [StringLengthValidator(5, RangeBoundaryType.Inclusive, 100, RangeBoundaryType.Inclusive, ErrorMessage = "Adress should have between {5} and {100} letters")]
        public String Adress
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "The email of the customer can not be null")]
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email")]
        public String Email
        {
            get;
            set;
        }

        [RegexValidator(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Invalid phone number")]
        public String Phone
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "The SSN of the user can not be null")]
        public String SSN
        {
            get;
            set;
        }

        #endregion

        #region Validation

        // TODO: Add Validation

        #endregion

    }
}
