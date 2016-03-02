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


    public abstract class ButtonGrid : BaseViewModel
    {
        private ButtonGridMatrix _matrix;

        public ObservableCollection<ButtonGridItem> Items
        {
            get { return _matrix.Items; }
            set
            {
                _matrix = new ButtonGridMatrix(_matrix.Ratio,value);
                OnPropertyChanged(nameof(Items));
            }
        }
        public ObservableCollection<ButtonGridMatrix.ButtonGridRow> Rows => _matrix.Rows;


        protected ButtonGrid(double ratio)
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
            _matrix = new ButtonGridMatrix(ratio);
        }
        
    }




   
}
