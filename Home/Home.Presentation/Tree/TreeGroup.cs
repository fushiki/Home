using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Home.Budget;

namespace Home.Presentation.Tree
{
    public class TreeGroup:TreeItem
    {
        public ObservableCollection<TreeItem> Items { get; }

        public override string Name
        {
            get { return ProductCategory.Name; }
            set { ProductCategory.Name = value; OnPropertyChanged(nameof(Name)); }
        }
        public override Visibility AddVisibility => Visibility.Visible;
        public ProductCategory ProductCategory { get; }

        public TreeGroup(
            TreeViewModel owner,
            TreeGroup parent,
            ProductCategory category,
            bool loadSubCategories,
            bool loadProducts)
            :base(owner,parent)
        {
            ProductCategory = category;
            var items = new List<TreeItem>();
            if (category!=null)
            {
                if(loadSubCategories)
                    items.AddRange((from item in category.SubCategories select new TreeGroup(Owner,this, item,true,loadProducts))
                            .ToList<TreeItem>());
                if(loadProducts)
                    items.AddRange(from item in category.Products select new TreeLeaf(Owner,this, item));

            }
            Items = new ObservableCollection<TreeItem>(items);
        }

        public void AddChild(TreeLeaf productVm)
        {
            Items.Add(productVm);
            ProductCategory.Products.Add(productVm.Product);
        }
        public void AddChild(TreeGroup @group)
        {
            Items.Add(@group);
            ProductCategory.SubCategories.Add(@group.ProductCategory);
        }

        public override bool Equals(object obj)
        {
            var category = obj as TreeGroup;
            return category != null && category.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public void RemoveChild(TreeItem item)
        {
            Items.Remove(item);
            var group = item as TreeGroup;
            if (group != null)
                ProductCategory.SubCategories.Remove(group.ProductCategory);
            var leaf = item as TreeLeaf;
            if (leaf != null)
                ProductCategory.Products.Remove(leaf.Product);
        }
    }
}
