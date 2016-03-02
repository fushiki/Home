using System.Windows;
using Home.Budget;

namespace Home.Presentation.Tree
{
    public class TreeLeaf:TreeItem
    {
        public Product Product { get; }

        public override string Name
        {
            get { return Product.Name; }
            set { Product.Name = value;OnPropertyChanged(nameof(Name)); }
        } 
        public override Visibility AddVisibility => Visibility.Collapsed;

        public TreeLeaf(TreeViewModel owner, TreeGroup parent, Product product)
            : base(owner, parent)
        {
            Product = product;
        }
    }
}
