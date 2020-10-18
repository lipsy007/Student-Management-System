using System;
using System.Collections.Generic;

namespace Ishant_Goyal.Models
{
    public partial class Instructor
    {
        public Instructor()
        {
            CourseAssignment = new HashSet<CourseAssignment>();
        }

        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public DateTime JoiningDate { get; set; }

        public virtual ICollection<CourseAssignment> CourseAssignment { get; set; }
    }
}
