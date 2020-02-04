/*
|   
|   FILE TYPE:      C# PROGRAM
|   AUTHOR:         MATTHEW CULIN
|   DATE:           JANUARY 2020
|       
|   PURPOSE:        PROGRAM TO MODEL 
|   USEAGE:         Culin-A1.exe
|
|   SOURCE CODE:    SOURCE CODE FROM BRIAN PATRICK (AdjacencyList.cs)
|
|   MODIFICATIONS:  A BRIEF DESCRIPTION OF THE MODIFICATIONS MADE CAN BE FOUND BELOW...
|
|       PROGRAM
|     -----------
|           ALLOWS FOR USER INPUT IN SELECTING A GRAPH TYPE AND
|           MODIFICATIONS TO MAKE TO THE GRAPH. THE USER IS
|           SHOWN THE MODIFICATION MENU AFTER EACH SUCCESSFULL 
|           MODIFICATION UNTIL THEY CHOSE TO FINSIH. 
|           
|           METHODS CREATED ARE AS FOLLOWED...
|               - AddVertex ( )
|               - RemoveVertex ( )
|               - AddEdge ( )
|               - RemoveEdge ( )
|               - PrintGraph ( )
|               - TopologicalSort ( )
|           EACH METHOD HAS ITS OWN DESCRIPTION IN A HEADER ABOVE ITS DECLARATION.
|
|       DIRECTED GRAPH
|     ------------------
|           PERFORMS BEHIND THE SCENES MODIFICATIONS TO THE GRAPH
|           AS USER HAS CHOSEN IN THE PROGRAM CLASS. 
|
|           METHODS CREATED ARE AS FOLLOWED...
|               - SortTopological ( )
|               - InDegreeTopological ( )
|               - SortByIndegree ( )
|           EACH METHOD HAS ITS OWN DESCRIPTION IN A HEADER ABOVE ITS DECLARATION.
|
|       VERTEX
|     ----------
|           CREATES A VERTEX WHICH STORES A SERIES OF DATA MEMBERS
|           INCLUDING A NAME, IN DEGREE COUNT, DISCOVERY TIME, LIST
|           OF EDGES, AND A FINSIHING TIME.
|
|           DATA MEMBERS CREATED ARE AS FOLLOWED...
|               - public string Colour
|               - public int DiscoveryTime
|               - public int FinishingTime
|               - public int InDegree
|               - public string VertexInfo
|           EACH DATA MEMBER HAS ITS OWN DESCRIPTION IN A COMMENT BESIDE ITS DECLARATION.  
|
|       EDGE
|     --------
|           CREATES AN EDGE TO BE INSERTED INTO THE VERTEX EDGE 
|           LIST. STORES DATA MEMBERS SUCH AS ADJACENT VERTEX, 
|           COST, AND EDGE TYPE.
|
|           DATA MEMBERS CREATED ARE AS FOLLOWED...
|               - public string EdgeType
|               - public string EdgeInfo
|           EACH DATA MEMBER HAS ITS OWN DESCRIPTION IN A COMMENT BESIDE ITS DECLARATION.
|
|       CLONE
|     ---------
|           CREATES A CLONE OF A VERTEX THAT CAN BE USED IN 
|           THE SECOND METHOD FOR A TOPOLOGICAL SORT. IT 
|           STORES THE ORIGINAL VERTICES NAME, IN DEGREE,
|           AND LIST OF EDGES (JUST NAME NOT REFERENCES)
|
|           DATA MEMBERS CREATED ARE AS FOLLOWED...
|               - public T Name
|               - public int InDegree
|               - public List<T> Adjacent
|           EACH DATA MEMBER HAS ITS OWN DESCRIPTION IN A COMMENT BESIDE ITS DECLARATION.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Culin_A1
{
    /*-------------------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |----------------------------------------------- PROGRAM CLASS ------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------------------*/
    class Program
    {
        public int graph_type;                                          // USER SELECTED GRAPH TYPE
        public DirectedGraph<string> s = new DirectedGraph<string>();   // STRING TYPE DIRECTED GRAPH
        public DirectedGraph<char> c = new DirectedGraph<char>();       // CHARACTER TYPE DIRECTED GRAPH
        public DirectedGraph<int> i = new DirectedGraph<int>();         // INTEGER TYPE DIRECTED GRAPH

        /*----------------------------------------------------------- 
        |                                                           |
        |   THE ONLY REASON I HAVE THE MAIN METHOD REDIRECTING TO   |
        |   THE PROMPT USER METHOD IS I HONESTLY FORGOT HOW TO USE  |
        |   NON STATIC METHODS                                      |
        |                                                           |
        |   I HONESTLY DON'T HAVE THE PATIENCE TO RE-LEARN HOW TO   |
        |   RIGHT NOW (OR THE TIME)                                 |
        |                                                           |
        |   THIS WAS ALL DONE IN AN ATTEMPT TO NOT HAVE TO PASS     |
        |   EACH METHOD EACH DIRECTED GRAPH EVEN IF IT WOULDN'T     |
        |   BE USED                                                 |
        |                                                           |
        -----------------------------------------------------------*/
        static void Main()
        {
            Program p = new Program();
            p.PromptUser();
        }// END OF MAIN

        private void PromptUser()
        {
            bool done = false;  // IF TRUE; PROGRAM COMPLETES
            int user_choice;    // USER INPUT --> GRAPH MODIFICATION OPTIONS
            
            try
            {
                // DETERMINE THE TYPE OF GRAPH LABELLING TO USE
                Console.WriteLine("\n..........SELECT A TYPE OF GRAPH..........");
                Console.WriteLine("Strings   --> 1");
                Console.WriteLine("Letters   --> 2");
                Console.Write("Numbers   --> 3\n\n --> ");
                graph_type = Convert.ToInt32(Console.ReadLine());

                // ENSURE VALID INPUT
                if (graph_type < 0 || graph_type > 3)
                {
                    Console.WriteLine("INVALID OPTION...");
                    return;
                }

                // LOOP UNTIL USER CHOOSES TO EXIT
                while (!done)
                {
                    try
                    {
                        // OPTIONS LIST FOR GRAPH MODIFICATION
                        Console.WriteLine("\n.....SELECT AN OPTION.....");
                        Console.WriteLine("Add Vertex       --> 1");
                        Console.WriteLine("Remove Vertex    --> 2");
                        Console.WriteLine("Add Edge         --> 3");
                        Console.WriteLine("Remove Edge      --> 4");
                        Console.WriteLine("Print Graph Data --> 5");
                        Console.WriteLine("Topological Sort --> 6");
                        Console.Write("Finish Graph     --> 7\n\n --> ");
                        user_choice = Convert.ToInt32(Console.ReadLine());

                        // ENSURE VALID INPUT
                        if (user_choice < 0 || user_choice > 7)
                        {
                            Console.WriteLine("INVALID OPTION...");
                            return;
                        }

                        // REDIRECT TO PROPER METHOD BASED ON USER'S CHOICE
                        switch (user_choice)
                        {
                            case 1:
                                AddVertex();
                                break;

                            case 2:
                                RemoveVertex();
                                break;

                            case 3:
                                AddEdge();
                                break;

                            case 4:
                                RemoveEdge();
                                break;

                            case 5:
                                PrintGraph();
                                break;

                            case 6:
                                TopologicalSort();
                                break;

                            // PROGRAM EXITS
                            case 7:
                                done = true;
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\n\nERROR PROCESSING SELECTION\n\n");
                    }
                }

                // ONCE PROGRAM EXITS THE FINAL VERSION 
                // OF THE GRAPH WILL BE PRINTED
                PrintGraph();
            }
            catch(Exception)
            {
                Console.WriteLine("\n\nERROR PROCESSING GRAPH TYPE\n\n");
            }
        }// END OF PROMPT USER


        /*-------------------------------------------------------
        |
        |       Name: AddVertex
        |
        |       Purpose: Add a vertex to a graph
        |
        |       Parameters: 
        |
        --------------------------------------------------------*/
        private void AddVertex()
        {
            bool done = false;          // IF TRUE; RETURN TO MAIN
            char extend;                // USER INPUT --> CONTINUE
            string verticies;           // USER INPUT --> VERTICIES TO ADD
            string[] s_vertex_name;     // ARRAY TO SPLIT VERTICIES INTO
    
            // LOOP UNTIL THE USER DECIDES TO EXIT
            while(!done)
            {
                try
                {
                    // PROMPT USER FOR VERTEX NAMES
                    Console.WriteLine("\n ADDING VERTEX...");
                    Console.Write("Vertex Names (Separated by a space) --> ");
                    verticies = Console.ReadLine();
                    
                    // SPLIT USER INPUT AND PUT IN AN ARRAY OF VERTEX NAMES
                    s_vertex_name = verticies.Split(' ');

                    // DETERMINE WHAT TYPE OF GRAPH TO ADD VERTEX TO
                    switch (graph_type)
                    {
                        // STRING GRAPH
                        case 1:
                            foreach (string element in s_vertex_name)
                                s.AddVertex(element);
                            break;

                        // CHARACTER GRAPH
                        case 2:
                            foreach (string element in s_vertex_name)
                                c.AddVertex(Convert.ToChar(element));
                            break;

                        // INTEGER GRAPH
                        case 3:
                            foreach (string element in s_vertex_name)
                                i.AddVertex(Convert.ToInt32(element));
                            break;

                        default:
                            break;
                    }
                    
                    // ALLOW USER TO CONTINUE ENTERING VERTICIES
                    try
                    {
                        Console.Write("\n CONTINUE? (Y/N) --> ");
                        extend = Convert.ToChar(Console.ReadLine());

                        if (extend == 'y' || extend == 'Y')
                            done = false;
                        else
                            done = true;
                    }
                    catch(Exception)
                    {
                        done = true;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("\n\nERROR ADDING VERTEX\n\n");
                }                
            }

        }// END OF ADD VERTEX


        /*-------------------------------------------------------
        |
        |       Name: RemoveVertex
        |
        |       Purpose: Remove a vertex from a graph
        |
        |       Parameters: - int graph_type
        |                   - DirectedGraph(s)
        |                       - string
        |                       - char
        |                       - int
        |
        --------------------------------------------------------*/
        private void RemoveVertex()
        {
            bool done = false;      // IF TRUE; RETURN TO MAIN
            char extend;            // USER INPUT --> CONTINUE
            string s_vertex_name;   // USER INPUT --> VERTEX TO DELETE

            // LOOP UNTIL THE USER DECIDES TO EXIT
            while(!done)
            {
                try
                {
                    Console.WriteLine("\n REMOVING VERTEX...");
                    Console.Write("Vertex Name --> ");
                    s_vertex_name = Console.ReadLine();

                    // DETERMINE WHAT TYPE OF GRAPH TO REMOVE VERTEX FROM
                    switch (graph_type)
                    {
                        case 1:
                            s.RemoveVertex(s_vertex_name);
                            break;

                        case 2:
                            c.RemoveVertex(Convert.ToChar(s_vertex_name));
                            break;

                        case 3:
                            i.RemoveVertex(Convert.ToInt32(s_vertex_name));
                            break;

                        default:
                            break;
                    }

                    try
                    {
                        Console.Write("\n CONTINUE? (Y/N) --> ");
                        extend = Convert.ToChar(Console.ReadLine());
                        if (extend == 'y' || extend == 'Y')
                            done = false;
                        else
                            done = true;
                    }
                    catch(Exception)
                    {
                        done = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\nERROR REMOVING VERTEX\n\n");
                }
            }

        }//END OF REMOVE VERTEX


        /*-------------------------------------------------------
        |
        |       Name: AddEdge
        |
        |       Purpose: Add an edge to a graph
        |
        |       Parameters: - int graph_type
        |                   - DirectedGraph(s)
        |                       - string
        |                       - char
        |                       - int
        |
        --------------------------------------------------------*/
        private void AddEdge()
        {
            bool done = false;  // IF TRUE; RETURN TO MAIN
            char extend;        // USER INPUT --> CONTINUE
            int cost;           // USER INPUT --> COST OF EDGE
            string verticies;   // USER INPUT --> VERTICIES TO PUT EDGE BETWEEN
            string[] input_arr; // ARRAY TO SPLIT VERTICIES INTO
            
            // LOOP UNTIL THE USER DECIDES TO EXIT
            while(!done)
            {
                try
                {
                    Console.WriteLine("\n ADDING EDGE...");
                    Console.Write(" Enter in following format: Start Finish Cost --> ");
                    verticies = Console.ReadLine();

                    // SPLIT USER INPUT AND PUT IN AN ARRAY
                    input_arr = verticies.Split(' ');
                    cost = Convert.ToInt32(input_arr[2]);

                    // DETERMINE WHAT TYPE OF GRAPH TO ADD EDGE TO
                    switch (graph_type)
                    {
                        case 1:
                            s.AddEdge(input_arr[0], input_arr[1], cost);
                            break;

                        case 2:
                            c.AddEdge(Convert.ToChar(input_arr[0]), Convert.ToChar(input_arr[1]), cost);
                            break;

                        case 3:
                            i.AddEdge(Convert.ToInt32(input_arr[0]), Convert.ToInt32(input_arr[1]), cost);
                            break;

                        default:
                            break;
                    }

                    try
                    {
                        Console.Write("\n CONTINUE? (Y/N) --> ");
                        extend = Convert.ToChar(Console.ReadLine());

                        if (extend == 'y' || extend == 'Y')
                            done = false;
                        else
                            done = true;
                    }
                    catch(Exception)
                    {
                        done = true;
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("\n\nERROR ADDING EDGE\n\n");
                }
            }
                     
        }// END OF ADD EDGE


        /*-------------------------------------------------------
        |
        |       Name: RemoveEdge
        |
        |       Purpose: Remove an edge from a graph
        |
        |       Parameters: - int graph_type
        |                   - DirectedGraph(s)
        |                       - string
        |                       - char
        |                       - int
        |
        --------------------------------------------------------*/
        private void RemoveEdge()
        {
            bool done = false;  // IF TRUE; RETURN TO MAIN
            char extend;        // USER INPUT --> CONTINUE
            string verticies;   // USER INPUT --> VERTICIES OF EDGE TO REMOVE
            string[] input_arr; // ARRAY TO SPLIT VERTICIES INTO

            while (!done)
            {
                try
                {
                    Console.WriteLine("\n REMOVING EDGE...");
                    Console.Write(" Enter in following format: Start Finish --> ");
                    verticies = Console.ReadLine();

                    // SPLIT USER INPUT AND PUT IN AN ARRAY
                    input_arr = verticies.Split(' ');

                    // DETERMINE WHAT TYPE OF GRAPH TO REMOVE EDGE FROM
                    switch (graph_type)
                    {
                        case 1:
                            s.RemoveEdge(input_arr[0], input_arr[1]);
                            break;

                        case 2:
                            c.RemoveEdge(Convert.ToChar(input_arr[0]), Convert.ToChar(input_arr[1]));
                            break;

                        case 3:
                            i.RemoveEdge(Convert.ToInt32(input_arr[0]), Convert.ToInt32(input_arr[1]));
                            break;

                        default:
                            break;
                    }

                    try
                    {
                        Console.Write("\n CONTINUE? (Y/N) --> ");
                        extend = Convert.ToChar(Console.ReadLine());

                        if (extend == 'y' || extend == 'Y')
                            done = false;
                        else
                            done = true;
                    }
                    catch(Exception)
                    {
                        done = true;
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("\n\nERROR REMOVING EDGE\n\n");
                }
               
            }
        }// END OF REMOVE EDGE


        /*-------------------------------------------------------
        |
        |       Name: Print Graph
        |
        |       Purpose: Print the graph information
        |
        |       Parameters: - int graph_type
        |                   - DirectedGraph(s)
        |                       - string
        |                       - char
        |                       - int
        |
        --------------------------------------------------------*/
        private void PrintGraph()
        {
            int j, k;

            switch (graph_type)
            {
                // PRINT THE STRING GRAPH
                case 1:
                    Console.WriteLine("\n----------DEPTH FIRST SEARCH----------");
                    s.DepthFirstSearch();
                    Console.ReadKey();

                    Console.WriteLine("----------BREADTH FIRST SEARCH----------");
                    s.BreadthFirstSearch();
                    Console.ReadKey();

                    Console.WriteLine("\n----------GRAPH INFO----------");
                    for (j = 0; j < s.V.Count; j++)
                    {
                        Console.WriteLine("\nVERTEX " + s.V[j].Name + " INFO\n\t" + s.V[j].VertexInfo);
                        for (k = 0; k < s.V[j].E.Count(); k++)
                            Console.WriteLine("\n\t\t Branch from {0} -> {1}", s.V[j].Name, s.V[j].E[k].EdgeInfo);
                    }
                    break;

                // PRINT THE CHARACTER GRAPH
                case 2:
                    Console.WriteLine("\n----------DEPTH FIRST SEARCH----------");
                    c.DepthFirstSearch();
                    Console.ReadKey();

                    Console.WriteLine("----------BREADTH FIRST SEARCH----------");
                    c.BreadthFirstSearch();
                    Console.ReadKey();

                    Console.WriteLine("\n----------GRAPH INFO----------");
                    for (j = 0; j < c.V.Count; j++)
                    {
                        Console.WriteLine("\nVERTEX " + c.V[j].Name + " INFO\n\t" + c.V[j].VertexInfo);
                        for (k = 0; k < c.V[j].E.Count(); k++)
                            Console.WriteLine("\n\t\t Branch from {0} -> {1}", c.V[j].Name, c.V[j].E[k].EdgeInfo);
                    }
                    break;

                //PRIN THE INTEGER GRAPH
                case 3:
                    Console.WriteLine("\n----------DEPTH FIRST SEARCH----------");
                    i.DepthFirstSearch();
                    Console.ReadKey();

                    Console.WriteLine("----------BREADTH FIRST SEARCH----------");
                    i.BreadthFirstSearch();
                    Console.ReadKey();

                    Console.WriteLine("\n----------GRAPH INFO----------");
                    for (j = 0; j < i.V.Count; j++)
                    {
                        Console.WriteLine("\nVERTEX " + i.V[j].Name + " INFO\n\t" + i.V[j].VertexInfo);
                        for (k = 0; k < i.V[j].E.Count(); k++)
                            Console.WriteLine("\t\t Branch from {0} -> {1}", i.V[j].Name, i.V[j].E[k].EdgeInfo);
                    }
                    break;
            }
        }// END OF PRINT GRAPH


        /*-------------------------------------------------------
        |
        |       Name: Topological Sort
        |
        |       Purpose: Sort graph topologically if it is 
        |                acyclic
        |
        |       Parameters: - int graph_type
        |                   - DirectedGraph(s)
        |                       - string
        |                       - char
        |                       - int
        |
        --------------------------------------------------------*/
        private void TopologicalSort()
        {
            switch (graph_type)
            {
                // PRINT THE STRING GRAPH
                case 1:
                    if (s.SortTopological() < 0)
                        Console.WriteLine("\nGRAPH CANNOT BE SORTED TOPOLOGICALLY... NOT ACYCLIC");
                    else
                        Console.WriteLine("\nGRAPH SORTED TOPOLOGICALLY");
                    Console.ReadKey();
                    break;

                // PRINT THE CHARACTER GRAPH
                case 2:
                    if (c.SortTopological() < 0)
                        Console.WriteLine("\nGRAPH CANNOT BE SORTED TOPOLOGICALLY... NOT ACYCLIC");
                    else
                        Console.WriteLine("\nGRAPH SORTED TOPOLOGICALLY");
                    Console.ReadKey();
                    break;

                //PRIN THE INTEGER GRAPH
                case 3:
                    if (i.SortTopological() < 0)
                        Console.WriteLine("\nGRAPH CANNOT BE SORTED TOPOLOGICALLY... NOT ACYCLIC");
                    else
                        Console.WriteLine("\nGRAPH SORTED TOPOLOGICALLY");
                    Console.ReadKey();
                    break;
            }
        }// END OF TOPOLOGICAL SORT

    }// END OF PROGRAM CLASS




    /*-------------------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |------------------------------------------- IDIRECTED GRAPH CLASS --------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------------------*/
    public interface IDirectedGraph<T>
    {
        void AddVertex(T name);

        void RemoveVertex(T name);

        void AddEdge(T name1, T name2, int cost);

        void RemoveEdge(T name1, T name2);

    }// END OF I DIRECTED GRAPH <T> CLASS




    /*-------------------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------- DIRECTED GRAPH CLASS --------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------
    |                                                                                   |
    |                               ADDITIONS                                           |
    |   Global Declarations                                                             |
    |       - int timer = 0;                                                            |
    |       - bool cycle = false;                                                       |
    |       - List<T> topSort;                                                          |
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
    |       - Sets discovery time and finishing time based of timer incrementor         |
    |       - Updates branch type based on traversal                                    |
    |         (Sets cycle boolean to true if there is a back edge)                      |
    |       - Adds vertex to topSort list once it has completed                         |
    |                                                                                   |
    |   SortTopological()                                                               |
    |       - Topologically sort the graph 2 ways if there are no cycles                |
    |       - 1) Print vertices in reverse order of finishing time                      |
    |       - 2) Recursively delete vertex with InDegree 0 and decrease InDegree of     |
    |            adjacent vertices                                                      |
    |            (Creates a cloned copy of the vertex list to modify)                   |
    |                                                                                   |
    |   InDegreeTopological(List<Clone<T>>)                                             |
    |       - Prints and deletes verticies from method 2 for topological sorting        |
    |                                                                                   |
    |   SortByIndegree(List<Clone<T>>)                                                  |
    |       - Recursively sort the vertices by indegree first then edge count           |
    |           - Vertices with lesser InDegree & higher edge count at front            |
    |           - Vertices with higher InDegree & lesser edge count at end              |
    |                                                                                   |
    |----------------------------------------------------------------------------------*/
    class DirectedGraph<T> : IDirectedGraph<T>
    {
        public List<Vertex<T>> V;
        public List<T> topSort;     // LIST OF VERTICIES IN ORDER OF FINISHING TIME

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
            topSort = new List<T>();
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
                    // THIS WILL ENSURE THE IN DEGREE FOR EACH ADJACENT
                    // WILL DECREASE
                    if (V[j].Name.Equals(name))
                    {
                        for (k = 0; k < V[j].E.Count();)
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
        |       Purpose: Adds an edge from two vertices 
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
        |       Purpose: Removes the edge between two vertices
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

            topSort.Clear();

            // RESET ALL STATS FOR EACH VERTEX
            foreach (Vertex<T> vertex in V)
            {
                vertex.Visited = false;
                vertex.Colour = "WHITE";
                vertex.DiscoveryTime = 0;
                vertex.FinishingTime = 0;

                foreach (Edge<T> edge in vertex.E)
                    edge.EdgeType = "";
            }

            for (i = 0; i < V.Count; i++)
            {
                for (j = 0; j < V.Count; j++)
                {
                    // START DEPTH FIRST WITH A VERTEX 
                    // WITH AN IN DEGREE OF 0 IF POSSIBLE
                    if (V[j].InDegree.Equals(0) && !V[j].Visited)
                    {
                        DepthFirstSearch(V[j]);
                        Console.WriteLine();
                    }


                    else if (j.Equals(V.Count() - 1))
                    {
                        if (!V[j].Visited)          // RESET WITH VERTEX J
                        {
                            DepthFirstSearch(V[0]);
                            Console.WriteLine();
                        }
                    }
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

            timer++;                            // INCREMENT TIMER (FINISHING TIME)
            v.Colour = "BLACK";                 // SETS COLOUR OF THE VERTEX TO BLACK ONCE COMPLETED     
            v.FinishingTime = timer;            // SETS THE FINISHING TIME 

            topSort.Add(v.Name);
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
            int i, j;

            for (i = 0; i < V.Count; i++)
                V[i].Visited = false;              // SET ALL VERTICES TO UNVISiTED

            for (i = 0; i < V.Count; i++)
            {
                for (j = 0; j < V.Count; j++)
                {
                    // START DEPTH FIRST WITH 
                    if (V[j].InDegree.Equals(0))
                    {
                        if (!V[j].Visited)          // RESET WITH VERTEX I
                        {
                            BreadthFirstSearch(V[j]);
                            Console.WriteLine();
                        }
                    }

                    else if (j.Equals(V.Count() - 1))
                    {
                        if (!V[j].Visited)          // RESET WITH VERTEX I
                        {
                            BreadthFirstSearch(V[j]);
                            Console.WriteLine();
                        }
                    }
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

                for (j = 0; j < v.E.Count; j++)             // ENQUEUE UNIVISITED ADJACENT VERTICES
                {
                    w = v.E[j].AdjVertex;
                    if (!w.Visited)
                    {
                        w.Visited = true;                   // MARK VERTEX AS VISITED
                        Q.Enqueue(w);
                    }
                }
            }
        }// END OF BREADTH FIRST SEARCH


        /*-------------------------------------------------------
        |
        |       Name: PrintVertices
        |
        |       Purpose: Prints out all vertices of a graph
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
        }// END OF PRINT VERTICES


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
            // COPY VERTICES INTO NEW LIST OF TYPE CLONE
            List<Clone<T>> inDegreeSort = new List<Clone<T>>();
            foreach (Vertex<T> vertex in V)
                inDegreeSort.Add(new Clone<T>(vertex));

            topSort.Clear();

            // MAKE SURE GRAPH IS ACYCLICAL
            DepthFirstSearch();

            // GRAPH IS ACYCLIC
            if (!cycle)
            {
                // SORTED USING THE FIRST METHOD
                Console.WriteLine("\n\tTOPOLOGICAL SORTING (METHOD 1)->");

                // PRINT THE ORDER THE VERTICES FINSHED FROM MOST RECENT TO OLDEST 
                topSort.Reverse();
                foreach (T vertex in topSort)
                    Console.WriteLine(" " + vertex);
                Console.ReadLine();

                // SORTED USING THE SECOND METHOD
                Console.WriteLine("\n\tTOPOLOGICAL SORTING (METHOD 2)->");
                InDegreeTopological(inDegreeSort);
                Console.ReadLine();

                return (1);
            }

            // GRAPH IS NOT ACYCLIC
            else
                return (-1);

        }// END OF SORT TOPOLOGICAL


        /*-------------------------------------------------------
        |
        |       Name: InDegreeTopological
        |
        |       Purpose: Delete vertices once in degree reaches 0
        |
        |       Parameters: 
        |
        --------------------------------------------------------*/
        private void InDegreeTopological(List<Clone<T>> inDegreeSort)
        {
            int i, j;

            // RECURSIVELY SORT ARRAY 
            // (ONLY IF A ZERO IS NOT ALREADY IN THE FIRST POSITION)
            while (inDegreeSort.Count() > 0)
            {
                SortByInDegree(inDegreeSort);

                // REMOVE THE FIRST VERTEX IN THE ARRAY
                // WILL ALSO DECREASE IN DEGREE OF ALL
                // ADJACENT VERTICES
                Console.WriteLine(" {0}", inDegreeSort[0].Name);

                // DECREASE IN DEGREE COUNTER FOR EACH ADJACENT VERTEX
                for (i = 0; i < inDegreeSort[0].Adjacent.Count(); i++)
                {
                    for (j = 0; j < inDegreeSort.Count(); j++)
                    {
                        // DECREASES IN DEGREE OF V LIST AS WELL
                        if (inDegreeSort[j].Name.Equals(inDegreeSort[0].Adjacent[i]))
                            inDegreeSort[j].InDegree--;
                    }
                }

                // REMOVE FIRST 
                inDegreeSort.RemoveAt(0);
            }

        }// END OF IN DEGREE TOPOLOGICAL SORT


        /*-------------------------------------------------------
        |
        |       Name: SortByInDegree
        |
        |       Purpose: Sort a list of vertices by its
        |                in degree and return if there is a
        |                vertex with an in degree of 0 in the
        |                first position
        |
        |       Parameters: - List<Vertex<T>> sort
        |
        --------------------------------------------------------*/
        private List<Clone<T>> SortByInDegree(List<Clone<T>> sort)
        {
            int i, j, k;
            int zeroLoc = 0;

            Clone<T> temp;

            // WORST CASE MOVES END ELEMENT TO FRONT (N - 1 ITERATIONS)
            for (i = 1; i < sort.Count(); i++)
            {
                for (j = 0; j < sort.Count(); j++)
                {
                    for (k = j + 1; k < sort.Count();)
                    {
                        // IF THERE IS A SAVED LOCATION FOR A ZERO IN DEGREE
                        // JUMP TO ITS LOCATION TO SWAP
                        if (zeroLoc > 0)
                        {
                            j = zeroLoc - 1;
                            k = zeroLoc;
                        }

                        // FIRST INDEX HAS IN DEGREE = 0
                        if (sort[0].InDegree.Equals(0))
                            return sort;

                        // INDEX K HAS IN DEGREE = 0
                        else if (sort[k].InDegree.Equals(0))
                        {
                            // SWAP POSITIONS
                            // RETURN IF IN DEGREE OF 0 IN FIRST POS
                            zeroLoc = j;

                            // SWAP POSITIONS
                            temp = sort[j];
                            sort[j] = sort[k];
                            sort[k] = temp;

                            if (zeroLoc.Equals(0))
                                return sort;

                            else
                            {
                                // IF ON FIRST RECURSIVE SORT 
                                // SORT REST OF LIST
                                if (i.Equals(0))
                                    break;

                                // LIST BEFORE AND AFTER ZEROLOC HAS BEEN SORTED ALREADY
                                // INCREMENT I; RESTART SORT 
                                // WILL ASSIGN J TO ZEROLOC-1 AND K TO ZEROLOC UPON RESTART
                                else
                                {
                                    j = sort.Count();
                                    break;
                                }
                            }

                        }

                        // J->IN DEGREE > K->IN DEGREE && J->EDGE COUNT < K->EDGE COUNT; SWAP
                        else if ((sort[j].InDegree > sort[k].InDegree) && (sort[j].Adjacent.Count() < sort[k].Adjacent.Count()))
                        {
                            // SWAP POSITIONS
                            temp = sort[j];
                            sort[j] = sort[k];
                            sort[k] = temp;

                            break;
                        }

                        // J->EDGE COUNT < K->EDGE COUNT; SWAP
                        else if (sort[j].Adjacent.Count() < sort[k].Adjacent.Count())
                        {
                            // SWAP POSITIONS
                            temp = sort[j];
                            sort[j] = sort[k];
                            sort[k] = temp;

                            break;
                        }

                        break;

                    }// END OF FOR K
                }// END OF FOR J
            }// END OF FOR I

            return sort;
        }// END OF SORT BY IN DEGREE

    }// END OF DIRECTED GRAPH <T> CLASS




    /*-------------------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |----------------------------------------------- VERTEX CLASS -------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------------------*/

    /*----------------------------------
    |                                   |            
    |             ADDITIONS             |            
    |                                   |           
    |   public string Colour { }        |        
    |   public int DiscoveryTime { }    |        
    |   public int FinishingTime { }    |            
    |   public int InDegree { }         |
    |   public string VertexInfo { }    |
    |                                   |            
    |   public Vertex(name)             |            
    |       - Colour = "White";         |        
    |                                   |           
    |----------------------------------*/
    public class Vertex<T>
    {
        public T Name { get; set; }             // VERTEX NAME

        public bool Visited { get; set; }       // VISITED OR UNVISITED

        public List<Edge<T>> E { get; set; }    // LIST OF ADJACENT VERTICIES

        public string Colour { get; set; }      // DETERMINE WHAT COLOUR THE VERTEX IS

        public int DiscoveryTime { get; set; }  // DISCOVERY TIME OF THE VERTEX (DFS)

        public int FinishingTime { get; set; }  // FINISHING TIME OF THE VERTEX (DFS)

        public int InDegree { get; set; }       // NUMBER OF VERTEICIES THAT HAVE A REFERENCE TO THE VERTEX

        public string VertexInfo
        {
            get
            {
                return $"{Name}, {Visited}, {Colour}, {DiscoveryTime}, {FinishingTime}, {InDegree}";
            }
        }

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
            Name = name;                // SETS NAME OF THE VERTEX
            Visited = false;            // SETS VISTED OF THE VERTEX TO FALSE
            E = new List<Edge<T>>();    // CREATES A LIST OF EDGES
            Colour = "WHITE";           // SETS THE COLOUR TO WHITE
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

            for (i = 0; i < E.Count; i++)
            {
                if (E[i].AdjVertex.Name.Equals(name))
                    return i;
            }

            return -1;
        }// END OF FIND EDGE

    }// END OF VERTEX <T> CLASS




    /*-------------------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |------------------------------------------------- EDGE CLASS -------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------------------*/

    /*----------------------------------
    |                                   |            
    |             ADDITIONS             |            
    |                                   |           
    |   public string EdgeType { }      |
    |   public string EdgeInfo { }      |
    |                                   |
    |----------------------------------*/
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




    /*-------------------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |------------------------------------------------ CLONE CLASS -------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------------------*/
    class Clone<T>
    {
        public T Name { get; set; }             // NAME OF THE VERTEX

        public int InDegree { get; set; }       // IN DEGREE OF THE VERTEX

        public List<T> Adjacent { get; set; }   // LIST OF ALL ADJACENT VERTEXES (NAMES NOT REFERENCES)


        /*-------------------------------------------------------
        |
        |       Name: clone
        |
        |       Purpose: Constructor for the Clone class
        |
        |       Parameters: - Vertex<T> og
        |
        --------------------------------------------------------*/
        public Clone(Vertex<T> og)
        {
            // ASSIGN THE NAME OF THE VERTEX
            // ASSIGN THE IN DEGREE OF THE VERTEX
            // CREATE THE LIST OF ADJACENT VERTICES
            Name = og.Name;
            InDegree = og.InDegree;
            Adjacent = new List<T>();

            // COPY THE NAME OF THE ADJACNENT VERTEX NOT THE REFERENCE
            foreach (Edge<T> edge in og.E)
                Adjacent.Add(edge.AdjVertex.Name);
        }

    }// END OF CLONE <T> CLASS




}
    /*-------------------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |---------------------------------------------- END OF NAMESPACE ----------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------------------*/