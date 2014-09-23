using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Caliburn.Micro;
using gogogoClientiOS.BusinessService;
using gogogoClientiOS.Model;
using gogogoClientiOS.Model.Messages;
using gogogoClientiOS.Tools;
using gogogoClientiOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public class CommentsListViewController : UITableViewController, IHandle<ItemsChangedMessage<CommentItem>>, IHandle<ItemsChangedMessage<CustomerItem>>
	{
		public List<CommentItem> CommentItems { get; private set; } 

		public CommentsListViewController ()
		{
			CommentItems = new List<CommentItem> ();

			AppDelegate.Shared.Messenger.Subscribe (this);

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new CommentsListTableSource (this);
		}

		public virtual new void Handle(ItemsChangedMessage<CommentItem> message)
		{
			InvokeOnMainThread (() => {
				Refresh ();
			});
		}


		public virtual new void Handle(ItemsChangedMessage<CustomerItem> message)
		{
			var comments = CommentItems.ToList();
			HashSet<NSIndexPath> changedRows = new HashSet<NSIndexPath> ();
			for (int i = 0; i < comments.Count; ++i) {
				var participant = message.Items.FirstOrDefault (item => item.Id == comments[i].CustomerId);
				if (participant == null)
					return;
				changedRows.Add (NSIndexPath.FromRowSection(i, 0));
			}

			InvokeOnMainThread (() => {
				TableView.ReloadRows(changedRows.ToArray(), UITableViewRowAnimation.None);
			});
		}

		public override void LoadView ()
		{
			base.LoadView ();
			View.BackgroundColor = UIColor.White;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Refresh ();
		}

		void Refresh ()
		{
			try {
				CommentItems.Clear ();
				CommentItems.AddRange (CommentService.GetInstance ().GetItems ().OrderBy(comment => comment.Date).ToList());
				TableView.ReloadData ();
			} catch (Exception ex)
			{

			}
		}
	}

	public class CommentsListTableSource : UITableViewSource {
		string cellIdentifier = "CommentTableCell";
		private CommentsListViewController _parentController;
		public CommentsListTableSource (CommentsListViewController parentController)
		{
			_parentController = parentController;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _parentController.CommentItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			CommentCellView cell = tableView.DequeueReusableCell (cellIdentifier) as CommentCellView;

			if (cell == null) {
				cell = new CommentCellView (cellIdentifier);
			}
			var comment = indexPath.Row < _parentController.CommentItems.Count ?
				_parentController.CommentItems [indexPath.Row] : CommentItem.NullComment ();

			CustomerItem participant;
			if (!string.IsNullOrEmpty (comment.CustomerId)) {
				participant = ParticipantService.GetInstance ().GetItems ().FirstOrDefault (item => item.Id == comment.CustomerId) ?? CustomerItem.LoadingCustomer (comment.CustomerId);
			} else {
				participant = CustomerItem.NullCustomer ();
			}

			cell.UpdateCell (comment.Text, participant, Converters.FromBase64 (participant.Image));

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//TODO show participant info
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = new CommentCellView(cellIdentifier);

			var item = indexPath.Row < _parentController.CommentItems.Count ? _parentController.CommentItems [indexPath.Row] : CommentItem.NullComment ();
			cell.UpdateCell (item.Text, CustomerItem.NullCustomer(), null);

			cell.SetNeedsUpdateConstraints();
			cell.UpdateConstraintsIfNeeded();

			cell.Bounds = new RectangleF(0, 0, _parentController.View.Bounds.Width, _parentController.View.Bounds.Height);

			cell.SetNeedsLayout();
			cell.LayoutIfNeeded();

			return cell.ExpectedRowHeight < 60 ? 60 : cell.ExpectedRowHeight;
		}

		public override float EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			// NOTE for iOS 7.0.x ONLY, this bug has been fixed by Apple as of iOS 7.1:
			// A constraint exception will be thrown if the estimated row height for an inserted row is greater
			// than the actual height for that row. In order to work around this, we need to return the actual
			// height for the the row when inserting into the table view - uncomment the below 3 lines of code.
			// See: https://github.com/caoimghgin/TableViewCellWithAutoLayout/issues/6
			//            if (this.isInsertingRow)
			//            {
			//                return this.GetHeightForRow(tableView, indexPath);
			//            }

			return UITableView.AutomaticDimension;
		}

	}
}

