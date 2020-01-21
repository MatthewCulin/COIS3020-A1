using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culin_A1
{
    public interface IDirectedGraph<T>
    {
        void AddVertex(T name);
        void RemoveVertex(T name);
        void AddEdge(T name1, T name2, int cost);
        void RemoveEdge(T name1, T name2);

    }// END OF I DIRECTED GRAPH <T> CLASS
}
