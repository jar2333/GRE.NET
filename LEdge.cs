using System;
using System.Collections.Generic;
using QuikGraph;


namespace GraphRewriteEngine
{
    public class LEdge : IEdge<Node>, IEquatable<LEdge>, ITagged<string> {

        public string Tag { get; set; }
        public Node Source { get; set; }
        public Node Target { get; set; }

        public LEdge(Node a, Node b, string tag) {
            this.Source = a;
            this.Source = b;
            this.Tag = tag;
        }

        public override int GetHashCode()
        {
            return (this.Source.Index, this.Source.Index).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as LEdge);
        }

        public override string ToString()
        {
            return $"e({this.Source.ToString()}, {this.Target.ToString()}):L.{this.Tag}";
        }

        //Should this be two-sided?
        public bool Equals (LEdge e) {
            if (e == null) {
                return false;
            }
            return this.Source.Equals(e.Source) && this.Target.Equals(e.Target);
        }

        public bool IsEquivalent(LEdge e) {
            return this.Tag.Equals(e.Tag);
        }

        public event EventHandler TagChanged;



    }

}