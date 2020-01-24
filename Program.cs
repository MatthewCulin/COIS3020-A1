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
|           description
|
|           METHODS CREATED ARE AS FOLLOWED...
|               - AddVertex( )
|               - RemoveVertex ( )
|               - AddEdge ( )
|               - RemoveEdge ( )
|           EACH METHOD HAS ITS OWN DESCRIPTION IN A HEADER ABOVE ITS DECLARATION.
|
|       VERTEX
|     ----------
|           description
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
|           description
|
|           DATA MEMBERS CREATED ARE AS FOLLOWED...
|               - public string EdgeType
|               - public string EdgeInfo
|           EACH DATA MEMBER HAS ITS OWN DESCRIPTION IN A COMMENT BESIDE ITS DECLARATION.
|
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        static void Main(string[] args)
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
                Console.WriteLine("\n SELECT A TYPE OF GRAPH...");
                Console.WriteLine("Locations (String) --> 1");
                Console.WriteLine("Labels (Letters)   --> 2");
                Console.Write("Labels (Numbers)   --> 3\n\n --> ");
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
                        Console.WriteLine("\n SELECT AN OPTION...");
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
                Console.WriteLine("\n\nERROR PROCESSING SELECTION\n\n");
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
                            i.AddVertex(Convert.ToInt32(s_vertex_name));
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
        }// END OF PRINT GRAPH

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
    |                                     ADDITIONS                                     |
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
    // CLASS HERE




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




}
    /*-------------------------------------------------------------------------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |---------------------------------------------- END OF NAMESPACE ----------------------------------------------|
    |--------------------------------------------------------------------------------------------------------------|
    |-------------------------------------------------------------------------------------------------------------*/
