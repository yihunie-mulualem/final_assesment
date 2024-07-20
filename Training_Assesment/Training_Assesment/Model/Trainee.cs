namespace Training_Assesment.Model
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Trainee_Name { get; set; }
        public string Trainee_Department { get; set; }
        public int CourseId { get; set; }
        public Course course { get; set; }
    }
}
