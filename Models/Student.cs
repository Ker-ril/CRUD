using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; }
 
        public string LastName { get; set; }

        //public DateAndTime DateOfBirth { get; set; }
        public string  NameOfTheFaculty { get; set; }
    
        public string StudentNumber { get; set; }
    }
}
