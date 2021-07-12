namespace db.Collections
{
    public abstract class ATree<ANode>
    {
        protected ATree(){}
        protected ATree(ANode root){
            Root = root;
        }
        public ANode Root{get;protected set;}
    }
}
