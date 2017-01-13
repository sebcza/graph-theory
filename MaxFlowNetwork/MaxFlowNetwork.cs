using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFlowNetwork
{
    public class MaxFlowNetwork
    {
        public float? Calculate(AdjacencyMatrix graph, int s, int t)
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
                    return fMax;
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
            }
        }
    }
}
