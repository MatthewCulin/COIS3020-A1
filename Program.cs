using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culin_A1
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectedGraph<string> s = new DirectedGraph<string>();
            DirectedGraph<char> c = new DirectedGraph<char>();
            DirectedGraph<int> i = new DirectedGraph<int>();

            bool done = false;

            int graph_type;
            int user_choice;

            Console.WriteLine("\n SELECT A TYPE OF GRAPH...");
            Console.WriteLine("Locations (String) --> 1");
            Console.WriteLine("Labels (Letters)   --> 2");
            Console.WriteLine("Labels (Numbers)   --> 3");
            graph_type = Convert.ToInt32(Console.ReadLine());

            if(graph_type < 0 || graph_type > 3)
            {
                Console.WriteLine("INVALID OPTION...");
                return;
            }

            while (!done)
            {
                Console.WriteLine("\n SELECT AN OPTION...");
                Console.WriteLine("Add Vertex    --> 1");
                Console.WriteLine("Remove Vertex --> 2");
                Console.WriteLine("Add Edge      --> 3");
                Console.WriteLine("Remove Edge   --> 4");
                Console.WriteLine("Finish Graph  --> 5");
                user_choice = Convert.ToInt32(Console.ReadLine());

                if (user_choice < 0 || user_choice > 5)
                {
                    Console.WriteLine("INVALID OPTION...");
                    return;
                }

                switch (user_choice)
                {
                    case 1:
                        AddVertex(graph_type, s, c, i);
                        break;

                    case 2:
                        RemoveVertex(graph_type, s, c, i);
                        break;

                    case 3:
                        AddEdge(graph_type, s, c, i);
                        break;

                    case 4:
                        RemoveEdge(graph_type, s, c, i);
                        break;

                    case 5:
                        done = true;
                        break;
                }
            }

            switch(graph_type)
            {
                case 1:
                    s.PrintEdges();
                    s.PrintVertices();
                    Console.ReadKey();

                    s.DepthFirstSearch();
                    Console.ReadKey();

                    s.BreadthFirstSearch();
                    Console.ReadKey();
                    break;
                
                case 2:
                    c.PrintEdges();
                    c.PrintVertices();
                    Console.ReadKey();

                    c.DepthFirstSearch();
                    Console.ReadKey();

                    c.BreadthFirstSearch();
                    Console.ReadKey();
                    break;

                case 3:
                    i.PrintEdges();
                    i.PrintVertices();
                    Console.ReadKey();

                    i.DepthFirstSearch();
                    Console.ReadKey();

                    i.BreadthFirstSearch();
                    Console.ReadKey();
                    break;
            }

        }// END OF MAIN


        /*-------------------------------------------------------
        |
        |       Name: AddVertex
        |
        |       Purpose: Add a vertex to a graph
        |
        |       Parameters: - int graph_type
        |                   - DirectedGraph(s)
        |                       - string
        |                       - char
        |                       - int
        |
        --------------------------------------------------------*/
        static void AddVertex(int graph_type, DirectedGraph<string> s, DirectedGraph<char> c, DirectedGraph<int> i)
        {
            string s_vertex_name;
            char c_vertex_name;
            int i_vertex_name;
            
            Console.WriteLine("ADDING VERTEX...");
            Console.WriteLine("Vertex Name ->");
            s_vertex_name = Console.ReadLine();
            
            // DETERMINE WHAT TYPE OF GRAPH TO ADD VERTEX TO
            switch (graph_type)
            {
                case 1:
                    s.AddVertex(s_vertex_name);
                    break;

                case 2:
                    c_vertex_name = Convert.ToChar(s_vertex_name);
                    c.AddVertex(c_vertex_name);
                    break;

                case 3:
                    i_vertex_name = Convert.ToInt32(s_vertex_name);
                    i.AddVertex(i_vertex_name);
                    break;

                default:
                    break;
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
        static void RemoveVertex(int graph_type, DirectedGraph<string> s, DirectedGraph<char> c, DirectedGraph<int> i)
        {
            string s_vertex_name;
            char c_vertex_name;
            int i_vertex_name;

            Console.WriteLine("REMOVING VERTEX...");
            Console.WriteLine("Vertex Name ->");
            s_vertex_name = Console.ReadLine();

            // DETERMINE WHAT TYPE OF GRAPH TO REMOVE VERTEX FROM
            switch(graph_type)
            {
                case 1:
                    s.RemoveVertex(s_vertex_name);
                    break;

                case 2:
                    c_vertex_name = Convert.ToChar(s_vertex_name);
                    c.RemoveVertex(c_vertex_name);
                    break;

                case 3:
                    i_vertex_name = Convert.ToInt32(s_vertex_name);
                    i.AddVertex(i_vertex_name);
                    break;

                default:
                    break;
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
        static void AddEdge(int graph_type, DirectedGraph<string> s, DirectedGraph<char> c, DirectedGraph<int> i)
        {
            int cost;

            string s_start_name;
            string s_end_name;

            char c_start_name;
            char c_end_name;

            int i_start_name;
            int i_end_name;

            Console.WriteLine("ADDING EDGE...");
            Console.WriteLine("Starting Vertex ->");
            s_start_name = Console.ReadLine();

            Console.WriteLine("Ending Vertex ->");
            s_end_name = Console.ReadLine();

            Console.WriteLine("Cost ->");
            cost = Convert.ToInt32(Console.ReadLine());

            // DETERMINE WHAT TYPE OF GRAPH TO ADD EDGE TO
            switch (graph_type)
            {
                case 1:
                    s.AddEdge(s_start_name, s_end_name, cost);
                    break;

                case 2:
                    c_start_name = Convert.ToChar(s_start_name);
                    c_end_name = Convert.ToChar(s_end_name);
                    c.AddEdge(c_start_name, c_end_name, cost);
                    break;

                case 3:
                    i_start_name = Convert.ToChar(s_start_name);
                    i_end_name = Convert.ToChar(s_end_name);
                    i.AddEdge(i_start_name, i_end_name, cost);
                    break;

                default:
                    break;
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
        static void RemoveEdge(int graph_type, DirectedGraph<string> s, DirectedGraph<char> c, DirectedGraph<int> i)
        {
            string s_start_name;
            string s_end_name;

            char c_start_name;
            char c_end_name;

            int i_start_name;
            int i_end_name;

            Console.WriteLine("REMOVING EDGE...");
            Console.WriteLine("Starting Vertex ->");
            s_start_name = Console.ReadLine();

            Console.WriteLine("Ending Vertex ->");
            s_end_name = Console.ReadLine();

            // DETERMINE WHAT TYPE OF GRAPH TO REMOVE EDGE FROM
            switch (graph_type)
            {
                case 1:
                    s.RemoveEdge(s_start_name, s_end_name);
                    break;

                case 2:
                    c_start_name = Convert.ToChar(s_start_name);
                    c_end_name = Convert.ToChar(s_end_name);
                    c.RemoveEdge(c_start_name, c_end_name);
                    break;

                case 3:
                    i_start_name = Convert.ToChar(s_start_name);
                    i_end_name = Convert.ToChar(s_end_name);
                    i.RemoveEdge(i_start_name, i_end_name);
                    break;

                default:
                    break;
            }

        }// END OF REMOVE EDGE

    }
}
