using CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private static List<Student> _students = new List<Student>
        {
            new Student { Id = 1, FirstName = "Viki", LastName = "Babenko", NameOfTheFaculty = "Engineering", StudentNumber = "12345" },
            new Student { Id = 2, FirstName = "Artem", LastName = "Herasymchuk", NameOfTheFaculty = "Mathematic", StudentNumber = "67890" },
            new Student { Id = 3, FirstName = "Viki", LastName = "Venher", NameOfTheFaculty = "English", StudentNumber = "13579" },
            new Student { Id = 4, FirstName = "Natallia", LastName = "Pavliuk", NameOfTheFaculty = "P/A", StudentNumber = "24680" },
            new Student { Id = 5, FirstName = "Natali", LastName = "Divchur", NameOfTheFaculty = "Mathematic", StudentNumber = "95173" }
        };


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_students);
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult GetById(int id)
        {
            var student = _students.Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student == null)
            {
                return BadRequest("Invalid data");
            }

            student.Id = _students.Count + 1;
            _students.Add(student);

            return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Student updatedStudent)
        {
            var existingStudent = _students.Find(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            //existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
            existingStudent.NameOfTheFaculty = updatedStudent.NameOfTheFaculty;
            existingStudent.StudentNumber = updatedStudent.StudentNumber;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _students.Find(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            _students.Remove(student);
            return NoContent();
        }
    }
}
