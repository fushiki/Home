using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Home.Budget;
using Home.Presentation.Aggregators;
using Home.Presentation.ViewModels;

namespace Home.Presentation.Tree
{
    public class TreeViewModel:BaseViewModel
    {
        public String Title { get; set; }

        private readonly TreeGroup _root;
        public ObservableCollection<TreeItem> Items => _root.Items;

        public Aggregator Aggregator { get; set; }

        public ICommand CommandSelectedItemChanged { get; }
        public ICommand CommandAddProduct { get; }
        public ICommand CommandAddCategory { get; }
        public ICommand CommandDelete { get; }
        public ICommand CommandRename { get; }

        public Visibility ContextMenuVisibility { get; }
        private TreeItem _selected;

        public TreeItem Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        public TreeGroup Root => _root;

        public TreeViewModel(Aggregator aggregator,ProductCategory root,bool initialAggregation,bool isContextMenuEnabled)
        {
            ContextMenuVisibility = isContextMenuEnabled? Visibility.Visible : Visibility.Collapsed;
            Aggregator = aggregator;
            _root = new TreeGroup(this,null,root,true,true);
            if(initialAggregation)
                Aggregator.AggregateDown(_root);
            CommandSelectedItemChanged = new LambdaCommand(OnSelectedChanged);
            CommandAddProduct = new LambdaCommand(AddProduct);
            CommandAddCategory = new LambdaCommand(AddCategory);
            CommandDelete = new LambdaCommand(Delete);
            CommandRename = new LambdaCommand(Rename);

        }

        private void Rename(object parameter)
        {
            var item = parameter as TreeItem;
            var renameViewModel = new RenameViewModel();
            if (renameViewModel.Window.ShowDialog() ?? false)
            {
                item.Name = renameViewModel.Name;
            }
        }

        public void Delete(object parameter)
        {
            var item = parameter as TreeItem;
            item?.Parent.RemoveChild(item);


        }
        private void OnSelectedChanged(object parameter)
        {
            _selected = parameter as TreeItem;
            
        }

        public void AddCategory(object parameter)
        {
           

            var addProductViewModel = new AddCategoryViewModel();
            var item = parameter as TreeGroup ?? _root;
            if (addProductViewModel.Window.ShowDialog() ?? false)
            {
                var categoryVM = new TreeGroup(this,item, addProductViewModel.Category, false, false);
                item.AddChild(categoryVM);
            }
            OnPropertyChanged(nameof(Items));
        }

        public void AddProduct(object parameter)
        {

            var addProductViewModel = new AddProductViewModel();
            var item = parameter as TreeGroup ?? _root;
            if (addProductViewModel.Window.ShowDialog() ?? false)
            {
                var productVM = new TreeLeaf(this,item, addProductViewModel.Product);
                item.AddChild(productVM);
            }
            OnPropertyChanged(nameof(Items));
        }
        public TreeItem Insert(TreeItem budgetTreeItemViewModel, bool search)
        {

            TreeItem newInsertedItem;
            if (search)
            {
                var ancestors = new List<TreeGroup>();
                var toInsert = _root;
                TrackGrandParent(budgetTreeItemViewModel, ancestors);
                var i = ancestors.Count - 2; //dont care about last (root)
                while (i >= 0)
                {

                    var item = toInsert.Items.FirstOrDefault((x) => x.Equals(ancestors[i]));
                    if (item == null) break;
                    toInsert = (TreeGroup) item;
                    i--;
                }

                while (i >= 0)
                {
                    var newItem = new TreeGroup(
                        this,
                        toInsert,
                        ancestors[i].ProductCategory,
                        false,
                        false
                        );
                    toInsert.Items.Add(newItem);
                    toInsert = newItem;
                    --i;
                }
                if (budgetTreeItemViewModel is TreeLeaf)
                    newInsertedItem = new TreeLeaf(this,toInsert, ((TreeLeaf) budgetTreeItemViewModel).Product);
                else
                    newInsertedItem = new TreeGroup(this,toInsert, ((TreeGroup) budgetTreeItemViewModel).ProductCategory, false,
                        false);
                toInsert.Items.Add(newInsertedItem);

            }
            else
                throw new NotImplementedException();
            OnPropertyChanged(nameof(Items));
            return newInsertedItem;
        }

        private void TrackGrandParent(TreeItem item, List<TreeGroup> track)
        {
            while (item.Parent != null)
            {
                track.Add(item.Parent);
                item = item.Parent;
            }
        }
      

        

        
    }

    
}
