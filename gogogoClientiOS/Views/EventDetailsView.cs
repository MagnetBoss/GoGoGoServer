using gogogoClientiOS.ViewControllers;
using MonoTouch.UIKit;

namespace gogogoClientiOS.Views
{
	public class EventDetailsView : UIView
	{
		private UIImageView _imageView;
		private UILabel _nameLabel;
		private UILabel _locationDescriptionLabel;
		private UILabel _dateLabel;
		private UILabel _timeLabel;
		private UIButton _goButton;
		private UILabel _participantsCountLabel;
		private UILabel _participantsCountWordLabel;
		private UILabel _descriptionlabel;
		private EventDetailsViewController _controller;

		public EventDetailsView (EventDetailsViewController controller)
		{
			AppDelegate.Shared.Messenger.Subscribe (this);
			_controller = controller;
		
			BackgroundColor = UIColor.White;
			_imageView = new UIImageView ();
			_nameLabel = new UILabel ();
			_nameLabel.Font = UIFont.FromName ("HelveticaNeue-Medium", 15f);
			_locationDescriptionLabel = new UILabel ();
			_locationDescriptionLabel.Font = UIFont.FromName ("HelveticaNeue-Light", 12f);
			_dateLabel = new UILabel ();
			_dateLabel.Font = UIFont.FromName ("HelveticaNeue-Medium", 15f); 
			_dateLabel.TextAlignment = UITextAlignment.Center;
			_timeLabel = new UILabel ();
			_timeLabel.Font = UIFont.FromName ("HelveticaNeue-Light", 13f); 
			_timeLabel.TextAlignment = UITextAlignment.Center;
			_goButton = UIButton.FromType (UIButtonType.RoundedRect);
			_goButton.SetTitle ("Я пойду!", UIControlState.Normal);
			_goButton.BackgroundColor = UIColor.FromRGB (240, 240, 240);
			_participantsCountLabel = new UILabel ();
			_participantsCountLabel.Font = UIFont.FromName ("HelveticaNeue-Bold", 25f); 
			_participantsCountLabel.TextColor = UIColor.Red;
			_participantsCountLabel.TextAlignment = UITextAlignment.Center;
			_participantsCountWordLabel = new UILabel ();
			_participantsCountWordLabel.Font = UIFont.FromName ("HelveticaNeue-Light", 13f); 
			_participantsCountWordLabel.TextAlignment = UITextAlignment.Center;
			_descriptionlabel = new UILabel ();
			_descriptionlabel.Font = UIFont.FromName ("HelveticaNeue-Light", 14f); 
			_descriptionlabel.LineBreakMode = UILineBreakMode.WordWrap;
			_descriptionlabel.Lines = 0;

			Add (_imageView);
			Add (_nameLabel);
			Add (_locationDescriptionLabel);
			Add (_dateLabel);
			Add (_timeLabel);
			Add (_goButton);
			Add (_participantsCountLabel);
			Add (_participantsCountWordLabel);
			Add (_descriptionlabel);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			_imageView.Frame = new System.Drawing.RectangleF (0, 0, Frame.Width, 180);
			_nameLabel.Frame = new System.Drawing.RectangleF (20, _imageView.Frame.Height + 10, 230, 16);
			_locationDescriptionLabel.Frame = new System.Drawing.RectangleF (20, _nameLabel.Frame.Top + _nameLabel.Frame.Height, _nameLabel.Frame.Width, 20);
			_dateLabel.Frame = new System.Drawing.RectangleF (_nameLabel.Frame.Left + _nameLabel.Frame.Width + 5, _imageView.Frame.Height + 10, Frame.Width - _nameLabel.Frame.Left - _nameLabel.Frame.Width - 25, 16);
			_timeLabel.Frame = new System.Drawing.RectangleF (_dateLabel.Frame.Left, _nameLabel.Frame.Top + _nameLabel.Frame.Height, _dateLabel.Frame.Width, 16);
			_goButton.Frame = new System.Drawing.RectangleF (Frame.Width - 25 - 200, _locationDescriptionLabel.Frame.Top + _locationDescriptionLabel.Frame.Height + 28, 205, 38);
			_participantsCountLabel.Frame = new System.Drawing.RectangleF (10, _goButton.Frame.Top - 5, 80, 28);
			_participantsCountWordLabel.Frame = new System.Drawing.RectangleF (_participantsCountLabel.Frame.Left, _participantsCountLabel.Frame.Top + _participantsCountLabel.Frame.Height, _participantsCountLabel.Frame.Width, 20);
			_descriptionlabel.Frame = new System.Drawing.RectangleF (20, _participantsCountWordLabel.Frame.Top + _participantsCountWordLabel.Frame.Height + 5, Frame.Width - 40, 0);
			_descriptionlabel.PreferredMaxLayoutWidth = Frame.Width - 40;
			_descriptionlabel.SizeToFit ();

			//TODO HACK
			_controller.SetScrollSize(new System.Drawing.SizeF(Frame.Width, _descriptionlabel.Frame.Top + _descriptionlabel.Frame.Height + 20));
		}

		public UIImage Image { 
			get {
				return _imageView.Image;
			}
			set {
				if (_imageView.Image == value)
					return;
				_imageView.Image = value;
			}
		}

		public string Name {
			get {
				return _nameLabel.Text;
			}
			set {
				if (_nameLabel.Text == value)
					return;
				_nameLabel.Text = value;
			}
		}

		public string LocationDescription {
			get {
				return _locationDescriptionLabel.Text;
			}
			set {
				if (_locationDescriptionLabel.Text == value)
					return;
				_locationDescriptionLabel.Text = value;
			}
		}

		public string Date {
			get {
				return _dateLabel.Text;
			}
			set {
				if (_dateLabel.Text == value)
					return;
				_dateLabel.Text = value;
			}
		}

		public string Time {
			get {
				return _timeLabel.Text;
			}
			set {
				if (_timeLabel.Text == value)
					return;
				_timeLabel.Text = value;
			}
		}

		public string ParticipantsCount {
			get {
				return _participantsCountLabel.Text;
			}
			set {
				if (_participantsCountLabel.Text == value)
					return;
				_participantsCountLabel.Text = value;
			}
		}

		public string ParticipantCountWord {
			get {
				return _participantsCountWordLabel.Text;
			}
			set {
				if (_participantsCountWordLabel.Text == value)
					return;
				_participantsCountWordLabel.Text = value;
			}
		}

		public string DescriptionText {
			get { 
				return _descriptionlabel.Text;
			}
			set {
				if (_descriptionlabel.Text == value)
					return;
				_descriptionlabel.Text = value;
				_descriptionlabel.SizeToFit ();
			}
		}

	}
}

