using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Home.Core;
using Home.Presentation.ViewModels;
using Microsoft.Expression.Interactivity.Core;

namespace Home.Presentation.ButtonGridControl
{

    public class ButtonGridItem : BaseViewModel
    {

        public string Header
        {
            get { return Model.Name; }
            set
            {
                if (Model.Name.Equals(value)) return;
                Model.Name = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected) return;
                _isSelected = value;
                if(_isSelected) Selected?.Execute(this);
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private BitmapImage _image;

        public BitmapImage Image => _image ?? (_image = new BitmapImage(new Uri(Model.ImageName)));

        private bool _over;

        private static readonly Brush SelectedBrush = Brushes.LightBlue;
        private static readonly Brush HighlightedBrush = Brushes.PaleTurquoise;
        private static readonly Brush HighlightedAndSelectedBrush = Brushes.PaleGreen;
        private static readonly Brush NormalnBrush = Brushes.Transparent;

        public IButtonGridItemModel Model { get; }
        public double NumericTag { get; set; }
        public Dictionary<string, object> Tag { get; }

        public ICommand MouseDoubleClick { get; set; }
        public ICommand MouseRightButtonUp { get; set; }
        public ICommand MouseLeftButtonUp { get; }
        public ICommand Selected { get; set; }
        public ICommand MouseEnter { get; }
        public ICommand MouseLeave { get; }

        private Brush _brush;
        public Brush BackgroundColor
        {
            get { return _brush; }
            set
            {
                if (_brush.Equals(value)) return;
                _brush = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }
        public ButtonGridItem(IButtonGridItemModel model)
        {
            _brush = NormalnBrush;
            MouseEnter = new ActionCommand((x) =>
            {
                _over = true;
                SetBackground();
            });
            MouseLeave = new ActionCommand((x) =>
            {
                _over = false;
                SetBackground();
            });
            MouseLeftButtonUp = new ActionCommand((x) =>
            {
                IsSelected = !IsSelected;
                SetBackground();
            });


            Model = model;
            Tag = new Dictionary<string, object>();

        }

        private void SetBackground()
        {
            if (IsSelected)
            {
                if (_over)
                {
                    BackgroundColor = HighlightedAndSelectedBrush;
                    return;
                }
                BackgroundColor = SelectedBrush;
                return;
            }
            if (_over)
            {
                BackgroundColor = HighlightedBrush;
                return;
            }
            BackgroundColor = NormalnBrush;
        }

    }
}
