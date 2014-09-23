using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using gogogoserverService.DataObjects;
using gogogoserverService.Models;

namespace gogogoserverService.Controllers
{
    public class EventItemController : TableController<EventItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new gogogoserverContext();
            DomainManager = new EntityDomainManager<EventItem>(context, Request, Services);
        }

        public static readonly Func<EventItem, EventItemDto> ConvertToDtoExpression = eventItem => new EventItemDto
        {
            Id = eventItem.Id,
            Name = eventItem.Name,
            Description = eventItem.Description,
            Date = eventItem.Date,
            Version = eventItem.Version,
            ShortImage = eventItem.ShortImage,
            LargeImage = eventItem.LargeImage,
            MaxParticipantsCount = eventItem.MaxParticipantsCount,
            MinParticipantsCount = eventItem.MinParticipantsCount,
            Participants =
                eventItem.ParticipantItems.Select(
                    participantItem => ParticipantItemController.ConvertToDtoExpression(participantItem)).ToList(),
            Place = eventItem.PlaceItem != null
                ? new PlaceItemDto
                {
                    Id = eventItem.PlaceItem.Id,
                    Description = eventItem.PlaceItem.Description,
                    Latitude = eventItem.PlaceItem.Latitude,
                    Longitude = eventItem.PlaceItem.Longitude,
                    Title = eventItem.PlaceItem.Title,
                    Version = eventItem.PlaceItem.Version
                }
                : null
        };

        // GET tables/EventItem
        public IQueryable<EventItemDto> GetAllEventItem()
        {
            return Query().Include(eventItem => eventItem.ParticipantItems).Include(eventItem => eventItem.PlaceItem).Select(eventItem => ConvertToDtoExpression(eventItem));
        }

        // GET tables/EventItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<EventItemDto> GetEventItem(string id)
        {
            return SingleResult.Create(Lookup(id).Queryable.Include(eventItem => eventItem.ParticipantItems).Include(eventItem => eventItem.PlaceItem).Select(eventItem => ConvertToDtoExpression(eventItem)));
        }

        // PATCH tables/EventItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<EventItem> PatchEventItem(string id, Delta<EventItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/EventItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostEventItem(EventItem item)
        {
            EventItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/EventItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEventItem(string id)
        {
             return DeleteAsync(id);
        }

    }

}