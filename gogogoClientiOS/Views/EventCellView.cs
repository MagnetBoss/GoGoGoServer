using System.Drawing;
using MonoTouch.UIKit;

namespace gogogoClientiOS.Views
{
	public class EventCellView : UITableViewCell
	{
		private UILabel _headingLabel, _subheadingLabel;
		private UIImageView _imageView;

		public EventCellView (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.None;

			_imageView = new UIImageView();

			_headingLabel = new UILabel () {
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White
			};
			_headingLabel.Font = UIFont.FromName("HelveticaNeue-Medium", 14f);
			_subheadingLabel = new UILabel () {
				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White
			};
			_subheadingLabel.Font = UIFont.FromName ("HelveticaNeue-Light", 13f);
			ContentView.Add(_imageView);
			ContentView.Add(_headingLabel);
			ContentView.Add(_subheadingLabel);
		}

		public void UpdateCell (string caption, string subtitle, UIImage image)
		{
			_imageView.Image = image;
			_headingLabel.Text = caption;
			_subheadingLabel.Text = subtitle;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			const int yOffset = 95;
			const int xOffset = 10;

			_imageView.Frame = new RectangleF(0, 0, ContentView.Bounds.Width, ContentView.Bounds.Height);
			_headingLabel.Frame = new RectangleF(xOffset, yOffset + 5, ContentView.Bounds.Width - 10, 25);
			_subheadingLabel.Frame = new RectangleF(xOffset, yOffset + 22, ContentView.Bounds.Width - 10, 20);
		}
	}
}

