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
    public class ParticipantItemController : TableController<ParticipantItem>
    {
        private gogogoserverContext _dataContext;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _dataContext = new gogogoserverContext();
            DomainManager = new EntityDomainManager<ParticipantItem>(_dataContext, Request, Services);
        }

        public static readonly Expression<Func<ParticipantItem, ParticipantItemDto>> ConvertToDtoExpression = participantItem => new ParticipantItemDto
        {
            Id = participantItem.Id,
            Name = participantItem.Name,
            Version = participantItem.Version,
            Image = participantItem.Image,
        };

        // GET tables/ParticipantItem
        public IQueryable<ParticipantItemDto> GetAllParticipantItem([FromUri] string eventId)
        {
            //указываем eventId, т.к. хотим получить участников, идущих на событие с id == eventId
            if (string.IsNullOrEmpty(eventId))
                return null;
            return Query().Where(x => x.EventItems.Any(y => y.Id == eventId)).Select(ConvertToDtoExpression);
        }

        // GET tables/ParticipantItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ParticipantItemDto> GetParticipantItem(string id)
        {
            return SingleResult.Create(Lookup(id).Queryable.Select(ConvertToDtoExpression));
        }

        // PATCH tables/ParticipantItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ParticipantItem> PatchParticipantItem(string id, Delta<ParticipantItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/ParticipantItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostParticipantItem(ParticipantItem item)
        {
            ParticipantItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ParticipantItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteParticipantItem(string id)
        {
             return DeleteAsync(id);
        }

    }
}