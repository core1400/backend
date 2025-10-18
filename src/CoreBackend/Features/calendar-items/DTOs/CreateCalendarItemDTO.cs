namespace CoreBackend.Features.CalendarItems.DTOs
{
    public class CreateCalendarItemDTO
    {
        public required string personalNum {get; set; }
        public required DateTime startAt {get; set; }
        public required DateTime endAt {get; set; }
        public required string name {get; set; }
        public required string description {get; set; }
        public required string backColor {get; set; }
    }
}