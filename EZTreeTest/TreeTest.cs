using System;
using db.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EZTreeTest
{
    [TestClass]
    public class TreeTest
    {
        [TestMethod]
        public void TestCreateTree(){
            try{
                var tree = new Tree();
                Assert.IsNotNull(tree);
                Assert.IsNotNull(tree.Root);
                Assert.AreEqual(0, tree.Root.Id);
                tree = new Tree(new Node("A"));
                Assert.IsNotNull(tree);
                Assert.IsNotNull(tree.Root);
                Assert.AreEqual("A", tree.Root.Id);
            }catch(Exception ex){
                Assert.Fail(ex.Message);
            }
        }
    }
}
