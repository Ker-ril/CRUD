using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        //public DateAndTime DateOfBirth { get; set; }
        public string  NameOfTheFaculty { get; set; }
        [Required]
        public string StudentNumber { get; set; }
    }
}
