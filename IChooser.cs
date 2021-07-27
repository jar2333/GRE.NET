using System;
using System.Collections.Generic;

namespace GraphRewriteEngine
{
    public interface IChooser {

        int Choose(Dictionary<int, Rule> rules); 
        
    }
}