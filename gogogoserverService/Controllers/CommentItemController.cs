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
    public class CommentItemController : TableController<CommentItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            var context = new gogogoserverContext();
            DomainManager = new EntityDomainManager<CommentItem>(context, Request, Services);
        }

        private static readonly Func<CommentItem, CommentItemDto> ConvertToDtoExpression = commentItem => new CommentItemDto
        {
            Id = commentItem.Id,
            Text = commentItem.Text,
            Version = commentItem.Version,
            ParticipantItem = ParticipantItemController.ConvertToDtoExpression(commentItem.ParticipantItem)
        };

        // GET tables/CommentItem
        public IQueryable<CommentItemDto> GetAllCommentItem()
        {
            return Query().Include(commentItem => commentItem.ParticipantItem).Select(commentItem => ConvertToDtoExpression(commentItem)); 
        }

        // GET tables/CommentItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<CommentItemDto> GetCommentItem(string id)
        {
            return SingleResult.Create(Lookup(id).Queryable.Include(commentItem => commentItem.ParticipantItem).Select(commentItem => ConvertToDtoExpression(commentItem)));
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