using System;

namespace db.Collections
{
    [Serializable]
    public class Node : ANode
    {
        public Node(object id):base(id){}

        public override ANode FindNode(object toFind, ANode node)
        {
            if (node != null)
                return node;
            if (IsSameId(toFind))
                return this;
            foreach (var child in Children)
            {
                var found = child.FindNode(toFind, null);
                if (found != null)
                    return found;
            }
            return null;
        }

        public override bool IsSameId(object id)
        {
            if (Id == null || id == null)
                return false;
            if (ReferenceEquals(Id, id))
                return true;
            return string.Compare(Id.ToString(), id.ToString(), StringComparison.Ordinal) == 0;
        }
    }
}
