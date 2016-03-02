using System;
using System.Windows.Controls;
using System.Windows.Input;
using Home.Presentation.Tree;

namespace Home.Presentation.ViewModels
{
    public class AddShoppingViewModel : BaseViewModel, IBudgetDynamicViewModel
    {
        private MainWindowViewModel _parent;

        public TreeViewModel TreeViewModel { get; }

        public ICommand CommandAdd { get; }
        public ICommand CommandRemove { get; }
        public ICommand CommandAddShopping { get; }
        public TreeViewModel AllProductTreeViewModel { get; }

        private double _price;
        private double _quantity;

        public double Quntity
        {
            get { return _quantity; }
            set
            {
                if (value - _quantity < 0.000001) return;
                _quantity = value;
                OnPropertyChanged(nameof(Quntity));
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value - _price < 0.000001) return;
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }



























        /*
        public AddShoppingViewModel(MainWindowViewModel parent)
        {
            AllProductTreeViewModel = new TreeViewModel(
               AggregateFabric.CountAggregator(AggregateFabric.Algorithm.Sum),
               BudgetTopic.Get().Database.AllCategory,
               true,
               true);
            TreeViewModel = new TreeViewModel(
                AggregateFabric.QuantityAggregator(AggregateFabric.Algorithm.Mean), null, false, false);
            _parent = parent;
            CommandAdd = new LambdaCommand(
                x =>
                {
                    var item = TreeViewModel.Insert(AllProductTreeViewModel.Selected, true);
                    var group = item as TreeGroup;
                    if (@group != null) return;
                    item.AggregationInfo.Args[nameof(Purchase.Price)] = _price;
                    item.AggregationInfo.Args[nameof(Purchase.Quantity)] = _quantity;
                    TreeViewModel.Aggregator.Aggregate(item);
                    TreeViewModel.Aggregator.AggregateUp(item);
                    OnPropertyChanged(nameof(TreeViewModel));//TODO
                }
                );
            CommandRemove = new LambdaCommand(
                x =>
                {
                    var actual = TreeViewModel.Selected.Parent;
                    TreeViewModel.Selected.Parent.Items.Remove(TreeViewModel.Selected);
                    while (actual != TreeViewModel.Root && actual.Items.Count == 0)
                    {
                        var tmp = actual.Parent;
                        actual.Parent.Items.Remove(actual);
                        actual = tmp;
                    }
                }

                );
            CommandAddShopping = new LambdaCommand(AddShopping);
        }

        public void AddShopping(object parameter)
        {
            Shopping shopping = new Shopping(DateTime.Now, new Shop("ASD", "ASD", Shop.ShopType.GroceryStore));
            var item = TreeViewModel.Root;
            foreach (var treeItem in item.Items)
            {
                AddItem(shopping, treeItem);
            }
            BudgetTopic.Get().Database.ShoppingHistory.Add(shopping.Date,shopping);
        }

        private void AddItem(Shopping shopping, TreeItem item)
        {
            var group = item as TreeGroup;
            if (group != null)
                foreach (var treeItem in group.Items)
                {
                    AddItem(shopping, treeItem);
                }
            var leaf = item as TreeLeaf;
            if (leaf != null)
            {
                shopping.ShoppingList.Add(
                    new Purchase()
                    {
                        Product = leaf.Product,
                        Price = leaf.AggregationInfo.Args[nameof(Price)],
                        Quantity = leaf.AggregationInfo.Args[nameof(Quantity)]
                    });
            }
        }

    */
    }
}
