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
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime StartDate
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "End date cannot be null!")]
        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime EndDate
        {
            get;
            set;
        }

        [NotNullValidator(MessageTemplate = "Subscription Name cannot be null!")]
        [StringLengthValidator(3, RangeBoundaryType.Inclusive, 60, RangeBoundaryType.Inclusive, ErrorMessage = "Subscription Name should have between {3} and {30} letters!")]
        public String ContractName
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

        public bool CheckDateValidity()
        {
            TimeSpan difDay = EndDate.Subtract(StartDate);
            if(((int)difDay.TotalDays / 365) < 28)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        //public int GetVarstaDinCNP(String cnpClient)
        //{
        //    //string sub = cnpClient.Substring(1, 6);
        //    String sex = cnpClient[0].ToString();


        //    String anCNP = "";

        //    if (sex.Equals("1") || sex.Equals("2"))
        //    {
        //        anCNP += "19";
        //    }
        //    else
        //    {
        //        if (sex.Equals("3") || sex.Equals("4"))
        //        {
        //            anCNP += "18";
        //        }
        //        else
        //        {
        //            if (sex.Equals("5") || sex.Equals("6"))
        //            {
        //                anCNP += "20";
        //            }
        //            else
        //            {
        //                throw new ValidationException("CNP Invalid");
        //            }
        //        }

        //    }

        //    anCNP += cnpClient[1].ToString() + cnpClient[2].ToString();


        //    String lunaCNP = cnpClient[3].ToString() + cnpClient[4].ToString();
        //    String ziuaCNP = cnpClient[5].ToString() + cnpClient[6].ToString();
        //    DateTime date = DateTime.ParseExact(anCNP + "-" + lunaCNP + "-" + ziuaCNP, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        //    TimeSpan tSpan = DateTime.Now.Subtract(date);
        //    return ((int)tSpan.TotalDays / 365);
        //}
    }

        #endregion
    }
}
