using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Mapping {

            public Node[] M;
            
            public HashSet<Node> D;

            public HashSet<Node> R;

            //Empty starting mapping
            public Mapping(int V1Size) {
                M = new Node[V1Size];
                D = new HashSet<Node>(); 
                R = new HashSet<Node>();

            }

            //For extended mappings
            public Mapping(int V1Size, HashSet<Node> d, HashSet<Node> r) {
                M = new Node[V1Size];
                D = new HashSet<Node>(d); 
                R = new HashSet<Node>(r);

            }

            //The matching order index is embedded in the node's index attribute
            //creates copy of mapping to return (not altering the original)
            public Mapping Extend(Node u, Node v) {
                Mapping extended = new Mapping(M.Length, this.D, this.R);
                extended.M[u.Index] = v;
                extended.D.Add(u);
                extended.R.Add(v);
                return extended;
            }

            public bool Covers(IEnumerable<Node> V) {
                return D.IsSupersetOf(V) || R.IsSupersetOf(V);
            }

            public bool Covers(Node v) {
                return D.Contains(v) || R.Contains(v);
            }

    }
}