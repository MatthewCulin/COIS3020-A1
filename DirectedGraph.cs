using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culin_A1
{
    class DirectedGraph<T> : IDirectedGraph<T>
    {
        public List<Vertex<T>> V;


        /*-------------------------------------------------------
        |
        |       Name: DirectedGraph
        |
        |       Purpose: Constructor for the Directed Graph class
        |
        |       Parameters:
        |
        --------------------------------------------------------*/
        public DirectedGraph()
        {
            V = new List<Vertex<T>>();
        }// END OF DIRECTED GRAPH CONSTRUCTOR


        /*-------------------------------------------------------
        |
        |       Name: Find Vertex
        |
        |       Purpose: Returns the index of the given vertex
        |                if found; otherwise return -1
        |
        |       Parameters: - T name
        |
        --------------------------------------------------------*/
        public int FindVertex(T name)
        {
            int i;

            for (i = 0; i < V.Count; i++)
            {
                if (V[i].Name.Equals(name))
                    return i;
            }

            return -1;
        }// END OF FIND VERTEX


        /*-------------------------------------------------------
        |
        |       Name: AddVertex
        |
        |       Purpose: Adds the given vertex ti the graph
        |
        |       Parameters: - T name
        |
        --------------------------------------------------------*/
        public void AddVertex(T name)
        {
            if (FindVertex(name) == -1)
            {
                Vertex<T> v = new Vertex<T>(name);
                V.Add(v);
            }
        }// END OF ADD VERTEX


        /*-------------------------------------------------------
        |
        |       Name: RemoveVertex
        |
        |       Purpose: Removes the given vertex and all edges
        |                from the graph
        |
        |       Parameters: - T name
        |
        --------------------------------------------------------*/
        public void RemoveVertex(T name)
        {
            int i, j, k;

            if ((i = FindVertex(name)) > -1)
            {
                for (j = 0; j < V.Count; j++)
                {
                    for (k = 0; k < V[j].E.Count; k++)
                    {
                        if (V[j].E[k].AdjVertex.Name.Equals(name))   // Incident edge
                        {
                            V[j].E.RemoveAt(k);
                            break;  // Since there are no duplicate edges
                        }
                    } 
                }

                V.RemoveAt(i);
            }
        }// END OF REMOVE VERTEX


        /*-------------------------------------------------------
        |
        |       Name: AddEdge
        |
        |       Purpose: Adds an edge from two verticies 
        |                (name1 -> name2) and associates a cost
        |
        |       Parameters: - T name1
        |                   - T name2
        |                   - int cost
        |
        --------------------------------------------------------*/
        public void AddEdge(T name1, T name2, int cost)
        {
            int i, j;
            Edge<T> e;

            if ((i = FindVertex(name1)) > -1 && (j = FindVertex(name2)) > -1)
            {
                if (V[i].FindEdge(name2) == -1)
                {
                    e = new Edge<T>(V[j], cost);
                    V[i].E.Add(e);
                }
            }
        }// END OF ADD EDGE
        

        /*-------------------------------------------------------
        |
        |       Name: RemoveEdge
        |
        |       Purpose: Removes the edge between two verticies
        |                (name1 -> name2) from the graph
        |
        |       Parameters: - T name1
        |                   - T name2
        |
        --------------------------------------------------------*/
        public void RemoveEdge(T name1, T name2)
        {
            int i, j;

            if ((i = FindVertex(name1)) > -1 && (j = V[i].FindEdge(name2)) > -1)
                V[i].E.RemoveAt(j);
        }// END OF REMOVE EDGE


        /*-------------------------------------------------------
        |
        |       Name: DepthFirstSearch
        |
        |       Purpose: Performs a depth-first search with 
        |                re-start
        |
        |       Parameters: 
        |
        --------------------------------------------------------*/
        public void DepthFirstSearch()
        {
            int i;

            for (i = 0; i < V.Count; i++)     // Set all vertices as unvisited
                V[i].Visited = false;

            for (i = 0; i < V.Count; i++)
                if (!V[i].Visited)                  // (Re)start with vertex i
                {
                    DepthFirstSearch(V[i]);
                    Console.WriteLine();
                }
        }// END OF DEPTH FIRST SEARCH


        /*-------------------------------------------------------
        |
        |       Name: DepthFirstSearch
        |
        |       Purpose: Performs a depth-first search with 
        |                re-start
        |
        |       Parameters: - Vertex<T> v
        |
        --------------------------------------------------------*/
        private void DepthFirstSearch(Vertex<T> v)
        {
            int j, k;
            Vertex<T> w;

            v.Visited = true;    // Output vertex when marked as visited
            Console.WriteLine(v.Name);

            for (j = 0; j < v.E.Count; j++)       // Visit next adjacent vertex
            {
                w = v.E[j].AdjVertex;  // Find index of adjacent vertex in V
                if (!w.Visited)
                    DepthFirstSearch(w);
            }
        }// END OF DEPTH FIRST SEARCH


        /*-------------------------------------------------------
        |
        |       Name: BreadthFirstSearch
        |
        |       Purpose: Performs a breadth-first search with 
        |                re-start
        |
        |       Parameters: 
        |
        --------------------------------------------------------*/
        public void BreadthFirstSearch()
        {
            int i;

            for (i = 0; i < V.Count; i++)
                V[i].Visited = false;              // Set all vertices as unvisited

            for (i = 0; i < V.Count; i++)
            {
                if (!V[i].Visited)                  // (Re)start with vertex i
                {
                    BreadthFirstSearch(V[i]);
                    Console.WriteLine();
                }
            }
                
        }// END OF BREADTH FIRST SEARCH


        /*-------------------------------------------------------
        |
        |       Name: BreadthFirstSearch
        |
        |       Purpose: Performs a breadth-first search with 
        |                re-start
        |
        |       Parameters: - Vertex<T> v
        |
        --------------------------------------------------------*/
        private void BreadthFirstSearch(Vertex<T> v)
        {
            int j;
            Vertex<T> w;
            Queue<Vertex<T>> Q = new Queue<Vertex<T>>();

            v.Visited = true;        // Mark vertex as visited when placed in the queue
            Q.Enqueue(v);            // Why? 

            while (Q.Count != 0)
            {
                v = Q.Dequeue();     // Output vertex when removed from the queue
                Console.WriteLine(v.Name);

                for (j = 0; j < v.E.Count; j++)    // Enqueue unvisited adjacent vertices
                {
                    w = v.E[j].AdjVertex;
                    if (!w.Visited)
                    {
                        w.Visited = true;          // Mark vertex as visited
                        Q.Enqueue(w);
                    }
                }
            }
        }// END OF BREADTH FIRST SEARCH


        /*-------------------------------------------------------
        |
        |       Name: PrintVerticies
        |
        |       Purpose: Prints out all verticies of a graph
        |
        |       Parameters: 
        |
        --------------------------------------------------------*/
        public void PrintVertices()
        {
            for (int i = 0; i < V.Count; i++)
            {
                Console.WriteLine(V[i].Name);
            }
            Console.ReadLine();
        }// END OF PRINT VERTICIES

        
        /*-------------------------------------------------------
        |
        |       Name: PrintEdges
        |
        |       Purpose: Prints out all edges of a graph
        |
        |       Parameters: 
        |
        --------------------------------------------------------*/
        public void PrintEdges()
        {
            int i, j;
            for (i = 0; i < V.Count; i++)
            {
                for (j = 0; j < V[i].E.Count; j++)
                {
                    Console.WriteLine("(" + V[i].Name + "," + 
                        V[i].E[j].AdjVertex.Name + "," + V[i].E[j].Cost + ")");
                }
            }
                
            Console.ReadLine();
        }// END OF PRINT EDGES

    }// END OF DIRECTED GRAPH <T> CLASS
}
