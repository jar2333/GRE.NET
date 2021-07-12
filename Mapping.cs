using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Mapping {

            public Dictionary<Node, Node> M;

            //Empty starting mapping
            public Mapping() {
                M = new Dictionary<Node, Node>();
            }

            //For extended mappings
            public Mapping(Dictionary<Node, Node> m) {
                M = new Dictionary<Node, Node>(m);
            }

            //The matching order index is embedded in the node's index attribute
            //creates copy of mapping to return (not altering the original)
            public Mapping Extend(Node u, Node v) {
                Mapping extended = new Mapping(this.M);
                extended.M[u] = v;
                return extended;
            }

            //This could be way better, but suffices for now
            public bool Covers(IEnumerable<Node> V) {
                return V.Where(v => Covers(v)).Count() == V.Count();
            }

            public bool Covers(Node v) {
                return M.Keys.Contains(v) || M.Values.Contains(v);
            }

    }
}