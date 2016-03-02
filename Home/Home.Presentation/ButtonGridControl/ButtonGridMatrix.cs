using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Home.Presentation.ViewModels;

namespace Home.Presentation.ButtonGridControl
{

    public class ButtonGridMatrix
    {
        public class Dimension
        {
            public int Width { get; }
            public int Height { get; }

            public Dimension(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public static Dimension Empty { get; } = new Dimension(0,0);
            public override bool Equals(object obj)
            {
                var dim = obj as Dimension;
                return dim != null && dim.Width == Width && dim.Height == Height;
            }

            public override string ToString()
            {
                return $"[w/h {Width}/{Height}}}";
            }
        }

        private const int MaxItems = 1000;


        public ButtonGridItem this[int row, int column] => Rows[row][column];

        public ButtonGridItem this[int index] => this[GetRow(index), GetIndex(index)];

        private List<Dimension> _dimensions; 

        public bool IsNeedRealocation
            =>Items.Count>0 && ( ActualDimension.Height * ActualDimension.Width < Items.Count || _dimensions[_dimension-1].Height * _dimensions[_dimension - 1].Width >= Items.Count);

        private int _dimension;
        private double _ratio;

        /// <summary>
        /// Width/Height
        /// </summary>
        public double Ratio
        {
            get { return _ratio;}
            set
            {
                if (_ratio == value) return;
                _ratio = value;
                _dimensions.Clear();
                CalculateDimensions();
                _dimension = 0;
                Realocate();
            }
        }

        public Dimension ActualDimension => _dimensions[_dimension];
        public ObservableCollection<ButtonGridItem> Items { get; }
        public ObservableCollection<ButtonGridRow> Rows { get; }


        public ButtonGridMatrix(double ratio)
            : this(ratio,null)
        {
          
        }

        public ButtonGridMatrix(double ratio, IEnumerable<ButtonGridItem> items)
        {
            _ratio = ratio;
            _dimensions = new List<Dimension>(40);
            _dimension = 0;
            CalculateDimensions();
            Items = items == null ?
                new ObservableCollection<ButtonGridItem>() :
                new ObservableCollection<ButtonGridItem>(items);
            Rows = new ObservableCollection<ButtonGridRow>();
            Items.CollectionChanged += ItemsCollectionChanged;
            if (IsNeedRealocation)
                Realocate();
        }

        private void CalculateDimensions()
        {
            var last = Dimension.Empty;
            _dimensions.Add(last);
            for (var i=0.1; last.Width*last.Height<MaxItems; i+=0.1)
            {
                var width = _ratio*i;
                var iwidth = Convert.ToInt32(Math.Round(width));
                var iheight = Convert.ToInt32(Math.Round(i));
                if (last.Width * last.Height < iwidth*iheight)
                {
                    last = new Dimension(iwidth, iheight);
                    _dimensions.Add(last);
                }
            }
        }
        

        private void Realocate()
        {
            Rows.Clear();
            _dimension=_dimensions.FindIndex((x) => x.Height*x.Width >= Items.Count);
            for (int i = 0; i < ActualDimension.Height && i * ActualDimension.Height < Items.Count; i++)
            {
                ButtonGridRow row = new ButtonGridRow();
                for (int j = 0; j < ActualDimension.Width && i * ActualDimension.Width + j < Items.Count; j++)
                {
                    row.Items.Add(Items[i * ActualDimension.Width + j]);
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
            var lastIndex = (Rows.Count - 1) * ActualDimension.Height + Rows.Last().Items.Count - 1;
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
            if (last.Items.Count == ActualDimension.Width)
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



        private int GetRow(int index) => index / ActualDimension.Width;
        private int GetIndex(int index) => index % ActualDimension.Width;
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
            var lastIndex = (Rows.Count - 1) * ActualDimension.Height + Rows.Last().Items.Count - 1;
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
            _dimension = 0;
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

}
