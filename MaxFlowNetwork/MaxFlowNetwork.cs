using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxFlowNetwork
{

    public class BigraphResult
    {
        public bool IsBigraph { get; set; }

        public int[] Colors { get; set; }
    }

    public class ResultMatrix
    {
        public float Fmax { get; set; }

        public AdjacencyMatrix Matrix { get; set; }
    }

    public class MaxFlowNetwork
    {
        public ResultMatrix Calculate(AdjacencyMatrix graph, int s, int t)
        {
            float fMax = 0;
            var fMatrix = new AdjacencyMatrix(graph.Order);
            while (true)
            {            
                var queueVertex = new Queue<int>();
                var prev = new int[graph.Order];
                var cfp = new float[graph.Order];
                bool breakWhile = false;

                for (int i = 0; i < prev.Length; i++)
                {
                    prev[i] = -1;
                }
                prev[s] = -2;
                cfp[s] = float.PositiveInfinity;
                queueVertex.Enqueue(s);

                while (queueVertex.Count > 0)
                {
                    var x = queueVertex.Dequeue();
                    foreach (var y in graph.Neighbours(x + 1))
                    {
                        var nettoValue = 0f;
                        if (fMatrix.IsEdgeByLabels(x + 1, y))
                            nettoValue = fMatrix.GetEdgeByLabels(x + 1, y).Weight;

                        var residualCapacity = graph.GetEdgeByLabels(x + 1, y).Weight - nettoValue;

                        if (residualCapacity == 0f || prev[y - 1] != -1)
                            continue;

                        prev[y - 1] = x;
                        cfp[y - 1] = Math.Min(cfp[x], residualCapacity);

                        if ((y - 1) == t)
                        {
                            breakWhile = true;
                            break;
                        }
                        queueVertex.Enqueue(y - 1);
                    }
                    if (breakWhile)
                    {
                        break;
                    }
                }
                if (!breakWhile)
                {
                    return new ResultMatrix() {Fmax = fMax, Matrix = fMatrix};
                }

                fMax += cfp[t];

                int tt = t;
                while (tt != s)
                {
                    var x = prev[tt];

                    if (!fMatrix.IsEdgeByLabels(x + 1, tt + 1))
                        fMatrix.AddEdge(x + 1, tt + 1, 0f);
                    fMatrix.GetEdgeByLabels(x + 1, tt + 1).Weight += cfp[t];

                    if (!fMatrix.IsEdgeByLabels(tt + 1, x + 1))
                        fMatrix.AddEdge(tt + 1, x + 1, 0f);

                    fMatrix.GetEdgeByLabels(tt + 1, x + 1).Weight -= cfp[t];

                    Console.WriteLine("nettoValue(" + (x + 1) + "," + (tt + 1) + ") == " +
                                      fMatrix.GetEdgeByLabels(x + 1, tt + 1).Weight);

                    tt = x;
                }
                Console.WriteLine("Kolejny obieg");
            }

        }

        public BigraphResult isBipartiale(AdjacencyMatrix graph, int s)
        {
            var colorArr = new int[graph.Order];

            for (int i = 0; i < graph.Order; i++)
            {
                colorArr[i] = -1;
            }
            colorArr[s] = 1;

            var q = new Queue<int>();
            q.Enqueue(s);

            while (q.Count != 0)
            {
                int u = q.Dequeue();

                for (int v = 0; v < graph.Order; ++v)
                {
                    if (graph.IsEdge(u, v) && colorArr[v] == -1)
                    {
                        colorArr[v] = 1 - colorArr[u];
                        q.Enqueue(v);
                    }
                    else if(graph.IsEdge(u, v) && colorArr[v] == colorArr[u])
                    {
                        return new BigraphResult() {Colors = null, IsBigraph = false };
                    }
                }
            }
            for (int i = 0; i < colorArr.Length; i++)
            {
                Console.WriteLine(i + ": " + colorArr[i]);
            }

            return new BigraphResult() { Colors = colorArr, IsBigraph = true };
        }

        public void CalculateBigraphAssociation(AdjacencyMatrix graph, int s, BigraphResult br)
        {
            var biggerGraph = new AdjacencyMatrix(graph.Order+2);
            var q = new List<int>();
            var blue = new List<int>();
            var red = new List<int>();

            for (int i = s; i < br.Colors.Length; i++)
            {
                if (br.Colors[i] == 0)
                {
                    biggerGraph.AddEdge(1, i + 2, 1f);
                    q.Add(i+2);
                    blue.Add(i+1);
                }
                else
                {
                    biggerGraph.AddEdge(i + 2, biggerGraph.Order, 1f);
                    red.Add(i+1);
                }
            } //Połączenie source z niebieskimi i czerownych z target

            Console.WriteLine(red + " " + blue);

            foreach (var VARIABLE in blue)
            {
                var neigh = graph.Neighbours((VARIABLE));
                neigh.ForEach(n => biggerGraph.AddEdge(VARIABLE+1, n+1, 1f));
            }



            var res = Calculate(biggerGraph, 0, biggerGraph.Order - 1);
            var rs = res.Matrix.Edges.Where(x => blue.Any(z=>z == x.VertexA) && x.Weight==1);

        }
    }
}
