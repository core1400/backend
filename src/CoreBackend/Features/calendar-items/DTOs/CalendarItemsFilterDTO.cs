namespace CoreBackend.Features.CalendarItems.DTOs
{
    public class CalendarItemsFilterDTO
    {
        public string? personalNum { get; set; }
        public string? name { get; set; }
        public DateTime? startBefore { get; set; }
        public DateTime? startAfter { get; set; }
        public DateTime? endBefore { get; set; }
        public DateTime? endAfter { get; set; }
    }
}