using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Node : ITagged<string> {
        
        public int Index {get; set;}
        public string Tag {get; set;}

        public Node(int index, string tag) {
            this.Index = index;
            this.Tag = tag;
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
            return $"v{this.Index}:L.{this.Tag}";
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
            return this.Tag.Equals(n.Tag);
        }

        public event EventHandler TagChanged;
    }
}