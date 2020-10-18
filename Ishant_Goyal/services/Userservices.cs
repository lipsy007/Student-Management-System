using Ishant_Goyal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ishant_Goyal.services
{
    public interface IUserservices
    {
        IEnumerable<City> GetCity(string requestBody);

        IEnumerable<Course> GetActiveCourse();
        IEnumerable<State> GetStates(); 
        IEnumerable<Student> GetStudents();
        IEnumerable<City> GetCitys();

        User Getuserbyemail(string email);
        Task<Student> CreateAsync(Student Student);
    }
        public class Userservices : IUserservices
        {
        private readonly IshantContext _context;
        

        public Userservices(IshantContext context)
        {
            _context = context;
        }
        public Student Student { get; set; }
        public User data { get; set; }

        public IEnumerable<Course> GetActiveCourse()
        {
            return _context.Course.Where(a => a.IsActive == true);
        }
        public IEnumerable<City> GetCity(string requestBody)
        {
            return (IEnumerable<City>)_context.City.AsNoTracking()
                    .OrderBy(n => n.CityName)
                    .Where(n => n.StateCode == requestBody);
        }
        public IEnumerable<State> GetStates()
        {
            return _context.State;
        }
        public IEnumerable<Student> GetStudents()
        {
            return _context.Student;
        }
        public IEnumerable<City> GetCitys()
        {
            return _context.City;
        }
        public async Task<Student> CreateAsync(Student Student)
        {
            _context.Student.Add(Student);
            await _context.SaveChangesAsync();
            return Student;
        }
        public User Getuserbyemail(string email)
        {
            data = _context.User.Where(e => e.Email == email).FirstOrDefault();
            return data;
        }
    }
}
