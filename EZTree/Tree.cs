using System;
using System.Collections.Generic;

namespace db.Collections{
    [Serializable]
    public class Tree : ATree<ANode>{
        public Tree() : base(){
            Root = new Node(0);
        }

        public Tree(ANode root) : base(root){}

        public string SelectedNode { get; set; }
    }
}
