using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ishant_Goyal.Models
{
    public partial class Enrollment
    {
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }
        public int StudentCount { get; set; }
    }
}
