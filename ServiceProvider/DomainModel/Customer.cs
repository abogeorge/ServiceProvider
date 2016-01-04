using Microsoft.Practices.EnterpriseLibrary.Validation;
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
    public class Customer
    {

        #region Fields

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "First name of the customer cannot be null!")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "First name should have between {3} and {30} letters!")]
        public String FirstName
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Last name of the customer cannot be null!")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 30, RangeBoundaryType.Inclusive, ErrorMessage = "Last name should have between {3} and {30} letters")]
        public String LastName
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Adress of the customer cannot be null!")]
        [StringLengthValidator(5, RangeBoundaryType.Inclusive, 100, RangeBoundaryType.Inclusive, ErrorMessage = "Adress should have between {5} and {100} letters!")]
        public String Adress
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "The email of the customer cannot be null!")]
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid email!")]
        public String Email
        {
            get;
            set;
        }

        [RegexValidator(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Invalid phone number!")]
        public String Phone
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "The CNP of the customer cannot be null!")]
        [RegexValidator(@"^[1-9]\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])(0[1-9]|[1-4]\d|5[0-2]|99)(00[1-9]|0[1-9]\d|[1-9]\d\d)\d$", ErrorMessage = "Invalid CNP!")]
        public String CNP
        {
            get;
            set;
        }

        #endregion

        #region Validations

        //private bool ValidateCNP()
        //{
        //    try
        //    {
        //        if (CNP.Length < 13)
        //            return false;

        //        int suma = Int32.Parse(CNP[0].ToString()) * 2 +
        //            Int32.Parse(CNP[1].ToString()) * 7 +
        //            Int32.Parse(CNP[2].ToString()) * 9 +
        //            Int32.Parse(CNP[3].ToString()) * 1 +
        //            Int32.Parse(CNP[4].ToString()) * 4 +
        //            Int32.Parse(CNP[5].ToString()) * 6 +
        //            Int32.Parse(CNP[6].ToString()) * 3 +
        //            Int32.Parse(CNP[7].ToString()) * 5 +
        //            Int32.Parse(CNP[8].ToString()) * 8 +
        //            Int32.Parse(CNP[9].ToString()) * 2 +
        //            Int32.Parse(CNP[10].ToString()) * 7 +
        //            Int32.Parse(CNP[11].ToString()) * 9;
        //        int rest = suma % 11;

        //        bool valid = false;
        //        if (
        //            (rest < 10) && (rest.ToString() == CNP[12].ToString())
        //            ||
        //            (rest == 10) && (CNP[12] == '1')
        //            )
        //        {
        //            valid = true;
        //        }

        //        return valid;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}

        internal static void Validate(Customer customer, ValidationResults results)
        {
            if (true)
            {
                results.AddResult
                    (
                        new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult("some reason from SelfValidation method", customer, "ValidateMethod", "error", null)
                    );
            }
        }

        #endregion

    }
}
