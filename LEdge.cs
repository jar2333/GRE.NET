using System;
using System.Collections.Generic;
using QuikGraph;


namespace GraphRewriteEngine
{
    public class LEdge : IEdge<Node>, IEquatable<LEdge>, ITagged<string>, ICloneable { //add IComparable

        public string Tag { get; set; }
        public Node Source { get; set; }
        public Node Target { get; set; }

        public LEdge(Node a, Node b) { //check if this ordering makes this edge "unordered"
            if (a.Index < b.Index) {
                this.Source = a;
                this.Target = b; 
            }
            else {
                this.Source = b; 
                this.Target = a; 
            }
            this.Tag = "";
        }

        public LEdge(Node a, Node b, string tag) {
            if (a.Index < b.Index) {
                this.Source = a;
                this.Target = b; 
            }
            else {
                this.Source = b; 
                this.Target = a; 
            }
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
            return $"e({this.Source.ToString()}, {this.Target.ToString()}).L:{this.Tag}";
        }

        //Should this be one-sided? Does this two-sided implementation mess things up?
        public bool Equals (LEdge e) {
            if (e == null) {
                return false;
            }
            return this.Source.Equals(e.Source) && this.Target.Equals(e.Target);
                    //|| this.Source.Equals(e.Target) && this.Target.Equals(e.Source);
        }

        public object Clone() {
            return new LEdge(this.Source.Clone() as Node, this.Target.Clone() as Node, this.Tag);
        }

        public bool IsEquivalent(LEdge e) {
            return this.Tag.Equals(e.Tag);
        }

        public event EventHandler TagChanged;



    }

}