using System.Text.Json;
using CoreBackend.Features.CalendarItems.DTOs;
using CoreBackend.Features.CalendarItems.ROs;
using Microsoft.AspNetCore.Mvc;

namespace CoreBackend.Features.CalendarItems
{
    public interface ICalendarItemsService
    {
        public Task<ActionResult<List<GetCalendarItemRO>>> GetSeveralCalendarItems(CalendarItemsFilterDTO calendarItemsFilter);
        public Task<ActionResult<CreateCalendarItemRO>> CreateCalendarItem(CreateCalendarItemDTO createCalendarItemDTO);
        public Task<ActionResult<GetCalendarItemRO>> GetSpecificCalendarItem(string calendarItemID);
        public Task<ActionResult> RemoveSpecificCalendarItem(string calendarItemID);
        public Task<ActionResult> UpdateSpecificCalendarItem(string calendarItemID, JsonElement updateElement);
    }
}