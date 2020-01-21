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
            int i, j;

            Console.WriteLine("Adjacency Matrix Implementation");

            DirectedGraph<char> H = new DirectedGraph<char>();

            for (i = 0; i < 7; i++)
            {
                H.AddVertex((char)(i + 'a'));
            }


            H.PrintVertices();

            for (i = 0; i < 7; i += 2)
            {
                for (j = 0; j < 7; j += 3)
                {
                    H.AddEdge((char)(i + 'a'), (char)(j + 'a'), 10);
                }
            }

            H.PrintEdges();
            Console.ReadKey();

            H.RemoveVertex('c');
            H.RemoveVertex('f');

            H.PrintVertices();
            H.PrintEdges();
            Console.ReadKey();

            DirectedGraph<int> G = new DirectedGraph<int>();

            for (i = 0; i < 7; i++)
            { 
                G.AddVertex(i); 
            }

            G.PrintVertices();

            G.AddEdge(0, 1, 0);
            G.AddEdge(1, 3, 0);
            G.AddEdge(1, 4, 0);
            G.AddEdge(3, 2, 0);
            G.AddEdge(4, 5, 0);
            G.AddEdge(2, 5, 0);
            G.AddEdge(5, 6, 0);

            G.PrintEdges();
            Console.ReadKey();

            G.DepthFirstSearch();
            Console.ReadKey();

            G.BreadthFirstSearch();
            Console.ReadKey();
        }
    }
}
