using System;
using System.Collections.Generic;

namespace db.Collections
{
    [Serializable]
    public abstract class ATree<ANode>
    {
        protected ATree()
        {
            Nodes = new List<ANode>();
        }
        protected ATree(ANode root):this(){
            Root = root;
        }
        public ANode Root{get;protected set;}
        public ICollection<ANode>Nodes{get;}
        //public abstract IEnumerable<ANode>AllNodesList();
    }
}
