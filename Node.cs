using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Node {
        
        public int Index {get; set;}
        public string Label {get; set;}

        public Node(int index, string label) {
            this.Index = index;
            this.Label = label;
        }

        //overriding for hashing purposes
        public override int GetHashCode()
        {
            return this.Index;
        }

        public override bool Equals(object obj) {
            return Equals(obj as Node);

        }

        //defining for convinience
        public override string ToString()
        {
            return $"v{this.Index}:L.{this.Label}";
        }

        //to define overwritten
        public bool Equals(Node n) {
            if (n == null) {
                return false;
            }
            return this.Index.Equals(n.Index);
        }

        //label equivalence is not node equality
        public bool IsEquivalent(Node n) {
            return this.Label.Equals(n.Label);
        }
    }
}