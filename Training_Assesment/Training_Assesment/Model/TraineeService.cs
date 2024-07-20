using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Training_Assesment.DBContext;

namespace Training_Assesment.Model
{
    public class TraineeEventArgs:EventArgs
    {
        public int Id { get; set; }
        public string Trainee_Name { get; set; }
        public string Trainee_Department { get; set; }
        public int CourseId { get; set; }
        public Course course { get; set; }
    }
    public delegate void TraineeAddedEventHandler(object sender, TraineeEventArgs e);
    public class TraineeService
    {
        public event TraineeAddedEventHandler TraineeAdded;
        private TrainingDbContext _context;
        public TraineeService(TrainingDbContext context)
        {
            _context = context;
        }
        public async Task AddTraineeAsync(Trainee trainee)
        {
            try
            {
                await _context.trainees.AddAsync(trainee);
                await _context.SaveChangesAsync();
                OnTraineeAdded(trainee);
            // return JsonResult("Inserted succssfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Trainee Added");
            }
        }
        protected virtual void OnTraineeAdded(Trainee trainee)
        {
            TraineeAdded?.Invoke(this, new TraineeEventArgs
            {
                Id = trainee.Id

            });
        }
    }
}
