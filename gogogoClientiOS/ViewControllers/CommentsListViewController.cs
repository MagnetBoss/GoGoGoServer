using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using gogogoClientiOS.BusinessService;
using gogogoClientiOS.Model;
using gogogoClientiOS.Tools;
using gogogoClientiOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public sealed class CommentsListViewController : UITableViewController
    {
	    private readonly string _eventId;
	    public List<CommentItem> CommentItems { get; private set; } 

		public CommentsListViewController (string eventId)
		{
		    _eventId = eventId;
		    CommentItems = new List<CommentItem> ();

			AppDelegate.Shared.Messenger.Subscribe (this);

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new CommentsListTableSource (this);
		}

        public override void LoadView ()
		{
			base.LoadView ();
			View.BackgroundColor = UIColor.White;
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            EventService.GetInstance().Init();
            await EventService.GetInstance().InitializeStoreAsync();
            await RefreshAsync();
            AddRefreshControl();
		}

        private void AddRefreshControl()
        {
            if (!UIDevice.CurrentDevice.CheckSystemVersion(6, 0)) return;
            // the refresh control is available, let's add it
            RefreshControl = new UIRefreshControl();
            RefreshControl.ValueChanged += async (sender, e) =>
            {
                await RefreshAsync();
            };
        }

        private bool _isLoading;
        private async Task RefreshAsync()
		{
            if (_isLoading)
                return;
            _isLoading = true;
            if (RefreshControl != null)
                RefreshControl.BeginRefreshing();
            try
            {
                await CommentService.GetInstance().SyncAsync();
                CommentItems.Clear();
                CommentItems.AddRange(await CommentService.GetInstance().GetItems());
                TableView.ReloadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _isLoading = false;
                if (RefreshControl != null)
                    RefreshControl.EndRefreshing();
            }
		}
	}

	public class CommentsListTableSource : UITableViewSource {
	    private const string CellIdentifier = "CommentTableCell";
	    private readonly CommentsListViewController _parentController;
		public CommentsListTableSource (CommentsListViewController parentController)
		{
			_parentController = parentController;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _parentController.CommentItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (CellIdentifier) as CommentCellView ?? new CommentCellView (CellIdentifier);

		    var comment = indexPath.Row < _parentController.CommentItems.Count ?
				_parentController.CommentItems [indexPath.Row] : CommentItem.NullComment ();

			/*ParticipantItem participant;
			if (!string.IsNullOrEmpty (comment.CustomerId)) {
				participant = ParticipantService.GetInstance ().GetItems ().FirstOrDefault (item => item.Id == comment.CustomerId) ?? ParticipantItem.LoadingCustomer (comment.CustomerId);
			} else {
				participant = ParticipantItem.NullCustomer ();
			}

			cell.UpdateCell (comment.Text, participant, Converters.FromBase64 (participant.Image));
            */
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//TODO show participant info
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = new CommentCellView(CellIdentifier);

			var item = indexPath.Row < _parentController.CommentItems.Count ? _parentController.CommentItems [indexPath.Row] : CommentItem.NullComment ();
			cell.UpdateCell (item.Text, ParticipantItem.NullCustomer(), null);

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

