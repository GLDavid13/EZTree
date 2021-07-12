using System.Collections.Generic;
using System.Linq;

namespace db.Collections
{
    public abstract class ANode
    {
        protected ANode() => Children = new LinkedList<ANode>();
        protected ANode(object id) : this() => Id = id;
        public object Id{get;set;}
        public ICollection<ANode>Children{get; protected set;}
        public ANode Parent{get; protected set;}
        public void AddChild(ANode node) 
        {
            Children.Add(node);
            node.Parent = this;
        }
        public abstract ANode FindNode(object toFind, ANode node);
        /// <summary>
        /// Find all parents of current ANode.
        /// </summary>
        /// <return>IEnumerable<ANode> as all list of parents from origin to current ANode</return>
        public IEnumerable<ANode>GetNodePath(){
            ICollection<ANode>parents = new List<ANode>();
            ANode current = this;
            while(current.Parent!=null){
                parents.Add(current.Parent);
                current = current.Parent;
            }
            return parents.Reverse();
        }
    }
}
