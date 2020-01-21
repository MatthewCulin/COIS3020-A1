using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culin_A1
{
    public class Vertex<T>
    {
        public T Name { get; set; }             // VERTEX NAME
        public bool Visited { get; set; }       // VISITED OR UNVISITED
        public List<Edge<T>> E { get; set; }    // LIST OF ADJACENT VERTICIES

        /*-------------------------------
        |                               |
        |           ADDITIONS           |
        |                               |
        |------------------------------*/

        public int Colour { get; set; }         // DETERMINE WHAT COLOUR THE VERTEX IS

        public int DiscoveryTime { get; set; }  // DISCOVERY TIME OF THE VERTEX (DFS)

        public int FinishingTime { get; set; }  // FINISHING TIME OF THE VERTEX (DFS)

        public int InDegree { get; set; }       // NUMBER OF VERTEICIES THAT HAVE A REFERENCE TO THE VERTEX


        /*-------------------------------------------------------
        |
        |       Name: Vertex
        |
        |       Purpose: Constructor for the Vertex class
        |
        |       Parameters: - T name
        |
        --------------------------------------------------------*/
        public Vertex(T name)
        {
            Name = name;
            Visited = false;
            E = new List<Edge<T>>();
        }// END OF VERTEX CONSTRUCTOR


        /*-------------------------------------------------------
        |
        |       Name: FindEdge
        |
        |       Purpose: Returns the index of the given adjacent 
        |                vertex in E; otherwise returns -1
        |
        |       Parameters: - T name
        |
        --------------------------------------------------------*/
        public int FindEdge(T name)
        {
            int i;

            for(i = 0; i < E.Count; i++)
            {
                if (E[i].AdjVertex.Name.Equals(name))
                    return i;
            }

            return -1;
        }// END OF FIND EDGE

    }// END OF VERTEX <T> CLASS
}
