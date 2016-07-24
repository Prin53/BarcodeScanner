using System.Collections.Specialized;
using System.Windows.Input;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Platform.IoC;
using UIKit;

namespace BarcodeScanner.iOS
{
    [Preserve(AllMembers = true)]
    public class Linker
    {
        public void Include(UIButton button)
        {
            button.TintColor = UIColor.Red;
            button.TouchUpInside += (s, e) => button.SetTitle(button.Title(UIControlState.Normal), UIControlState.Normal);
        }

        public void Include(UIBarButtonItem barButtonItem)
        {
            barButtonItem.Clicked += (s, e) => barButtonItem.Title = barButtonItem.Title + "";
        }

        public void Include(UITextField textField)
        {
            textField.Text = textField.Text + "";
            textField.EditingChanged += (sender, args) =>
            {
                textField.Text = "";
            };
        }

        public void Include(UITextView textView)
        {
            textView.Text = textView.Text + "";
            textView.Changed += (sender, args) =>
            {
                textView.Text = "";
            };
        }

        public void Include(UILabel label)
        {
            label.Text = label.Text + "";
            label.AttributedText = new NSAttributedString(label.AttributedText + "");
        }

        public void Include(UIImageView imageView)
        {
            imageView.Image = new UIImage(imageView.Image.CGImage);
        }

        public void Include(UIDatePicker date)
        {
            date.Date = date.Date.AddSeconds(1);
            date.ValueChanged += (sender, args) =>
            {
                date.Date = NSDate.DistantFuture;
            };
        }

        public void Include(UISlider slider)
        {
            slider.Value = slider.Value + 1;
            slider.ValueChanged += (sender, args) =>
            {
                slider.Value = 1;
            };
        }

        public void Include(UIProgressView progressView)
        {
            progressView.Progress = progressView.Progress + 1;
        }

        public void Include(UISwitch @switch)
        {
            @switch.On = !@switch.On;
            @switch.ValueChanged += (sender, args) =>
            {
                @switch.On = false;
            };
        }

        public void Include(MvxViewController viewController)
        {
            viewController.Title = viewController.Title + "";
        }

        public void Include(UIStepper stepper)
        {
            stepper.Value = stepper.Value + 1;
            stepper.ValueChanged += (sender, args) =>
            {
                stepper.Value = 0;
            };
        }

        public void Include(UIPageControl pageControl)
        {
            pageControl.Pages = pageControl.Pages + 1;
            pageControl.ValueChanged += (sender, args) =>
            {
                pageControl.Pages = 0;
            };
        }

        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s, e) =>
            {
                string.Format("{0}{1}{2}{3}{4}", e.Action, e.NewItems, e.NewStartingIndex, e.OldItems, e.OldStartingIndex);
            };
        }

        public void Include(ICommand command)
        {
            command.CanExecuteChanged += (s, e) =>
            {
                if (command.CanExecute(null))
                    command.Execute(null);
            };
        }

        public void Include(MvxPropertyInjector injector)
        {
            injector = new MvxPropertyInjector();
        }

        public void Include(System.ComponentModel.INotifyPropertyChanged changed)
        {
            changed.PropertyChanged += (sender, e) =>
            {
                var test = e.PropertyName;
            };
        }

        public void Include(UISegmentedControl segmentControl)
        {
            segmentControl.TintColor = UIColor.Red;
        }
    }
}

