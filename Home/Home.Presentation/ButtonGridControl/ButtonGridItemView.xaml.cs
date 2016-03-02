using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Home.Presentation.ButtonGridControl
{
    /// <summary>
    /// Interaction logic for ButtonGridItemView.xaml
    /// </summary>
    public partial class ButtonGridItemView : UserControl
    {
        public ButtonGridItemView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IsHighlightedProperty = DependencyProperty.Register(
            "IsHighlighted", typeof (bool), typeof (ButtonGridItemView), new PropertyMetadata(default(bool)));

        public bool IsHighlighted
        {
            get { return (bool) GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof (bool), typeof (ButtonGridItemView), new PropertyMetadata(default(bool)));

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }

    
}
