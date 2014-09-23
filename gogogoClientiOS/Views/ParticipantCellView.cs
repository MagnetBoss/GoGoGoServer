using System.Drawing;
using MonoTouch.UIKit;

namespace gogogoClientiOS.Views
{
	public class ParticipantCellView : UITableViewCell
	{
		private UILabel _headingLabel;
		private UIImageView _imageView;

		public ParticipantCellView (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.None;

			_imageView = new UIImageView();

			_headingLabel = new UILabel () {
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.Black
			};
			_headingLabel.Font = UIFont.FromName("HelveticaNeue-Light", 17f);
			ContentView.Add(_imageView);
			ContentView.Add(_headingLabel);
		}

		public void UpdateCell (string userName, UIImage userImage)
		{
			_headingLabel.Text = userName;
			_imageView.Image = userImage;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			const float imageWidth = 40;
			const float imageHeight = 40;
			const float padding = 9;
			const float paddingBetweenLabels = 3;
			const float nameLabelHeight = 17;

			_imageView.Frame = new RectangleF(padding, padding, imageWidth, imageHeight);
			_headingLabel.Frame = new RectangleF(padding + imageWidth + padding, padding + imageHeight / 2 - nameLabelHeight / 2, ContentView.Bounds.Width - 2 * padding - imageWidth, nameLabelHeight);
		}
	}
}

