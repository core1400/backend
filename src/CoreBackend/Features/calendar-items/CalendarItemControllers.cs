using CoreBackend.Features.auth;
using CoreBackend.Features.CalendarItems;
using CoreBackend.Features.CalendarItems.DTOs;
using CoreBackend.Features.CalendarItems.ROs;
using CoreBackend.Features.users;
using CoreBackend.Features.Users.DTOs;
using CoreBackend.Features.Users.ROs;
using Microsoft.AspNetCore.Mvc;
using MongoConnection.Enums;
using System.Security.Claims;
using System.Text.Json;

namespace CoreBackend.Features.Users
{
    [ApiController]
    [Route("calendar-items")]
    public class CalendarItemsController : ControllerBase
    {
        private ICalendarItemsService _calendarItemsService;

        public CalendarItemsController(ICalendarItemsService calendarItemsService)
        {
            _calendarItemsService = calendarItemsService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCalendarItemRO>> CreateCalendarItem(CreateCalendarItemDTO createCalendarItemDTO)
        {
            return await _calendarItemsService.CreateCalendarItem(createCalendarItemDTO);
        }

        [HttpGet("{calendarItemID}")]
        public async Task<ActionResult<GetCalendarItemRO>> GetSpecificCalendarItem(string calendarItemID)
        {
            return await _calendarItemsService.GetSpecificCalendarItem(calendarItemID);
        }

        [HttpDelete("{calendarItemID}")]
        public async Task<ActionResult> RemoveSpecificCalendarItem(string calendarItemID)
        {
            return await _calendarItemsService.RemoveSpecificCalendarItem(calendarItemID);
        }

        [HttpPatch("{calendarItemID}")]
        public async Task<ActionResult> UpdateSpecificCalendarItem(string calendarItemID, [FromBody] JsonElement updateElement)
        {
            return await _calendarItemsService.UpdateSpecificCalendarItem(calendarItemID, updateElement);
        }
    }
}