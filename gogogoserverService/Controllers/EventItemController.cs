using System;
using System.Linq;
using System.Linq.Expressions;
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

        public static readonly Expression<Func<EventItem, EventItemDto>>  ConvertToDtoExpression = eventItem => new EventItemDto
        {
            Id = eventItem.Id,
            Name = eventItem.Name,
            Description = eventItem.Description,
            SmallDescription = eventItem.SmallDescription,
            Date = eventItem.Date,
            Version = eventItem.Version,
            ShortImage = eventItem.ShortImage,
            LargeImage = eventItem.LargeImage,
            MaxParticipantsCount = eventItem.MaxParticipantsCount,
            MinParticipantsCount = eventItem.MinParticipantsCount,
            PlaceDescription = eventItem.PlaceItem != null ? eventItem.PlaceItem.Description : null,
            PlaceLatitude = eventItem.PlaceItem != null ? eventItem.PlaceItem.Latitude : 0,
            PlaceLongitude = eventItem.PlaceItem != null ? eventItem.PlaceItem.Longitude : 0,
            PlaceTitle = eventItem.PlaceItem != null ? eventItem.PlaceItem.Title : null,
        };

        // GET tables/EventItem
        public IQueryable<EventItemDto> GetAllEventItem()
        {
            return Query().Select(ConvertToDtoExpression);
        }

        // GET tables/EventItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<EventItemDto> GetEventItem(string id)
        {
            return SingleResult.Create(Lookup(id).Queryable.Select(ConvertToDtoExpression));
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