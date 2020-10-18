using System;
using System.Collections.Generic;

namespace Ishant_Goyal.Models
{
    public partial class CourseAssignment
    {
        public int Id { get; set; }
        public string CourseId { get; set; }
        public int InstructorId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
