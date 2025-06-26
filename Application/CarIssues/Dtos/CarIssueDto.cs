namespace Application.CarIssues.Dtos
{
    public class CarIssueDto
    {
        public int CarIssueId { get; set; }
        public string Description { get; set; }
        public string? OptionalComment { get; set; }
        public string? AIAnalysis { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CarId { get; set; }
    }
}