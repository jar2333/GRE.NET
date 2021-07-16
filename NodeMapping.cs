using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class NodeMapping : Mapping<Node> {

            //Empty starting mapping
            public NodeMapping() {
                M = new Dictionary<Node, Node>();
            }

            public NodeMapping(Dictionary<Node, Node> m) {
                M = new Dictionary<Node, Node>(m);
            }

            //The matching order index is embedded in the node's index attribute
            //creates copy of mapping to return (not altering the original)
            public NodeMapping Extend(Node u, Node v) {
                NodeMapping extended = new NodeMapping(this.M);
                extended.M[u] = v;
                return extended;
            }

            //This could be way better, but suffices for now
            public bool Covers(IEnumerable<Node> V) {
                return V.Where(v => Covers(v)).Count() == V.Count();
            }

            public bool Covers(Node v) { //Despite the paper including both D and R, that causes issues
                return M.Keys.Contains(v);
            }

    }
}