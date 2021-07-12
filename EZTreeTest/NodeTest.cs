using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace db.Collections.EZTreeTest
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void TestCreateNode()
        {
            try{
                var node = new Node(1);
                Assert.AreEqual(1, node.Id);
                Assert.IsNull(node.Parent);
                Assert.IsNotNull(node.Children);
                Assert.IsTrue(!node.Children.Any());
            }
            catch(Exception ex){
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAddChild()
        {
            try{
                var node = new Node(1);
                var node2 = new Node(2);
                node.AddChild(node2);
                var children = node.Children;
                Assert.AreEqual(1, children.Count);
                Assert.AreEqual(2, children.FirstOrDefault().Id);
                Assert.AreEqual(1, children.FirstOrDefault().Parent.Id);
            }
            catch(Exception ex){
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAddFamily()
        {
            try{
                var node = new Node(1);
                var node2 = new Node(2);
                var node2a = new Node(4);
                node2.AddChild(node2a);
                var node3 = new Node(3);
                node.AddChild(node2);
                node.AddChild(node3);
                var children = node.Children;
                Assert.AreEqual(2, children.Count);
                var child = children.FirstOrDefault();
                Assert.AreEqual(2, child.Id);
                Assert.AreEqual(1, child.Children.Count);
                Assert.AreEqual(4, child.Children.FirstOrDefault().Id);
                Assert.AreEqual(3, node.Children.LastOrDefault().Id);
            }
            catch(Exception ex){
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestFindNode()
        {
            try{
                var node = new Node(1);
                var node2 = new Node(2);
                var node2a = new Node(4);
                node2.AddChild(node2a);
                var node3 = new Node(3);
                node.AddChild(node2);
                node.AddChild(node3);
                var obvious = node.FindNode(1, null);
                Assert.IsNotNull(obvious);
                Assert.AreEqual(1, obvious.Id);
                var found = node.FindNode(4, null);
                Assert.IsNotNull(found);
                Assert.AreEqual(4, found.Id);
                Assert.AreEqual(2, found.Parent.Id);
                var notFound = node.FindNode(5, null);
                Assert.IsNull(notFound);
            }
            catch(Exception ex){
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGetNodePath()
        {
            try{
                var node = new Node(1);
                var node2 = new Node(2);
                var node2a = new Node(4);
                node2.AddChild(node2a);
                var node3 = new Node(3);
                node.AddChild(node2);
                node.AddChild(node3);
                var found = node.FindNode(4, null);
                var path = found.GetNodePath();
                Assert.IsNotNull(path);
                var matchIds = new int[]{1, 2};
                Assert.AreEqual(matchIds.Count(), path.Count());
                for(int i=0; i<matchIds.Count(); i++){
                    Assert.AreEqual(matchIds[i], path.ElementAt(i).Id);
                }
                path = node.GetNodePath();
                Assert.IsTrue(!path.Any());
            }
            catch(Exception ex){
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAddLittleChild()
        {
            try{
                var node = new Node(1);
                var node2 = new Node(2);
                var node2a = new Node(4);
                var node3 = new Node(3);
                node.AddChild(node2);
                node.AddChild(node3);
                var found = node.FindNode(2, null);
                found.AddChild(node2a);
                Assert.AreEqual(1, found.Children.Count);
                var nfound = node.FindNode(2, null);
                Assert.AreEqual(1, nfound.Children.Count);
            }
            catch(Exception ex){
                Assert.Fail(ex.Message);
            }
        }
    }
}
