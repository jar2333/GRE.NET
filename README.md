# GRE: Graph Rewrite Engine

An engine for simple labelled graph rewriting. 

Provides interfaces for implementing subgraph isomorphism algorithms

Included is VF2++ (Jüttner and Madarasi, 2018)
https://www.sciencedirect.com/science/article/abs/pii/S0166218X18300829

For the purposes of graph transformation, all morphisms are assumed to be injective. 
The included VF2 modules produce such morphisms. This assumption may change in the future.

Currently functional:
* naive VF2 Induced Subgraph matching (partially implemented, edge labels not preserved)
* Graph Rewriting through Rewriter objects (functional, not optimized, not all cases tested)

Roadmap:
1) Implement VF2++ fully (in progress)
2) Implement Graph Grammars and Graph Rewriting (in progress)
3) Implement grammar rule parser/serialization
4) Implement Probabilistic Graph Grammars and Probabilistic Rewriting
5) Optimization and cleanup
6) First NuGet release

Extension package planned for Probabilistic Edge Replacement Grammars (PERGs):
* production weight learner classes (max likelihood estimators from graph data)
  - (T. Oates et al, 2003), (Agunaga et al, 2016), (Reddy et al, 2019)
* production weight solver classes (systems of equations with graph property probabilities)
  - (B. Courcelle, 1990), (M. Mosbah, 1996)

Dependencies:
- QuikGraph 2.3.0