using System;
using System.Collections.Generic;
using QuikGraph;


namespace GraphRewriteEngine
{
    public class LEdge : IEdge<Node> {

        public string Label { get; set; }
        public Node Source { get; set; }
        public Node Target { get; set; }

        public LEdge(Node a, Node b, string label) {
            this.Source = a;
            this.Source = b;
            this.Label = label;
        }

        public bool Equals (LEdge e) {
            if (e == null) {
                return false;
            }
            return this.Source.Equals(e.Source) && this.Target.Equals(e.Target);
        }

        public bool IsEquivalent(LEdge e) {
            return this.Label.Equals(e.Label);
        }

    }

}