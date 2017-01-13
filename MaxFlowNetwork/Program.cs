using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFlowNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxFlow = new MaxFlowNetwork();
            //TestBiGraph();

            //Console.WriteLine(maxFlow.isBipartiale(CorrectBiGraph(), 1));
            Console.WriteLine(maxFlow.Calculate(MatrixFromExample(), 0, 6));
          //  Console.WriteLine(maxFlow.Calculate(NoPathMatrix(), 0, 4));
            Console.ReadKey();
        }


        private static void TestBiGraph()
        {
            var maxFlow = new MaxFlowNetwork();
            BigraphResult foo =  maxFlow.isBipartiale(CorrectBiGraph(), 1);
            if (foo.IsBigraph)
            {
                maxFlow.CalculateBigraphAssociation(CorrectBiGraph(), 1, foo);
            }
        }

        private static AdjacencyMatrix SimpleMatrix()
        {
            var matrix = new AdjacencyMatrix(3);
            matrix.AddEdge(1, 2, 7f);

            matrix.AddEdge(2, 3, 100f);

            return matrix;
        }

        //Przykład z http://eduinf.waw.pl/inf/alg/001_search/0146.php#P2
        private static AdjacencyMatrix MatrixFromExample()
        {
            var matrix = new AdjacencyMatrix(7);
            matrix.AddEdge(1, 2, 9f);

            matrix.AddEdge(1, 5, 9f);
            matrix.AddEdge(2, 3, 7f);
            matrix.AddEdge(2, 4, 3f);
            matrix.AddEdge(3, 4, 4f);
            matrix.AddEdge(3, 7, 6f);
            matrix.AddEdge(4, 7, 9f);
            matrix.AddEdge(4, 6, 2f);
            matrix.AddEdge(5, 4, 3f);
            matrix.AddEdge(5, 6, 6f);
            matrix.AddEdge(6, 7, 8f);

            return matrix;
        }

        private static AdjacencyMatrix CorrectBiGraph()
        {
            var matrix = new AdjacencyMatrix(12);
            matrix.AddEdge(2, 7, 1f);
            matrix.AddEdge(2, 9, 1f);
            matrix.AddEdge(2, 11, 1f);

            matrix.AddEdge(3, 8, 1f);
            matrix.AddEdge(3, 10, 1f);

            matrix.AddEdge(4, 7, 1f);
            matrix.AddEdge(4, 8, 1f);
            matrix.AddEdge(4, 9, 1f);
            matrix.AddEdge(4, 10, 1f);
            matrix.AddEdge(4, 11, 1f);

            matrix.AddEdge(5, 9, 1f);
            matrix.AddEdge(5, 10, 1f);

            matrix.AddEdge(6, 8, 1f);
            matrix.AddEdge(6, 10, 1f);

            return matrix;
        }

        private static AdjacencyMatrix NoPathMatrix()
        {
            var matrix = new AdjacencyMatrix(5);
            matrix.AddEdge(1, 2, 5f);

            matrix.AddEdge(2, 3, 7f);

            matrix.AddEdge(3, 2, 2f);

            matrix.AddEdge(4, 5, 15f);

            return matrix;
        }
    }
}
