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
            TestBiGraph();

            //Console.WriteLine(maxFlow.isBipartiale(CorrectBiGraph(), 0).IsBigraph);
            //Console.WriteLine(maxFlow.Calculate(NoPathMatrix(), 0, 2));
          //  Console.WriteLine(maxFlow.Calculate(NoPathMatrix(), 0, 4));
            Console.ReadKey();
        }


        private static void TestBiGraph()
        {
            var maxFlow = new MaxFlowNetwork();
            BigraphResult foo =  maxFlow.isBipartiale(Cycled6Matrix(), 0);
            if (foo.IsBigraph)
            {
                maxFlow.CalculateBigraphAssociation(Cycled6Matrix(), 0, foo);
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

        private static AdjacencyMatrix Cycled6Matrix()
        {
            var matrix = new AdjacencyMatrix(6);
            matrix.AddDuplexEdge(1, 2, 1f);

            matrix.AddDuplexEdge(2, 3, 1f);

            matrix.AddDuplexEdge(3, 4, 1f);

            matrix.AddDuplexEdge(4, 5, 1f);

            matrix.AddDuplexEdge(5, 6, 1f);

            matrix.AddDuplexEdge(6, 1, 1f);


            return matrix;
        }

        private static AdjacencyMatrix CorrectBiGraph()
        {
            var matrix = new AdjacencyMatrix(17);
            matrix.AddDuplexEdge(1, 3, 1f);
            matrix.AddDuplexEdge(1, 4, 1f);

            matrix.AddDuplexEdge(2, 3, 1f);
            matrix.AddDuplexEdge(2, 15, 1f);

            matrix.AddDuplexEdge(3, 7, 1f);

            matrix.AddDuplexEdge(4, 5, 1f);
            matrix.AddDuplexEdge(4, 7, 1f);
            matrix.AddDuplexEdge(4, 14, 1f);

            matrix.AddDuplexEdge(5, 8, 1f);
            matrix.AddDuplexEdge(5, 13, 1f);

            matrix.AddDuplexEdge(6, 7, 1f);
            matrix.AddDuplexEdge(6, 10, 1f);
            matrix.AddDuplexEdge(6, 11, 1f);

            matrix.AddDuplexEdge(7, 9, 1f);
            matrix.AddDuplexEdge(7, 8, 1f);
            matrix.AddDuplexEdge(7, 13, 1f);

            matrix.AddDuplexEdge(8, 14, 1f);

            matrix.AddDuplexEdge(9, 10, 1f);

            matrix.AddDuplexEdge(11, 12, 1f);
            matrix.AddDuplexEdge(11, 15, 1f);
            matrix.AddDuplexEdge(11, 16, 1f);

            matrix.AddDuplexEdge(12, 17, 1f);

            matrix.AddDuplexEdge(13, 17, 1f);

            matrix.AddDuplexEdge(13, 17, 1f);


            return matrix;
        }

        private static AdjacencyMatrix NoPathMatrix()
        {
            var matrix = new AdjacencyMatrix(3);
            matrix.AddEdge(2, 3, 5f);


            return matrix;
        }
        private static AdjacencyMatrix ExampleMatrix()
        {
            var matrix = new AdjacencyMatrix(4);
            matrix.AddEdge(1, 2, 4f);

            matrix.AddEdge(2, 3, 3f);

            matrix.AddEdge(3, 1, 5f);

            matrix.AddEdge(2, 4, 3f);
            matrix.AddEdge(3, 4, 2f);
            matrix.AddEdge(1, 4, 1f);

            return matrix;
        }

    }
}
