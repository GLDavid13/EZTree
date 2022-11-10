using db.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace db.EZTreeConsoleApp
{
    public class MyTree :  Tree
    {
        public MyTree(MyNode node) : base(node) { }

        private static IEnumerable<JToken> GetDescendantsByRegex(JContainer jsonTree, Regex regex)
        {
            return (from descendant in jsonTree.Descendants() let properties = descendant.Children<JProperty>() from property in properties where property.Name.Equals("id") && regex.IsMatch(property.Value.ToString()) select descendant).ToList();
        }

        private static IEnumerable<JToken> GetCompaniesByRegex(JContainer jsonTree)
        {
            var regex = new Regex("C\\d+", RegexOptions.Compiled);
            return (from descendant in jsonTree.Descendants() let properties = descendant.Children<JProperty>() from property in properties where property.Name.Equals("id") && regex.IsMatch(property.Value.ToString()) select descendant).ToList();
        }

        private static IEnumerable<JToken> GetSitesByRegex(JContainer jsonTree)
        {
            var regex = new Regex("SI\\d+", RegexOptions.Compiled);
            return (from descendant in jsonTree.Descendants() let properties = descendant.Children<JProperty>() from property in properties where property.Name.Equals("id") && regex.IsMatch(property.Value.ToString()) select descendant).ToList();
        }

        private static IEnumerable<JToken> GetLocationsByRegex(JContainer jsonTree)
        {
            var regex = new Regex("L\\d+", RegexOptions.Compiled);
            return (from descendant in jsonTree.Descendants() let properties = descendant.Children<JProperty>() from property in properties where property.Name.Equals("id") && regex.IsMatch(property.Value.ToString()) select descendant).ToList();
        }

        private static IEnumerable<JToken> GetSystemsByRegex(JContainer jsonTree)
        {
            var regex = new Regex("S\\d+", RegexOptions.Compiled);
            return (from descendant in jsonTree.Descendants() let properties = descendant.Children<JProperty>() from property in properties where property.Name.Equals("id") && regex.IsMatch(property.Value.ToString()) select descendant).ToList();
        }

        public static Tree BuildTreeFast(JArray jsonTree)
        {
            var myTree = new Tree(new MyNode("#"));
            var nodes = new List<JToken>();
            nodes.AddRange(GetCompaniesByRegex(jsonTree));
            nodes.AddRange(GetSitesByRegex(jsonTree));
            nodes.AddRange(GetLocationsByRegex(jsonTree));
            nodes.AddRange(GetSystemsByRegex(jsonTree));
            /*Parallel.ForEach(nodes, (jToken, loopstate) =>
            {
                var parentId = jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("parent"))?.Value
                    .ToString();
                var parent = myTree.Root.FindNodeFast(parentId, null, myTree.Root.Children);
                if (parent == null) return;
                var node = new MyNode(
                    jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("id"))?.Value.ToString(),
                    jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("text"))?.Value.ToString(),
                    jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("icon"))?.Value.ToString(),
                    jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("a_attr"))?.Value.ToString()
                );
                myTree.Nodes.Add(node);
                parent.AddChild(node);
            });*/
            foreach (var jToken in nodes)
            {
                var parentId = jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("parent"))?.Value
                    .ToString();
                var parent = myTree.Root.FindNode(parentId, null);
                if (parent == null) continue;
                var node = new MyNode(
                    jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("id"))?.Value.ToString(),
                    jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("text"))?.Value.ToString(),
                    jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("icon"))?.Value.ToString(),
                    jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("a_attr"))?.Value.ToString()
                );
                myTree.Nodes.Add(node);
                parent.AddChild(node);
            }
            return myTree;
        }

        public static Tree BuildTree(JArray jsonTree)
        {
            var regexps = new[] { new Regex("C\\d+"), new Regex("SI\\d+"), new Regex("L\\d+"), new Regex("S\\d+") };
            var myTree = new Tree(new MyNode("#"));
            foreach (var t in regexps)
            {
                var descendants = GetDescendantsByRegex(jsonTree, t);
                foreach (var jToken in descendants)
                {
                    var parentId = jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("parent"))?.Value
                        .ToString();
                    var parent = myTree.Root.FindNode(parentId, null);
                    if (parent == null) continue;
                    var node = new MyNode(
                            jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("id"))?.Value.ToString(),
                            jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("text"))?.Value.ToString(),
                            jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("icon"))?.Value.ToString(),
                            jToken.Children<JProperty>().FirstOrDefault(p => p.Name.Equals("a_attr"))?.Value.ToString()
                            );
                    myTree.Nodes.Add(node);
                    parent.AddChild(node);
                }
            }
            return myTree;
        }
    }
}
