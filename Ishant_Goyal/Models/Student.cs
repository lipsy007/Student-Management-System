using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ishant_Goyal.Models
{
    public enum Gradetype
    {
        A = 10, B = 9, C = 8, D = 7, E = 6
    }
    public partial class Student /*: IValidatableObject*/
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First Name ")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name ")]

        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [ValidationForDate("12/1/1999")]
        public DateTime Dob { get; set; }
        [Range(7000, 14000)]
        [Required(ErrorMessage = "Fees Cannot be Null")]
        [DataType(DataType.Currency)]
        public decimal Fees { get; set; }
        [Display(Name = "Course")]
        public String CourseId { get; set; }
        public Course Course { get; set; }
        [Required(ErrorMessage = "Email Cannot be null")]
        public string Email { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Tax { get; set; }
        [DataType(DataType.Date)]
        //[ValidationForDate(ErrorMessage = "The Date must ve  grater than 31/12/2019")]
        [ValidationForDate("12/31/2019")]
        [Display(Name = "Enrollment Date")]
        public DateTime? EnrollmentDate { get; set; }

        public Gradetype Grade { get; set; }
        public string StateCode { get; set; }
        public string CityCode { get; set; }
        //public City City { get; set; }
        //public State State { get; set; }
        public virtual City CityCodeNavigation { get; set; }
        public virtual State StateCodeNavigation { get; set; }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    string d = "12/1/1999";
        //    DateTime date1 = DateTime.Parse(d);
        //    List<ValidationResult> results = new List<ValidationResult>();

        //    if (Dob < date1)
        //    {
        //        results.Add(new ValidationResult("Date of Birth must be greater than 1999", new[] { "Dob" }));
        //    }
        //    return results;

        //}


    }
}
