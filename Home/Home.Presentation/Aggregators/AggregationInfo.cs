using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Home.Budget;
using Home.Presentation.Tree;

namespace Home.Presentation.Aggregators
{
    public class AggregationInfo
    {
        public Dictionary<string, double> Args { get; } = new Dictionary<string, double>();

        private double _aggregation;
        public double Aggregation
        {
            get { return _aggregation;}
            set { _aggregation = value;  OnAggregationChanged();}
        }

        public event EventHandler AggregationChanged;

        private void OnAggregationChanged()
        {
            AggregationChanged?.Invoke(this,EventArgs.Empty);
        }
        // zliczanie ile elementow
        // zliczanie ile wartosci, ktore sa wpisane
        // srednia cena
        // suma ceny


    }

    public abstract class LeafInitializer
    {

        public abstract bool LeafInitialize { get; }
        public abstract double InitializeValue(TreeLeaf item);
    }

    public abstract class Aggregator
    {
        protected LeafInitializer _initializer;

        protected Aggregator(LeafInitializer initializer)
        {
            _initializer = initializer;
        }

        public void AggregateUp(TreeItem item)
        {
            var group = item.Parent;
            while (group != null)
            {
                Aggregate(group);
                group = group.Parent;
            }
        }

        public void AggregateDown(TreeItem item)
        {
            var group = item as TreeGroup;
            if (group == null) return;
            foreach (var treeItem in group.Items)
            {
                AggregateDown(treeItem);
                Aggregate(treeItem);
            }
        }

        public void Aggregate(TreeItem item)
        {
            var group = item as TreeGroup;
            if (group != null)
            {
                Start();
                foreach (var treeItem in group.Items)
                {
                    Action(treeItem);
                }
                FinishAggregation(group);
            }
            else
            {
                if (_initializer.LeafInitialize)
                    item.AggregationInfo.Aggregation = _initializer.InitializeValue((TreeLeaf) item);
            }
        }

        protected abstract void Start();
        protected abstract void Action(TreeItem item);
        protected abstract void FinishAggregation(TreeGroup group);
    }

    class CountAggregator : Aggregator
    {

        private double _sum;

        public CountAggregator(LeafInitializer initializer)
            : base(initializer)
        {

        }

        protected override void Start()
        {
            _sum = 0.0;
        }

        protected override void Action(TreeItem item)
        {
            _sum += item.AggregationInfo.Aggregation;
        }

        protected override void FinishAggregation(TreeGroup group)
        {
            group.AggregationInfo.Aggregation = _sum;
        }
    }

    class MeanInitializer : LeafInitializer
    {
        public override bool LeafInitialize => true;
        public override double InitializeValue(TreeLeaf item)
        {
            var value = _innerInitializer.InitializeValue(item);
            item.AggregationInfo.Args[nameof(MeanAggregator.Sum)] = value;
            item.AggregationInfo.Args[nameof(MeanAggregator.Count)] = 1;
            return value;
        }

        private readonly LeafInitializer _innerInitializer;

        public MeanInitializer(LeafInitializer initializer)
        {
            _innerInitializer = initializer;
        }
    }
    class MeanAggregator : Aggregator
    {
        private double _sum;
        private int _count;
        public double Sum => _sum;
        public int Count => _count;

        public MeanAggregator(LeafInitializer initializer) : base(new MeanInitializer(initializer))
        {
        }

        protected override void Start()
        {
            _sum = 0.0;
            _count = 0;
        }

        protected override void Action(TreeItem item)
        {
            _sum += item.AggregationInfo.Aggregation;
            _count++;
        }

        protected override void FinishAggregation(TreeGroup @group)
        {
            if (_count == 0)
                group.AggregationInfo.Aggregation = 0.0;
            else
                group.AggregationInfo.Aggregation = _sum/_count;
        }
    }

    class SimplyCountInitializer : LeafInitializer
    {
        public override bool LeafInitialize => true;

        public override double InitializeValue(TreeLeaf item)
        {
            return 1.0;
        }
    }

    class QuantityInitializer : LeafInitializer
    {
        public override bool LeafInitialize => true;

        public override double InitializeValue(TreeLeaf item)
        {
            return item.AggregationInfo.Args[nameof(Purchase.Quantity)];
        }
    }

    class PriceInitializer : LeafInitializer
    {
        public override bool LeafInitialize => true;

        public override double InitializeValue(TreeLeaf item)
        {
            return item.AggregationInfo.Args[nameof(Purchase.Price)];
        }
    }

    public static class AggregateFabric
    {
        public enum Algorithm
        {
            Mean,
            Sum
        }

        public static Aggregator PriceAggregator(Algorithm algorithm)
        {
            return CreateAggregator(new PriceInitializer(), algorithm);
        }

        public static Aggregator QuantityAggregator(Algorithm algorithm)
        {
            return CreateAggregator(new QuantityInitializer(), algorithm);
        }

        public static Aggregator CountAggregator(Algorithm algorithm)
        {
            return CreateAggregator(new SimplyCountInitializer(), algorithm);
        }

        public static Aggregator CreateAggregator(LeafInitializer initializer, Algorithm algorithm)
        {
            switch (algorithm)
            {
                case Algorithm.Mean:
                    return new MeanAggregator(initializer);
                case Algorithm.Sum:
                    return new CountAggregator(initializer);
                default:
                    throw new Exception();
            }
        }
    }
}
