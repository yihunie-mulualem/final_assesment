namespace Training_Assesment.Model
{
    public class Trainner
    {
        public int Id { get; set; }
        public string Trainner_firstName { get; set; }
        public string? Trainner_lastname { get; set; }
        public DateTime? Training_Date { get; set; }
        public int CourseId { get; set; }

        // Navigation Property
        public Course Course { get; set; }

    }
}
