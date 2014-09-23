using System;
using System.Collections.Generic;
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
	public class ParticipantsListViewController : UITableViewController, IHandle<ItemsChangedMessage<EventItem>>
	{
		public List<CustomerItem> ParticipantItems { get; private set; } 

		public ParticipantsListViewController ()
		{
			ParticipantItems = new List<CustomerItem> ();

			AppDelegate.Shared.Messenger.Subscribe (this);

			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.Source = new ParticipantsListTableSource (this);
		}

		public virtual new void Handle(ItemsChangedMessage<EventItem> message)
		{
			InvokeOnMainThread (() => {
				Refresh ();
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

		private void Refresh ()
		{
			try {
				ParticipantItems.Clear ();

				ParticipantItems.AddRange (ParticipantService.GetInstance ().GetItems ());
				TableView.ReloadData ();
			} catch (Exception ex)
			{

			}
		}
	}

	public class ParticipantsListTableSource : UITableViewSource {
		string cellIdentifier = "ParticipantTableCell";
		private ParticipantsListViewController _parentController;
		public ParticipantsListTableSource (ParticipantsListViewController parentController)
		{
			_parentController = parentController;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _parentController.ParticipantItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			ParticipantCellView cell = tableView.DequeueReusableCell (cellIdentifier) as ParticipantCellView;

			if (cell == null) {
				cell = new ParticipantCellView (cellIdentifier);
			}
			var participant = indexPath.Row < _parentController.ParticipantItems.Count ?
				_parentController.ParticipantItems [indexPath.Row] : CustomerItem.NullCustomer ();
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

