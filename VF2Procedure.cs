using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine {

    public abstract class VF2Procedure: IMatcher {

        public BidirectionalGraph<Node, LEdge> pattern;
        public BidirectionalGraph<Node, LEdge> host;

        public List<Morphism> morphisms;

        //Constructor
        public VF2Procedure() {
            this.morphisms = new List<Morphism>();
        }

        //Main procedure     
        public abstract void VF2(int iter);

        //purely helper method (perhaps optimize with Linq)
        public bool ExistsUncoveredNeighbor(Node v, IEnumerable<Node> V, BidirectionalGraph<Node, LEdge> G) {
            IEnumerable<Node> neighbors = AdjacentVertices(G, v);
            foreach (var node in V) {
                if (neighbors.Contains(node)) {
                    return true;
                }
            }
            return false;
        }

        //Cartesian product helper, perhaps change Node[] to Tuple<Node>?
        public IEnumerable<Node[]> CartesianProduct(IEnumerable<Node> A, IEnumerable<Node> B) {
            var product =
                from first in A
                from second in B
                select new[] {first, second};

            return product; //this returns an array of Node arrays I think
        }

        public IEnumerable<Node> AdjacentVertices(BidirectionalGraph<Node, LEdge> G, Node n) {
            IEnumerable<LEdge> adjacentEdges = G.InEdges(n).Concat(G.OutEdges(n));
            List<Node> adjacentVertices = new List<Node>();
            foreach (var e in adjacentEdges) {
                if (e.Source.Equals(n)) {
                    adjacentVertices.Add(e.Target);
                }
                else {
                    adjacentVertices.Add(e.Source);
                }
            }
            return adjacentVertices.AsEnumerable<Node>();
        }

        //Yeah, make a mapping class, with IEnumerables for D(m) and R(m)
        public IEnumerable<Node[]> Candidates(NodeMapping m) {
            IEnumerable<Node> uV1 = pattern.Vertices.Except(m.M.Keys);
            IEnumerable<Node> uV2 = host.Vertices.Except(m.M.Values);

            IEnumerable<Node> T1 = uV1.Where(node => ExistsUncoveredNeighbor(node, m.M.Keys, pattern));
            IEnumerable<Node> T2 = uV2.Where(node => ExistsUncoveredNeighbor(node, m.M.Values, host));
            //Use IEnumerable.Any to check if empty
            if (T1.Any() && T2.Any()) {
                return CartesianProduct(T1, T2);
            }
            return CartesianProduct(uV1, uV2);
            
        }

        //problem specific functions
        public abstract bool Cons(NodeMapping m, Node[] p);

        public abstract bool Cut(NodeMapping m, Node[] p);


        //Interface methods
        public abstract Morphism Find(BidirectionalGraph<Node, LEdge> pattern, BidirectionalGraph<Node, LEdge> host);

        public abstract IList<Morphism> Enumerate(BidirectionalGraph<Node, LEdge> pattern, BidirectionalGraph<Node, LEdge> host, int iter = 0);

        public abstract bool Exists(BidirectionalGraph<Node, LEdge> pattern, BidirectionalGraph<Node, LEdge> host);

    }

}