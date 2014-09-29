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
    public class CommentItemController : TableController<CommentItem>
    {
        private gogogoserverContext _context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _context = new gogogoserverContext();
            DomainManager = new EntityDomainManager<CommentItem>(_context, Request, Services);
        }

        // GET tables/CommentItem
        public IQueryable<CommentItemDto> GetAllCommentItem()
        {
            return Query().Select(x => new CommentItemDto
            {
                Id = x.Id,
                Version = x.Version,
                ParticipantImage = x.ParticipantItem.Image,
                ParticipantName = x.ParticipantItem.Name,
                Text = x.Text,
                ParticipantItemId = x.ParticipantItemId,
                EventItemId = x.EventItemId
            }); 
        }

        // GET tables/CommentItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<CommentItemDto> GetCommentItem(string id)
        {
            return SingleResult.Create(Lookup(id).Queryable.Select(x => new CommentItemDto
            {
                Id = x.Id,
                Version = x.Version,
                ParticipantImage = x.ParticipantItem.Image,
                ParticipantName = x.ParticipantItem.Name,
                Text = x.Text,
                ParticipantItemId = x.ParticipantItemId,
                EventItemId = x.EventItemId
            }));
        }

        // PATCH tables/CommentItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<CommentItem> PatchCommentItem(string id, Delta<CommentItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/CommentItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<IHttpActionResult> PostCommentItem(CommentItem item)
        {
            CommentItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/CommentItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCommentItem(string id)
        {
             return DeleteAsync(id);
        }

    }

}