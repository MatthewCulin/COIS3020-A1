﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

        /*----------------------------------------------------------------------------------
        |                                                                                   |
        |                                           ADDITIONS                               |
        |   Global Declarations                                                             |
        |       - int timer = 0;                                                            |
        |       - bool cycle = false;                                                       |
        |                                                                                   |
        |   RemoveVertex(name)                                                              |
        |       - For loop to remove all edges from deleted vertex                          |
        |         (calls RemoveEdge( ) to ensure the InDegree for each is decremented)      |
        |                                                                                   |
        |   AddEdge(name, name, cost)                                                       |
        |       - Increment InDegree for destination Vertex                                 |
        |                                                                                   |
        |   RemoveEdge(name, name)                                                          |
        |       - Decrements InDegree for destination Vertex                                |
        |                                                                                   |
        |   DepthFirstSearch(v)                                                             |
        |       - Updates vertex colour based on the traversal                              |
        |         (set to white in Vertex class)                                            |
        |       - Sets discovery time and finsihing time based of timer incrementor         |
        |       - Updates branch type based on traversal                                    |
        |         (Sets cycle boolean to true if there is a back edge)                      |
        |                                                                                   |
        |----------------------------------------------------------------------------------*/

namespace Culin_A1
{
    class DirectedGraph<T> : IDirectedGraph<T>
    {
        public List<Vertex<T>> V;
        List<Vertex<T>> first;      // USED FOR FIRST METHOD OF TOPOLOGICAL SORT
        
        List<Vertex<T>> vertex_copy;
        List<Edge<T>> edge_copy;

        int timer = 0;              // INITIALIZES THE TIMER
        bool cycle = false;         // SET CYCLE TO FALSE; TRUE IF THERE IS A CYCLE IN THE GRAPH


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
            first = new List<Vertex<T>>();
            vertex_copy = V;
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
            T dest;

            if ((i = FindVertex(name)) > -1)
            { 
                for (j = 0; j < V.Count; j++)
                {
                    // IF THE VERTEX INDEX IS THE ONE BEING REMOVED
                    // GO THROUGH THE EDGES ARRAY AND REMOVE ALL EDGES
                    // THIS WILL ENSURE THE INDEGREE FOR EACH ADJACENT
                    // WILL DECREASE
                    if (V[j].Name.Equals(name))
                    {
                        for (k = 0; k < V[j].E.Count(); k++)
                        {
                            dest = V[j].E[k].AdjVertex.Name;
                            RemoveEdge(name, dest);
                        }
                    }

                    // GO THROUGH THE EDGES ARRAY AND DELETE ALL EDGES
                    // GOING TO THE VERTEX BEING DELETED
                    else
                    {
                        for (k = 0; k < V[j].E.Count; k++)
                        {
                            if (V[j].E[k].AdjVertex.Name.Equals(name))   // INCIDENT EDGE
                            {
                                V[j].E.RemoveAt(k);
                                break;                                  // SINCE THERE ARE NO DUPLICATE EDGES
                            }
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
                    V[j].InDegree++;
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
            int i, j, k;

            if ((i = FindVertex(name1)) > -1 && (j = V[i].FindEdge(name2)) > -1)
            {
                k = FindVertex(name2);

                V[i].E.RemoveAt(j);
                V[k].InDegree--;
            }
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
            int i, j;

            timer = 0;

            for (i = 0; i < V.Count; i++)   // SET ALL VERTICIES AS UNVISITED
            { 
                V[i].Visited = false;
            }

            for (i = 0; i < V.Count; i++)
            {
                if (!V[i].Visited)          // RESET WITH VERTEX I
                {
                    DepthFirstSearch(V[i]);
                    Console.WriteLine();
                }
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
            int j;
            Vertex<T> w;
            timer++;                            // INCREMENT TIMER (DISCOVERY TIME)

            v.Visited = true;                   // MARK VERTEX AS VISITED
            v.Colour = "GREY";                  // MARK VERTEX COLOUR AS GREY; INITIALIZED WHEN VERTEX CREATED
            v.DiscoveryTime = timer;            // SET DISCOVERY TIME

            Console.WriteLine(v.Name);          // OUTPUT CURRENT VERTEX

            for (j = 0; j < v.E.Count; j++)     // VISIT NEXT ADJACENT VERTEX
            {
                w = v.E[j].AdjVertex;           // FIND INDEX OF ADJACENT VERTEX IN V
                if (!w.Visited)
                {
                    v.E[j].EdgeType = "TREE";   // SET EDGE TYPE TO TREE
                    DepthFirstSearch(w);        // PERFORM A DEPTH FIRST SEARCH TO VERTEX W
                }
                else
                {
                    switch (w.Colour)
                    {
                        // SET CASE FOR THE UNLIKELY SITUATION AN UNVISITED VERTEX SLIPS THROUGH TO HERE
                        // VERTEX W COLOUR IS WHITE
                        case "WHITE":
                            v.E[j].EdgeType = "TREE";   // SET CURRENT VERTEX EDGE TYPE TO TREE
                            DepthFirstSearch(w);        // PERFORM A DEPTH FIRST SEARCH TO VERTEX W
                            break;

                        // VERTEX W COLOUR IS GREY
                        case "GREY":
                            v.E[j].EdgeType = "BACK ";  // SET CURRENT VERTEX EDGE TYPE TO BACK
                            cycle = true;               // SINCE THERE IS A BACK EDGE; GRAPH NOT ACYCLIC
                            break;

                        // VERTEX W COLOUR IS BLACK
                        case "BLACK":
                            if (v.DiscoveryTime < w.DiscoveryTime)      // CURRENT VERTEX HAS NOT FINISHED
                                v.E[j].EdgeType = "FORWARD";            // SET CURRENT VERTEX EDGE TYPE TO FORWARD
                            else if (v.DiscoveryTime > w.DiscoveryTime) // CURRENT VERTEX HAS A SMALLER FINISHING TIME THAN VERTEX W
                                v.E[j].EdgeType = "CROSS";              // SET CURRENT VERTEX EDGE TO CROSS 
                            break;

                        default:
                            break;
                    }
                }

            }

            timer++;                            // INCREMENT TIMER (FINSIHING TIME)
            v.Colour = "BLACK";                 // SETS COLOUR OF THE VERTEX TO BLACK ONCE COMPLETED     
            v.FinishingTime = timer;            // SETS THE FINSIHING TIME 
              
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
                V[i].Visited = false;              // SET ALL VERTICIES TO UNVISISTED

            for (i = 0; i < V.Count; i++)
            {
                if (!V[i].Visited)                  // RESTART WITH VERTEX I
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

            v.Visited = true;                               // MARK VERTEX AS VISITED WHEN PLACED IN THE QUEUE
            Q.Enqueue(v);            

            while (Q.Count != 0)
            {
                v = Q.Dequeue();                            // OUTPUT VERTEX WHEN REMOVED FROM THE QUEUE
                Console.WriteLine(v.Name);

                for (j = 0; j < v.E.Count; j++)             // ENQUEUE UNIVISITED ADJACENT VERTICIES
                {
                    w = v.E[j].AdjVertex;
                    if (!w.Visited)
                    {
                        w.Visited = true;                   // MARK VERTEX AS VISISTED
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


        /*-------------------------------------------------------
        |
        |       Name: SortTopological
        |
        |       Purpose: Prints the graph in topological order
        |
        |       Parameters: 
        |
        --------------------------------------------------------*/
        public int SortTopological()
        {
            int i;

            // MAKE SURE GRAPH IS ACYCLICAL
            // ALSO SORTS USING THE FIRST METHOD
            DepthFirstSearch();

            // GRAPH IS ACYCLIC
            if (!cycle)
            {
                // SORTED USING THE FIRST METHOD
                for (i = 0; i < first.Count; i++)
                    Console.WriteLine("Method 1: " + first[i].Name);
                Console.WriteLine();

                // SORTED USING THE SECOND METHOD
                for (i = 0; i < vertex_copy.Count; i++)
                    Console.WriteLine("Method 2: " + vertex_copy[i].Name);
                Console.ReadLine();
                
                return (1);
            }

            // GRAPH IS NOT ACYCLIC
            else
                return (-1);

        }// END OF PRINT EDGES

    }// END OF DIRECTED GRAPH <T> CLASS
}
