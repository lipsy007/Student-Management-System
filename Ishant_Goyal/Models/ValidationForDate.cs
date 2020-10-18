using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ishant_Goyal
{
    public class ValidationForDate : ValidationAttribute

    {
        private const string DefaultErrorMessage = "{0} must be greater than {1}.";

        public string OtherProperty { get; private set; }

        public ValidationForDate(string otherProperty) : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherProperty);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date1 = DateTime.Parse(OtherProperty);
                DateTime dateTime = Convert.ToDateTime(value);
                //var otherProperty = validationContext.ObjectInstance.GetType()
                //                   .GetProperty(OtherProperty);

                //var otherPropertyValue = otherProperty
                //              .GetValue(validationContext.ObjectInstance, null);

                if (dateTime < date1)
                {
                    return new ValidationResult(
                      FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }




        //public override bool IsValid(object value)
        //{
        //    string d = "12/31/2019";
        //    DateTime date1 = DateTime.Parse(d);
        //    DateTime dateTime = Convert.ToDateTime(value);

        //    return dateTime>date1;
        //}
    }
}

