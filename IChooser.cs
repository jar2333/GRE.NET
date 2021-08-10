using System;
using System.Collections.Generic;

namespace GraphRewriteEngine
{
    public interface IChooser {

        //Revamp the chooser

        //For the ProbabilisticRule chooser, add property for AliasSampler
        //The constructor then build this, given a Random object. Work out the details as you read more
        int Choose(Dictionary<int, Rule> rules); 
        
    }
}