using System;
using System.Linq;

namespace db.Collections
{
    public class Node : ANode
    {
        public Node(object id):base(id){}

        public override ANode FindNode(object toFind, ANode node)
        {
            if(node!=null)
                return node;
            if(IsSameId(toFind))
                return this;
            return !Children.Any() ? null : Children.Select(child => child.FindNode(toFind, null)).FirstOrDefault(found => found != null);
        }

        public override bool IsSameId(object id)
        {
            return string.Compare(Id.ToString(), id.ToString(), StringComparison.Ordinal) == 0;
        }
    }
}
