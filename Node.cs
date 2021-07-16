using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Node : IEquatable<Node>, IComparable<Node>, ITagged<string> { //Add IComparable
        
        public int Index {get; set;}
        public string Tag {get; set;}

        public Node(int index) {
            this.Index = index;
            this.Tag = "";
        }

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
            if (obj is int) {
                return Equals(obj);
            }
            return Equals(obj as Node);

        }

        public int CompareTo(Node n) {
            if (n == null) {
                return 1;
            }
            return this.Index.CompareTo(n.Index);
        }

        //defining for convinience
        public override string ToString()
        {
            return $"v{this.Index}.L:{this.Tag}";
        }

        //to define overwritten
        public bool Equals(Node n) {
            if (n == null) {
                return false;
            }
            return this.Index.Equals(n.Index);
        }

        //for indexing!
        public bool Equals(int i) { 
            return this.Index.Equals(i);
        }

        //label equivalence is not node equality
        public bool IsEquivalent(Node n) {
            return this.Tag.Equals(n.Tag);
        }

        public event EventHandler TagChanged;
    }
}