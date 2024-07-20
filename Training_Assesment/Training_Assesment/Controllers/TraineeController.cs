using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training_Assesment.DBContext;
using Training_Assesment.Model;

namespace Training_Assesment.Controllers
{
    public class TraineeController : Controller
    {
        private TrainingDbContext _context;
        private TraineeService _traineeservice;
        public TraineeController(TrainingDbContext context,TraineeService traineeService)
        {
            _context = context;
            _traineeservice = traineeService;
            _traineeservice.TraineeAdded += OnTraineeAdded;
        }
        [HttpGet]
        [Route("api/[controller]/GetTrainee")]
        public async Task<ActionResult<IEnumerable<Trainee>>> GetTrainee()
        {
            try
            {
                var Trainees = await _context.trainees.ToListAsync();
                return Trainees; // Return the list of  as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetTraineeById")]
        public async Task<ActionResult<Trainee>> GetTraineeById(int id)
        {
            try
            {
                 
                 var trainee= await _context.trainees.FindAsync(id);
                return trainee; // Return the list of  as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("api/[controller]/InsertTrainee")]
        public async Task<IActionResult> InsertTrainee(Trainee trainee)
        {
            try
            {
                await _traineeservice.AddTraineeAsync(trainee);
                return Ok("Trianee's are inserted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("api/[controller]/UpdateTrainee")]
        public async Task<IActionResult> UpdateTrainee(Trainee trainee)
        {
            try
            {
                if (trainee == null)
                {
                    return BadRequest("Course is null.");
                }
                var existingTrainee = _context.trainees.Find(trainee.Id);
                if (existingTrainee == null)
                {
                    return NotFound($"Trainee with ID {trainee.Id} not found.");
                }
                existingTrainee.Trainee_Name = trainee.Trainee_Name;
                existingTrainee.Trainee_Department = trainee.Trainee_Department;
                existingTrainee.CourseId= trainee.CourseId;
                _context.trainees.Update(existingTrainee);
                await _context.SaveChangesAsync();
                return Ok("Trainee updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteTrainee")]
        public async Task<IActionResult> DeleteTrainee(int id)
        {
            try
            {
                Trainee trainee = _context.trainees.Find(id);
                _context.trainees.Remove(trainee);
               await _context.SaveChangesAsync();
                return Ok("Trainess are Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public virtual void OnTraineeAdded(object sender, TraineeEventArgs e)
        {
            Console.WriteLine($"Trainee added: {e.Id}");
        }
    }
}
