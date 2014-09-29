using System.Drawing;
using MonoTouch.UIKit;

namespace gogogoClientiOS.Views
{
	public sealed class LoginView : UIView
	{
		private readonly UIImageView _imageView;
		private readonly UIButton _loginButton;

		public LoginView ()
		{
			Add (
				_imageView = new UIImageView {
					Image = UIImage.FromBundle ("login.png"),
				}
			);

			_loginButton = UIButton.FromType (UIButtonType.RoundedRect);
			Add (_loginButton);
			_loginButton.SetTitle ("Войти", UIControlState.Normal);
			_loginButton.BackgroundColor = UIColor.FromRGB (240, 240, 240);

			_loginButton.TouchUpInside += (sender, e) => AppDelegate.Shared.ShowEventsList();
		}


		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			_imageView.Frame = new RectangleF (0, 0, Frame.Width, Frame.Height);
			_loginButton.Frame = new RectangleF (20, 100, Frame.Width - 40, 30);
		}
	}
}

