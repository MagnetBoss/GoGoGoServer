using System;
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
    public class ParticipantItemController : TableController<ParticipantItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new gogogoserverContext();
            DomainManager = new EntityDomainManager<ParticipantItem>(context, Request, Services);
        }

        public static readonly Func<ParticipantItem, ParticipantItemDto> ConvertToDtoExpression = participantItem => new ParticipantItemDto
        {
            Id = participantItem.Id,
            Name = participantItem.Name,
            Version = participantItem.Version
        };

        // GET tables/ParticipantItem
        public IQueryable<ParticipantItemDto> GetAllParticipantItem()
        {
            return Query().Select(participantItem => ConvertToDtoExpression(participantItem)); 
        }

        // GET tables/ParticipantItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ParticipantItemDto> GetParticipantItem(string id)
        {
            return SingleResult.Create(Lookup(id).Queryable.Select(participantItem => ConvertToDtoExpression(participantItem)));
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