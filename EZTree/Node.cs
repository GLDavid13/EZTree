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
            if(toFind.Equals(Id))
                return this;
            if(Children.Any()){
                foreach(var child in Children){
                    return child.FindNode(toFind, null);
                }
            }
            return null;
        }
    }
}
