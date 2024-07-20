namespace Training_Assesment.Model
{
    public class TrainingDto
    {
        public string Trainee_Name { get; set; }
        public string Trainee_Department { get; set; }
   
        public string Trainner_firstName { get; set; }
        public string? Trainner_lastname { get; set; }
        public DateTime? Training_Date { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
    }
}
