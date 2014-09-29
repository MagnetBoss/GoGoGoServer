using gogogoClientiOS.ViewControllers;
using MonoTouch.UIKit;

namespace gogogoClientiOS.Views
{
	public sealed class EventDetailsView : UIView
	{
		private readonly UIImageView _imageView;
		private readonly UILabel _nameLabel;
		private readonly UILabel _locationDescriptionLabel;
		private readonly UILabel _dateLabel;
		private readonly UILabel _timeLabel;
		private readonly UIButton _goButton;
		private readonly UILabel _participantsCountLabel;
		private readonly UILabel _participantsCountWordLabel;
		private readonly UILabel _descriptionlabel;
		private readonly EventDetailsViewController _controller;

		public EventDetailsView (EventDetailsViewController controller)
		{
			AppDelegate.Shared.Messenger.Subscribe (this);
			_controller = controller;
		
			BackgroundColor = UIColor.White;
			_imageView = new UIImageView ();
			_nameLabel = new UILabel {Font = UIFont.FromName("HelveticaNeue-Medium", 15f)};
		    _locationDescriptionLabel = new UILabel {Font = UIFont.FromName("HelveticaNeue-Light", 12f)};
		    _dateLabel = new UILabel
			{
			    Font = UIFont.FromName("HelveticaNeue-Medium", 15f),
			    TextAlignment = UITextAlignment.Center
			};
		    _timeLabel = new UILabel
		    {
		        Font = UIFont.FromName("HelveticaNeue-Light", 13f),
		        TextAlignment = UITextAlignment.Center
		    };
		    _goButton = UIButton.FromType (UIButtonType.RoundedRect);
			_goButton.SetTitle ("Я пойду!", UIControlState.Normal);
			_goButton.BackgroundColor = UIColor.FromRGB (240, 240, 240);
			_participantsCountLabel = new UILabel
			{
			    Font = UIFont.FromName("HelveticaNeue-Bold", 25f),
			    TextColor = UIColor.Red,
			    TextAlignment = UITextAlignment.Center
			};
		    _participantsCountWordLabel = new UILabel
		    {
		        Font = UIFont.FromName("HelveticaNeue-Light", 13f),
		        TextAlignment = UITextAlignment.Center
		    };
		    _descriptionlabel = new UILabel
		    {
		        Font = UIFont.FromName("HelveticaNeue-Light", 14f),
		        LineBreakMode = UILineBreakMode.WordWrap,
		        Lines = 0
		    };

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
				_imageView.Image = value;
			}
		}

		public string Name {
			get {
				return _nameLabel.Text;
			}
			set {
				_nameLabel.Text = value;
			}
		}

		public string LocationDescription {
			get {
				return _locationDescriptionLabel.Text;
			}
			set {
				_locationDescriptionLabel.Text = value;
			}
		}

		public string Date {
			get {
				return _dateLabel.Text;
			}
			set {
				_dateLabel.Text = value;
			}
		}

		public string Time {
			get {
				return _timeLabel.Text;
			}
			set {
				_timeLabel.Text = value;
			}
		}

		public string ParticipantsCount {
			get {
				return _participantsCountLabel.Text;
			}
			set {
				_participantsCountLabel.Text = value;
			}
		}

		public string ParticipantCountWord {
			get {
				return _participantsCountWordLabel.Text;
			}
			set {
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

