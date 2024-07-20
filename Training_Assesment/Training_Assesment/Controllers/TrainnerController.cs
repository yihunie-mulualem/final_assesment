using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training_Assesment.DBContext;
using Training_Assesment.Model;

namespace Training_Assesment.Controllers
{
 
    public class TrainnerController : Controller
    {
        private TrainingDbContext _context;
        private TrainerService _trainerService;
        public TrainnerController(TrainingDbContext context,TrainerService trainerservice)
        {
            _context = context;
            _trainerService = trainerservice;
            _trainerService.TrainerAdded += OnTrainerAdded;

        }
        [HttpGet]
        [Route("api/[controller]/GetTrainer")]
        public async Task<ActionResult<IEnumerable<Trainner>>> GetTrainer()
        {
            try
            {
                var trainners = await _context.trainners.ToListAsync();
                return trainners; // Return the list of  as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetDto")]
        public async Task<ActionResult<IEnumerable<TrainingDto>>> GetDto()
        {
            var trainer = _context.trainners.Include(t => t.Course).ToList();
            var dtoList = new List<TrainingDto>();
                foreach(var train in trainer)
            {
                var trainees = _context.trainees
                      .Where(t => t.CourseId == train.CourseId)
                      .ToList();
                foreach (var TraineeObj in trainees) {
                    var trainingDto = new TrainingDto
                    {
                        Trainee_Name = TraineeObj.Trainee_Name,
                        Trainee_Department = TraineeObj.Trainee_Department,
                        Trainner_firstName = train.Trainner_firstName,
                        Trainner_lastname = train.Trainner_lastname,
                        Training_Date = train.Training_Date,
                        CourseCode = train.Course.CourseCode,
                        CourseName = train.Course.CourseName
                    };

                    dtoList.Add(trainingDto);
                }
            }
            return dtoList;
        }
        [HttpGet]
        [Route("api/[controller]/GetTrainerById")]
        public async Task<ActionResult<Trainner>> GetTrainerById(int id)
        {
            try
            {

                var trainer = await _context.trainners.FindAsync(id);
                return trainer; // Return the list of  as JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("api/[controller]/InsertTrainer")]
        public async Task<IActionResult> InsertTrainer(Trainner trainer)
        {
            try
            {
                await _trainerService.AddTrainerAsync(trainer);
                return Ok("Trainer's are inserted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("api/[controller]/UpdateTrainer")]
        public async Task<IActionResult> UpdateTrainer(Trainner trainner)
        {
            try
            {
                if (trainner == null)
                {
                    return BadRequest("Course is null.");
                }
                var existingTrainer = _context.trainners.Find(trainner.Id);
                if (existingTrainer == null)
                {
                    return NotFound($"Trainer with ID {trainner.Id} not found.");
                }
                existingTrainer.Trainner_firstName = trainner.Trainner_firstName;
                existingTrainer.Trainner_lastname = trainner.Trainner_lastname;
                existingTrainer.CourseId = trainner.CourseId;
                existingTrainer.Training_Date = trainner.Training_Date;
                _context.trainners.Update(existingTrainer);
                await _context.SaveChangesAsync();
                return Ok("Trainers updated successfully.");
            }
            catch (Exception ex)
            {
                // Return an error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteTrainer")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            try
            {
                Trainner trainner = await _context.trainners.FindAsync(id);
                if(trainner != null)
                {
              _context.trainners.Remove(trainner);

                }
                await _context.SaveChangesAsync();
                return Ok("Trainers are Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public virtual void OnTrainerAdded(object sender, TrainerEventArgs e)
        {
            Console.WriteLine($"Trainner added: {e.Id}, {e.Trainner_firstName},{e.Trainner_lastname},{e.Trainner_firstName},{e.Training_Date}");
        }
    }
}
