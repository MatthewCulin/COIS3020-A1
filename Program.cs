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
            int j, k;
            
            try
            {
                Console.WriteLine("\n SELECT A TYPE OF GRAPH...");
                Console.WriteLine("Locations (String) --> 1");
                Console.WriteLine("Labels (Letters)   --> 2");
                Console.Write("Labels (Numbers)   --> 3\n --> ");
                graph_type = Convert.ToInt32(Console.ReadLine());

                if (graph_type < 0 || graph_type > 3)
                {
                    Console.WriteLine("INVALID OPTION...");
                    return;
                }

                while (!done)
                {
                    try
                    {
                        Console.WriteLine("\n SELECT AN OPTION...");
                        Console.WriteLine("Add Vertex    --> 1");
                        Console.WriteLine("Remove Vertex --> 2");
                        Console.WriteLine("Add Edge      --> 3");
                        Console.WriteLine("Remove Edge   --> 4");
                        Console.Write("Finish Graph  --> 5\n --> ");
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
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n\nERROR PROCESSING SELECTION\n\n");
                    }

                }

                switch (graph_type)
                {
                    case 1:
                        Console.WriteLine("\nDEPTH FIRST SEARCH");
                        s.DepthFirstSearch();
                        Console.ReadKey();

                        Console.WriteLine("BREADTH FIRST SEARCH");
                        s.BreadthFirstSearch();
                        Console.ReadKey();

                        Console.WriteLine("\nVERTEX INFO");
                        for (j = 0; j < s.V.Count; j++)
                        {
                            Console.WriteLine(s.V[j].VertexInfo);
                            Console.WriteLine("\tEDGE INFO");
                            for (k = 0; k < s.V[j].E.Count(); k++)
                                Console.WriteLine(s.V[j].E[k].EdgeType);
                        }
                        break;

                    case 2:
                        Console.WriteLine("\nDEPTH FIRST SEARCH");
                        c.DepthFirstSearch();
                        Console.ReadKey();

                        Console.WriteLine("BREADTH FIRST SEARCH");
                        c.BreadthFirstSearch();
                        Console.ReadKey();

                        Console.WriteLine("\nVERTEX INFO");
                        for (j = 0; j < c.V.Count; j++)
                        {
                            Console.WriteLine(c.V[j].VertexInfo);
                            Console.WriteLine("\tEDGE INFO");
                            for (k = 0; k < c.V[j].E.Count(); k++)
                                Console.WriteLine(c.V[j].E[k].EdgeType);
                        }
                        break;

                    case 3:
                        Console.WriteLine("\nDEPTH FIRST SEARCH");
                        i.DepthFirstSearch();
                        Console.ReadKey();

                        Console.WriteLine("BREADTH FIRST SEARCH");
                        i.BreadthFirstSearch();
                        Console.ReadKey();

                        Console.WriteLine("\nVERTEX INFO");
                        for (j = 0; j < i.V.Count; j++)
                        {
                            Console.WriteLine(i.V[j].VertexInfo);
                            Console.WriteLine("\tEDGE INFO");
                            for (k = 0; k < i.V[j].E.Count(); j++)
                                Console.WriteLine(i.V[j].E[k].EdgeType);
                        }
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("\n\nERROR PROCESSING SELECTION\n\n");
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
            bool done = false;
            char extend;
            int j;
            string input;
            string[] s_vertex_name;
            
            while(!done)
            {
                try
                {
                    Console.WriteLine("\n ADDING VERTEX...");
                    Console.Write("Vertex Names (Separated by a space) --> ");
                    input = Console.ReadLine();
                    s_vertex_name = input.Split(' ');

                    char[] c_vertex_name = new char[s_vertex_name.Count()];
                    int[] i_vertex_name = new int[s_vertex_name.Count()];

                    // DETERMINE WHAT TYPE OF GRAPH TO ADD VERTEX TO
                    switch (graph_type)
                    {
                        case 1:
                            foreach (string element in s_vertex_name)
                                s.AddVertex(element);
                            break;

                        case 2:
                            for (j = 0; j < s_vertex_name.Count(); j++)
                                c_vertex_name[j] = Convert.ToChar(s_vertex_name[j]);
                            foreach (char element in c_vertex_name)
                                c.AddVertex(element);
                            break;

                        case 3:
                            for (j = 0; j < s_vertex_name.Count(); j++)
                                i_vertex_name[j] = Convert.ToChar(s_vertex_name[j]);
                            foreach (char element in i_vertex_name)
                                i.AddVertex(element);
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
                    catch(Exception ex)
                    {
                        done = true;
                    }

                }
                catch (Exception e)
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
        static void RemoveVertex(int graph_type, DirectedGraph<string> s, DirectedGraph<char> c, DirectedGraph<int> i)
        {
            bool done = false;
            char extend;
            string s_vertex_name;

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
                        Console.Write("\n CONTINUE? (Y/N) -->");
                        extend = Convert.ToChar(Console.ReadLine());
                        if (extend == 'y' || extend == 'Y')
                            done = false;
                        else
                            done = true;
                    }
                    catch(Exception e)
                    {
                        done = true;
                    }
                }
                catch (Exception e)
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
        static void AddEdge(int graph_type, DirectedGraph<string> s, DirectedGraph<char> c, DirectedGraph<int> i)
        {
            bool done = false;
            char extend;
            int cost;
            string inputs;
            string[] input_arr;

            while(!done)
            {
                try
                {
                    Console.WriteLine("\n ADDING EDGE...");
                    Console.Write(" Enter in following format: Start Finish Cost --> ");
                    inputs = Console.ReadLine();
                    input_arr = inputs.Split(' ');
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
                    catch(Exception ex)
                    {
                        done = true;
                    }
                }
                catch(Exception e)
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
        static void RemoveEdge(int graph_type, DirectedGraph<string> s, DirectedGraph<char> c, DirectedGraph<int> i)
        {
            bool done = false;
            char extend;
            string inputs;
            string[] input_arr;

            while (!done)
            {
                try
                {
                    Console.WriteLine("\n REMOVING EDGE...");
                    Console.Write(" Enter in following format: Start Finish --> ");
                    inputs = Console.ReadLine();
                    input_arr = inputs.Split(' ');

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
                    catch(Exception ex)
                    {
                        done = true;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("\n\nERROR REMOVING EDGE\n\n");
                }
               
            }
        }// END OF REMOVE EDGE

    }
}
