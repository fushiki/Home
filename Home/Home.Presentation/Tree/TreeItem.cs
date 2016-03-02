using System;
using System.Windows;
using System.Windows.Input;
using Home.Presentation.Aggregators;
using Home.Presentation.ViewModels;

namespace Home.Presentation.Tree
{
    public abstract class TreeItem:BaseViewModel
    {
        public AggregationInfo AggregationInfo { get; }
        public string AggregationValue => $"[{AggregationInfo.Aggregation}]";
        public TreeGroup Parent { get; }
        public abstract string Name { get; set; }
        public abstract Visibility AddVisibility { get; }
        public bool IsExpanded { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public TreeViewModel Owner { get; }
        public ICommand CommandAddProduct => Owner.CommandAddProduct;
        public ICommand CommandAddCategory => Owner.CommandAddCategory;
        public ICommand CommandDelete => Owner.CommandDelete;
        public ICommand CommandRename => Owner.CommandRename;
        public Visibility ContextMenuVisibility => Owner.ContextMenuVisibility;
        protected TreeItem(TreeViewModel owner, TreeGroup parent)
        {
            Owner = owner;
            Parent = parent;
            AggregationInfo = new AggregationInfo();
            AggregationInfo.AggregationChanged += AggregationChanged;
        }

        protected virtual void AggregationChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(AggregationValue));
        }
    }

    
}
