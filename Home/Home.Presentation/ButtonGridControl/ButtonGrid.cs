using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Home.Core;
using Home.Presentation.ViewModels;
using Microsoft.Expression.Interactivity.Core;

namespace Home.Presentation.ButtonGridControl
{


    public class ButtonGridMatrix
    {

        public ButtonGridItem this[int row, int column] => Rows[row][column];

        public ButtonGridItem this[int index] => this[GetRow(index), GetIndex(index)];

        public bool IsNeedRealocation
            => _dim * _dim < Items.Count || (_dim - 1) * (_dim - 1) >= Items.Count;

        private int _dim;

        public int Dimension => _dim;
        public ObservableCollection<ButtonGridItem> Items { get; }
        public ObservableCollection<ButtonGridRow> Rows { get; }


        public ButtonGridMatrix()
            : this(null)
        {
            Items = new ObservableCollection<ButtonGridItem>();
            Rows = new ObservableCollection<ButtonGridRow>();
            Items.CollectionChanged += ItemsCollectionChanged;
            _dim = 0;
        }

        public ButtonGridMatrix(IEnumerable<ButtonGridItem> items)
        {
            _dim = 0;
            Items = items == null ?
                new ObservableCollection<ButtonGridItem>() :
                new ObservableCollection<ButtonGridItem>(items);
            Rows = new ObservableCollection<ButtonGridRow>();
            Items.CollectionChanged += ItemsCollectionChanged;
            if(IsNeedRealocation)
                Realocate();
        }

        private void Realocate()
        {
            Rows.Clear();
            _dim = Convert.ToInt32(Math.Ceiling(Math.Sqrt(Items.Count)));
            for (int i = 0; i < _dim && i * _dim < Items.Count; i++)
            {
                ButtonGridRow row = new ButtonGridRow();
                for (int j = 0; j < _dim && i * _dim + j < Items.Count; j++)
                {
                    row.Items.Add(Items[i * _dim + j]);
                }
                Rows.Add(row);
            }
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    Reset();
                    break;
                case NotifyCollectionChangedAction.Add:
                    if (IsNeedRealocation) Realocate();
                    else Add(e.NewStartingIndex, e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (IsNeedRealocation) Realocate();
                    else Remove(e.OldStartingIndex, e.OldItems.Count);
                    break;
                case NotifyCollectionChangedAction.Move:
                    Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Replace(e.OldStartingIndex, e.NewItems);
                    break;
            }
        }

        private void Move(int oldStartingIndex, int newStartingIndex)
        {
            var item = this[oldStartingIndex];
            Remove(oldStartingIndex, 1);
            Add(newStartingIndex, new[] { item });
        }

        private void Replace(int oldStartingIndex, IList newItems)
        {
            for (int i = 0; i < newItems.Count; i++)
            {
                Replace(oldStartingIndex + i, (ButtonGridItem)newItems[i]);
            }
        }

        private void RemoveLast()
        {
            var last = Rows.Last();
            last.Items.RemoveAt(last.Items.Count - 1);
            if (last.Items.Count == 0)
                Rows.RemoveAt(Rows.Count - 1);
        }

        // 0 [1] [2] 3 4 5 6
        private void Remove(int oldStartingIndex, int count)
        {
            var lastIndex = (Rows.Count - 1) * _dim + Rows.Last().Items.Count - 1;
            for (int i = 0; i + oldStartingIndex <= lastIndex - count; i++)
            {
                Replace(oldStartingIndex + i, this[oldStartingIndex + i + count]);
            }
            while (count > 0)
            {
                RemoveLast();
                count--;
            }
        }

        //_dim 3
        // 0  0  0
        // 0  0  0
        // 0  0  0


        //   0   1    [2]  3  2  3  4 5 6 7      //2

        private void Append(ButtonGridItem item)
        {
            var last = Rows.Last();
            if (last.Items.Count == _dim)
            {
                var row = new ButtonGridRow();
                row.Items.Add(item);
                Rows.Add(row);
            }
            else
            {
                Rows[Rows.Count - 1].Items.Add(item);
            }
        }



        private int GetRow(int index) => index / _dim;
        private int GetIndex(int index) => index % _dim;
        private void Add(int newStarginIndex, IList newItems)
        {
            RawMove(newStarginIndex, newItems.Count);
            var count = newItems.Count;
            for (var i = 0; i < count; i++)
            {
                Replace(newStarginIndex + i, (ButtonGridItem)newItems[i]);
            }
        }

        private void RawMove(int newStarginIndex, int count)
        {
            var lastIndex = (Rows.Count - 1) * _dim + Rows.Last().Items.Count - 1;
            var lastItem = this[lastIndex];
            int temp = count;
            while (temp > 0)
            {
                Append(lastItem);
                temp--;
            }
            var newlastIndex = lastIndex + count;
            while (lastIndex >= newStarginIndex)
            {
                Replace(newlastIndex, this[lastIndex]);
                newlastIndex--;
                lastIndex--;
            }


        }

        private void Replace(int index, ButtonGridItem item) =>
            Rows[GetRow(index)].Items[GetIndex(index)] = item;


        private void Reset()
        {
            Rows.Clear();
            _dim = 0;
        }

        public class ButtonGridRow : BaseViewModel
        {
            public ObservableCollection<ButtonGridItem> Items { get; }

            public ButtonGridItem this[int column] => Items[column];
            public ButtonGridRow()
            {
                Items = new ObservableCollection<ButtonGridItem>();
            }

        }
    }

    public abstract class ButtonGrid : BaseViewModel
    {
        private ButtonGridMatrix _matrix;

        public ObservableCollection<ButtonGridItem> Items
        {
            get { return _matrix.Items; }
            set
            {
                _matrix = new ButtonGridMatrix(value);
                OnPropertyChanged(nameof(Items));
            }
        }
        public ObservableCollection<ButtonGridMatrix.ButtonGridRow> Rows => _matrix.Rows;


        protected ButtonGrid()
        {
            //var cmd = new ActionCommand((x) => MessageBox.Show(x?.ToString() ?? "Dupa"));
            //BitmapImage bmp = new BitmapImage(new Uri(@"C:\Users\Sadako\Desktop\temp.jpg"));
            //var list = new List<ButtonGridItem>()
            //{
            //    new ButtonGridItem() {Header = "0 0", Image = bmp, MouseDoubleClick = cmd},
            //    new ButtonGridItem() {Header = "0 1", Image = bmp, MouseDoubleClick = cmd},
            //    new ButtonGridItem() {Header = "1 0", Image = bmp, MouseDoubleClick = cmd},
            //    new ButtonGridItem() {Header = "1 1", Image = bmp, MouseDoubleClick = cmd}
            //};
            //_matrix = new ButtonGridMatrix(list);
            _matrix = new ButtonGridMatrix();
        }
        
    }



    public class ButtonGridItem : BaseViewModel
    {

        public string Header
        {
            //get { return Model.Name; }
            get { return _over.ToString(); }
            set
            {
                if (Model.Name.Equals(value)) return;
                Model.Name = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (value == _selected) return;
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        
        private BitmapImage _image;

        public BitmapImage Image => _image ?? (_image = new BitmapImage(new Uri(Model.ImageName)));

        private bool _over;

        private static readonly Brush SelectedBrush = Brushes.Blue;
        private static readonly Brush HighlightedBrush = Brushes.PaleTurquoise;
        private static readonly Brush HighlightedAndSelectedBrush = Brushes.PaleGreen;
        private static readonly Brush NormalnBrush = Brushes.Transparent;

        public IButtonGridItemModel Model { get; }
        public double NumericTag { get; set; }
        public Dictionary<string,object> Tag { get; }

        public ICommand MouseDoubleClick { get; set; }
        public ICommand MouseRightButtonUp { get; set; }
        public ICommand MouseLeftButtonUp { get; }
        public ICommand MouseEnter { get; }
        public ICommand MouseLeave { get; }

        private Brush _brush;
        public Brush BackgroundColor
        {
            get { return _brush;  }
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
                Selected = !Selected;
                SetBackground();
            });


            Model = model;
            Tag = new Dictionary<string, object>();
            
        }

        private void SetBackground()
        {
            if (Selected)
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
