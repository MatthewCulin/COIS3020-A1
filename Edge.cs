using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culin_A1
{
    public class Edge<T>
    {
        public Vertex<T> AdjVertex { get; set; }
        public int Cost { get; set; }

        public string EdgeType { get; set; }

        /*-------------------------------------------------------
        |
        |       Name: Edge
        |
        |       Purpose: Constructor for the Edge class
        |
        |       Parameters: - Vertex<T> vertex
        |                   - int cost
        |
        --------------------------------------------------------*/
        public Edge(Vertex<T> vertex, int cost)
        {
            AdjVertex = vertex;
            Cost = cost;
        }//END OF EDGE CONSTRUCTOR

    }// END OF EDGE <T> CLASS
}
