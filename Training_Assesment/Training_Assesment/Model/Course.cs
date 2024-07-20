namespace Training_Assesment.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public ICollection<Trainner> Trainners { get; set; }
        public ICollection<Trainee> Trainees { get; set; }

    }
}
