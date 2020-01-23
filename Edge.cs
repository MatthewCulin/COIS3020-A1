using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

        /*-----------------------------------------------
        |                                               |
        |                   ADDITIONS                   |
        |                                               |
        |   public string EdgeType { }        (23)      |
        |                                               |
        |----------------------------------------------*/

namespace Culin_A1
{
    public class Edge<T>
    {
        public Vertex<T> AdjVertex { get; set; }    // REFERENCE TO THE ADJACENT VERTEX
        
        public int Cost { get; set; }               // COST OF THE EDGE

        public string EdgeType { get; set; }        // EDGE TYPE DETERMINED BY THE DEPTH FIRST SEACRH

        public string EdgeInfo
        {
            get
            {
                return $"{AdjVertex.Name}, {EdgeType}";
            }
        }

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
            AdjVertex = vertex; // SETS THE ADJACENT VERTEX
            Cost = cost;        // SETS THE COST 
        }//END OF EDGE CONSTRUCTOR

    }// END OF EDGE <T> CLASS
}
