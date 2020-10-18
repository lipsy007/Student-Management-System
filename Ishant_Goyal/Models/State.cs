using System;
using System.Collections.Generic;

namespace Ishant_Goyal.Models
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
            Student = new HashSet<Student>();
        }

        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
