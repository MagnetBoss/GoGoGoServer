using System.Drawing;
using gogogoClientiOS.Model;
using MonoTouch.UIKit;

namespace gogogoClientiOS.Views
{
	public class CommentCellView : UITableViewCell
	{
		private UILabel _headingLabel, _subheadingLabel;
		private UIImageView _imageView;
		private ParticipantItem _participant;

		public CommentCellView (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.None;

			_imageView = new UIImageView();

			_headingLabel = new UILabel () {
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.Black
			};
			_headingLabel.Font = UIFont.FromName("HelveticaNeue-Medium", 14f);
			_subheadingLabel = new UILabel () {
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.DarkGray
			};
			_subheadingLabel.Font = UIFont.FromName ("HelveticaNeue-Light", 13f);
			ContentView.Add(_imageView);
			ContentView.Add(_headingLabel);
			ContentView.Add(_subheadingLabel);
		}



		public void UpdateParticipantInfo(string participantName)
		{
			_headingLabel.Text = _participant.Name;
		}

		public void UpdateCell (string messageText, ParticipantItem participant, UIImage userImage)
		{
			_participant = participant ?? ParticipantItem.NullCustomer ();

			_imageView.Image = userImage;
			UpdateParticipantInfo (participant.Name);
			_subheadingLabel.Text = messageText;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			const float imageWidth = 40;
			const float imageHeight = 40;
			const float padding = 9;
			const float paddingBetweenLabels = 3;
			const float nameLabelHeight = 15;

			_subheadingLabel.LineBreakMode = UILineBreakMode.WordWrap;
			_subheadingLabel.Frame = new RectangleF(padding + imageWidth + padding, padding + nameLabelHeight + paddingBetweenLabels, ContentView.Bounds.Width - 4 * padding - imageWidth, _subheadingLabel.Frame.Height);
			_subheadingLabel.PreferredMaxLayoutWidth = ContentView.Bounds.Width - 4 * padding - imageWidth;
			_subheadingLabel.Lines = 0;
			_subheadingLabel.SizeToFit ();

			_imageView.Frame = new RectangleF(padding, padding, imageWidth, imageHeight);
			_headingLabel.Frame = new RectangleF(padding + imageWidth + padding, padding, ContentView.Bounds.Width - 4 * padding - imageWidth, nameLabelHeight);
			_expectedRowHeight = _subheadingLabel.Frame.Y + _subheadingLabel.Frame.Height + padding;
		}

		protected override void Dispose (bool disposing)
		{
			if (disposing) {
				AppDelegate.Shared.Messenger.Unsubscribe (this);
			}
			base.Dispose (disposing);
		}

		//HACK
		private float _expectedRowHeight;
		public float ExpectedRowHeight {
			get 
			{
				return _expectedRowHeight;
			}
		}
	}
}

