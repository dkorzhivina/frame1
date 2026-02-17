namespace frame1.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Difficulty { get; set; }
        public int EstimatedHours { get; set; }
    }
}
