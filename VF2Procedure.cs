using System;
using System.Collections.Generic;
using System.Linq;
using QuikGraph;

namespace GraphRewriteEngine {

    public abstract class VF2Procedure: IMapper {

        public UndirectedGraph<Node, LEdge> pattern;
        public UndirectedGraph<Node, LEdge> host;

        public List<Morphism> morphisms;

        //Constructor
        public VF2Procedure() {
            this.morphisms = new List<Morphism>();
        }

        //Main procedure, avoid the recursion (fix later)
        public void VF2(NodeMapping m) {
            if (m.Covers(pattern.Vertices)) {
                //Getting the edge mapping from a given valid Ind node mapping is straightforward
                ICollection<Node> D = m.M.Keys;
                IEnumerable<LEdge> E1 = pattern.Edges.Where(e => D.Contains(e.Source) && D.Contains(e.Target));
                var edgeMap = new Dictionary<LEdge, LEdge>(); 
                foreach (var e in E1) {
                    edgeMap[e] = new LEdge(m.M[e.Source], m.M[e.Target]);
                }
                this.morphisms.Add(new Morphism(m, new EdgeMapping(edgeMap)));
            }
            else {
                IEnumerable<Node[]> candidatePairs = Candidates(m);
                foreach (var p in candidatePairs) {
                    if (Cons(m, p) && !Cut(m, p)) {
                        VF2(m.Extend(p[0], p[1]));
                    }
                }
            }
        }

        
        public void VF2(int iter) {
            Stack<NodeMapping> mapStack = new Stack<NodeMapping>();
            mapStack.Push(new NodeMapping());
            int i = 0; //the number of added morphisms
            NodeMapping m;
            while (mapStack.Count > 0 && (iter == 0 ? true : iter > i)) {
                m = mapStack.Pop();
                if (m.Covers(pattern.Vertices)) {
                    //Getting the edge mapping from a given valid Ind node mapping is straightforward
                    ICollection<Node> D = m.M.Keys;
                    IEnumerable<LEdge> E1 = pattern.Edges.Where(e => D.Contains(e.Source) && D.Contains(e.Target));
                    var edgeMap = new Dictionary<LEdge, LEdge>(); 
                    foreach (var e in E1) {
                        edgeMap[e] = new LEdge(m.M[e.Source], m.M[e.Target]);
                    }
                    this.morphisms.Add(new Morphism(m, new EdgeMapping(edgeMap)));
                    i++;
                }
                else {
                    IEnumerable<Node[]> candidatePairs = Candidates(m);
                    foreach (var p in candidatePairs) {
                        if (Cons(m, p) && !Cut(m, p)) {
                            mapStack.Push(m.Extend(p[0], p[1]));
                        }
                    }
                }
            }

        }

        //purely helper method (perhaps optimize with Linq)
        public bool ExistsUncoveredNeighbor(Node v, IEnumerable<Node> V, UndirectedGraph<Node, LEdge> G) {
            IEnumerable<Node> neighbors = G.AdjacentVertices(v);
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
        public abstract Morphism Find(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host);

        public abstract IList<Morphism> Enumerate(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host, int iter = 0);

        public abstract bool Exists(UndirectedGraph<Node, LEdge> pattern, UndirectedGraph<Node, LEdge> host);

    }

}