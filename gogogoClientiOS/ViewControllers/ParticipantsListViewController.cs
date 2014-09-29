using System.Collections.Generic;
using System.Linq;
using gogogoClientiOS.Model;
using gogogoClientiOS.Tools;
using gogogoClientiOS.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace gogogoClientiOS.ViewControllers
{
	public sealed class ParticipantsListViewController : UITableViewController
	{
		public List<ParticipantItem> ParticipantItems { get; private set; } 

		public ParticipantsListViewController (List<ParticipantItem> participantItems)
		{
		    ParticipantItems = new List<ParticipantItem>();
            if (participantItems != null && participantItems.Any())
                ParticipantItems.AddRange(participantItems);

			AppDelegate.Shared.Messenger.Subscribe (this);

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new ParticipantsListTableSource (this);
		}

		public override void LoadView ()
		{
			base.LoadView ();
			View.BackgroundColor = UIColor.White;
		}
	}

	public class ParticipantsListTableSource : UITableViewSource {
	    private const string CellIdentifier = "ParticipantTableCell";
	    private readonly ParticipantsListViewController _parentController;
		public ParticipantsListTableSource (ParticipantsListViewController parentController)
		{
			_parentController = parentController;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _parentController.ParticipantItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (CellIdentifier) as ParticipantCellView ??
			           new ParticipantCellView (CellIdentifier);

		    var participant = indexPath.Row < _parentController.ParticipantItems.Count ?
				_parentController.ParticipantItems [indexPath.Row] : ParticipantItem.NullCustomer ();
			cell.UpdateCell (participant.Name, Converters.FromBase64 (participant.Image));

			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 140;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			//TODO show participant info
		}
	}
}

