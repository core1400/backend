using System.Text.Json;
using CoreBackend.Features.CalendarItems.DTOs;
using CoreBackend.Features.CalendarItems.ROs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoConnection;
using MongoConnection.Collections.CalendarItem;

namespace CoreBackend.Features.CalendarItems
{
    public class CalendarItemsService : ICalendarItemsService
    {
        private CalendarItemRepo _calendarItemRepo;

        public CalendarItemsService(MongoContext mongoContext)
        {
            _calendarItemRepo = new CalendarItemRepo(mongoContext);
        }

        public async Task<ActionResult<List<GetCalendarItemRO>>> GetSeveralCalendarItems(CalendarItemsFilterDTO calendarItemsFilter)
        {
            // IEnumerable<CalendarItem> calendarItems = await _calendarItemRepo.GetAllAsync();
            // List<GetCalendarItemRO> calendarItemsList = new List<GetCalendarItemRO>();
            // foreach (CalendarItem calItem in calendarItems)
            // {
            //     if (calendarItemsFilter.name is not null && calendarItemsFilter.name.Equals(calItem.Name))

            // }
            return new OkResult();
        }

        public async Task<ActionResult<CreateCalendarItemRO>> CreateCalendarItem(CreateCalendarItemDTO createCalendarItemDTO)
        {
            CalendarItem newCalendarItem = new CalendarItem
            {
                PersonalNum = createCalendarItemDTO.personalNum,
                StartAt = createCalendarItemDTO.startAt,
                EndAt = createCalendarItemDTO.endAt,
                Name = createCalendarItemDTO.name,
                Description = createCalendarItemDTO.description,
                BackColor = createCalendarItemDTO.backColor
            };

            await _calendarItemRepo.CreateAsync(newCalendarItem);
            return new CreateCalendarItemRO { calendarItem = newCalendarItem };
        }

        public async Task<ActionResult<GetCalendarItemRO>> GetSpecificCalendarItem(string calendarItemID)
        {
            CalendarItem? wantedCalendarItem = await _calendarItemRepo.GetByIdAsync(calendarItemID);
            if (wantedCalendarItem is null)
                return new NotFoundResult();
            GetCalendarItemRO getCalendarItemRO = new GetCalendarItemRO { calendarItem = wantedCalendarItem };
            return getCalendarItemRO;
        }

        public async Task<ActionResult> RemoveSpecificCalendarItem(string calendarItemID)
        {
            CalendarItem? wantedCalendarItem = await _calendarItemRepo.GetByIdAsync(calendarItemID);
            if (wantedCalendarItem is null)
                return new NotFoundResult();

            await _calendarItemRepo.DeleteByIdAsync(calendarItemID);
            return new OkResult();
        }

        public async Task<ActionResult> UpdateSpecificCalendarItem(string calendarItemID, JsonElement updateElement)
        {
            CalendarItem? wantedCalendarItem = await _calendarItemRepo.GetByIdAsync(calendarItemID);
            if (wantedCalendarItem is null)
                return new NotFoundResult();

            await _calendarItemRepo.UpdateAsync(calendarItemID, updateElement);
            return new OkResult();
        }
    }
}
