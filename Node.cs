using System;
using System.Collections.Generic;
using QuikGraph;

namespace GraphRewriteEngine
{
    public class Node {
        
        public int Index {get; set;}
        public string Label {get; set;}
        public Dictionary<string, string> attributes;

        public Node(int index, string label) {
            attributes = new Dictionary<string, string>();
            this.Index = index;
            this.Label = label;
        }

        public void AddAttribute(string key, string value) {
            attributes[key] = value;
        }

        public string GetAttribute(string key) {
            string att = "null";
            attributes.TryGetValue(key, out att);
            return att;
        }

        public bool Equals(Node n) {
            if (n == null) {
                return false;
            }
            return this.Index.Equals(n.Index);
        }

        public bool IsEquivalent(Node n) {
            return this.Label.Equals(n.Label);
        }
    }
}