using System;
using CRUD.Interface;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var students = _studentRepository.GetAllStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult GetById(int id)
        {
            var student = _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest("Invalid data");
                }

                _studentRepository.AddStudent(student);

                return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student updatedStudent)
        {
            var existingStudent = _studentRepository.GetStudentById(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.NameOfTheFaculty = updatedStudent.NameOfTheFaculty;
            existingStudent.StudentNumber = updatedStudent.StudentNumber;

            _studentRepository.UpdateStudent(existingStudent);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _studentRepository.DeleteStudent(id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
