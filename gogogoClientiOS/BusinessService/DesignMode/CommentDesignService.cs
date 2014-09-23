using System;
using gogogoClientiOS.Model;

namespace gogogoClientiOS.BusinessService.DesignMode
{
	public class CommentDesignService : CommentService
	{
		public override void Init ()
		{

		}

		public override void SetModelIsDirty ()
		{

		}

		public override System.Collections.Generic.List<CommentItem> GetItems ()
		{
			return new System.Collections.Generic.List<CommentItem> {
				new CommentItem {
					Id = "Customer1",
					Text = "My text",
					CustomerId = "Customer1",
					Date = DateTime.Now
				}
			};
		}
	}
}

