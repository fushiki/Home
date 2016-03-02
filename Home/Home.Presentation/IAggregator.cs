using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Home.Presentation.Tree;

namespace Home.Presentation
{
    public interface IAggregator
    {
        void Aggregate(TreeItem item,bool deep);
    }

    public interface IAggregation
    {
        string Value { get; }

    }
}
