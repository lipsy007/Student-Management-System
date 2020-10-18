using System;
using System.Collections.Generic;

namespace Ishant_Goyal.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseAssignment = new HashSet<CourseAssignment>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CourseAssignment> CourseAssignment { get; set; }
    }
}
