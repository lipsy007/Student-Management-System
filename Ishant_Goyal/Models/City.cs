using System;
using System.Collections.Generic;

namespace Ishant_Goyal.Models
{
    public partial class City
    {
        public City()
        {
            Student = new HashSet<Student>();
        }

        public int CityId { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }

        public virtual State StateCodeNavigation { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
