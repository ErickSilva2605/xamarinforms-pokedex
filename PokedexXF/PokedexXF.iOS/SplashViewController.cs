using Airbnb.Lottie;
using CoreGraphics;
using UIKit;

namespace PokedexXF.iOS
{
    public partial class SplashViewController : UIViewController
    {
        public SplashViewController() : base("SplashViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var animationView = LOTAnimationView.AnimationNamed("splash");
            var boundSize = UIScreen.MainScreen.Bounds.Size;
            animationView.Frame = new CGRect(x: 0, y: 0, width: boundSize.Width, height: boundSize.Height);
            animationView.ContentMode = UIViewContentMode.ScaleAspectFit;

            this.View.AddSubview(animationView);
            animationView.PlayWithCompletion((animationFinished) =>
            {
                UIApplication.SharedApplication.Delegate.FinishedLaunching(UIApplication.SharedApplication,
                                                                           new Foundation.NSDictionary());
            });
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

