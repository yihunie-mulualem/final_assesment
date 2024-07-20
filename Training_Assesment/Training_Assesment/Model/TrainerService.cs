using Training_Assesment.DBContext;

namespace Training_Assesment.Model
{
    public class TrainerEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Trainner_firstName { get; set; }
        public string? Trainner_lastname { get; set; }
        public DateTime? Training_Date { get; set; }
        public int CourseId { get; set; }
    }
    public delegate void TrainerAddedEventHandler(object sender, TrainerEventArgs e);

    public class TrainerService
    {
        public event TrainerAddedEventHandler TrainerAdded;
        public TrainingDbContext _context;
        public TrainerService(TrainingDbContext trainingDbcontext)
        {
            _context = trainingDbcontext;
        }
        public async Task AddTrainerAsync(Trainner trainer)
        {
            try
            {
                await _context.trainners.AddAsync(trainer);
                await _context.SaveChangesAsync();
                OnTrainerAdded(trainer);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

   
        protected virtual void OnTrainerAdded(Trainner trainer)
        {
            TrainerAdded?.Invoke(this, new TrainerEventArgs
            {
                Id = trainer.Id,
                Trainner_firstName = trainer.Trainner_firstName,
                Trainner_lastname=trainer.Trainner_lastname,
                Training_Date=trainer.Training_Date,
                CourseId=trainer.CourseId,

            });
 
        }
    }
}
