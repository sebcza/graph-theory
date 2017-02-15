using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cechowanie
{
    public class Vertex
    {
        public string Label { get; set; }
        public Feature Feature { get; set; }
    }

    public class Edge
    {
        public Vertex UVertex { get; set; }

        public Vertex VVertex { get; set; }

        public int Value { get; set; }

        public bool IsRes { get; set; }

    }

    public class Feature
    {
        public int Value { get; set; }
        public char Direction { get; set; }
        public Vertex Previous { get; set; }
    }

    public class Graph
    {
        public int Fmax = 0;
        public List<Vertex> vertexs = new List<Vertex>();
        public List<Edge> edges = new List<Edge>();
        public void Calculate()
        {

            vertexs.Add(new Vertex()
            {
                Feature = null,
                Label = "S"
            });
            vertexs.Add(new Vertex()
            {
                Feature = null,
                Label = "1"
            });
            vertexs.Add(new Vertex()
            {
                Feature = null,
                Label = "2"
            });
            vertexs.Add(new Vertex()
            {
                Feature = null,
                Label = "3"
            });
            vertexs.Add(new Vertex()
            {
                Feature = null,
                Label = "4"
            });
            vertexs.Add(new Vertex()
            {
                Feature = null,
                Label = "T"
            });

            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "S"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "1"),

                Value = 10
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "S"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "3"),

                Value = 10
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "1"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "2"),

                Value = 4
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "1"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "3"),

                Value = 2
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "3"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "4"),

                Value = 9
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "1"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "4"),

                Value = 8
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "4"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "2"),

                Value = 6
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "4"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "2"),

                Value = 6
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "2"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "T"),

                Value = 10
            });
            edges.Add(new Edge()
            {
                UVertex = vertexs.FirstOrDefault(x => x.Label == "4"),
                VVertex = vertexs.FirstOrDefault(x => x.Label == "T"),

                Value = 10
            });

            while (true)
            {
                try
                {
                    Cechuj();
                    WypiszIZmienKrawedzie();
                }
                catch (Exception e)
                {
                    break;
                }

            }

            var result = vertexs.Where(x => x.Feature != null);
            Console.WriteLine("Max flow:" + Fmax);

        }

        private void WypiszIZmienKrawedzie()
        {
            Vertex startV = vertexs.FirstOrDefault(x => x.Label == "T");
            var capacity = startV.Feature.Value;
            Fmax += capacity;
            while (true)
            {
                Console.WriteLine(startV.Label);
                var v = startV.Feature.Previous;

                if (startV.Feature.Direction == '+')
                {
                    Edge edge2 = edges.FirstOrDefault(x => x.UVertex == startV && x.VVertex == v);
                    if (edge2 == null)
                    {
                        edges.Add(new Edge()
                        {
                            UVertex = startV,
                            VVertex = v,
                            Value = capacity,
                            IsRes = true
                        });
                    }
                    else
                    {
                        edge2.Value += capacity;
                    }
                }
                else
                {
                    Edge edge2 = edges.FirstOrDefault(x => x.UVertex == startV && x.VVertex == v);
                    if (edge2 == null)
                    {
                        edges.Add(new Edge()
                        {
                            UVertex = startV,
                            VVertex = v,
                            Value = capacity,
                            IsRes = true
                        });
                    }
                    else
                    {
                        edge2.Value -= capacity;
                    }
                }
                if (v.Label == "S")
                {
                    ; break;
                }
                startV = v;
            }

            vertexs.ForEach(x => x.Feature = null);
        }

        private void Cechuj()
        {
            var Q = new Queue<Vertex>();
            Q.Enqueue(vertexs.FirstOrDefault(x => x.Label == "S"));
            while (Q.Count > 0)
            {
                var vertex = Q.Dequeue();
                if (vertex.Label == "S")
                {
                    vertex.Feature = new Feature()
                    {
                        Previous = null,
                        Direction = '_',
                        Value = int.MaxValue // infinity
                    };
                }

                var childs = edges.Where(x => x.UVertex == vertex).Select(x => x.VVertex).ToList();

                for (int i = 0; i < childs.Count; i++)
                {
                    var x = childs[i];

                    Edge d = edges.FirstOrDefault(z => z.UVertex == x && z.VVertex == vertex);
                    var edge = edges.FirstOrDefault(z => z.UVertex == vertex && z.VVertex == x);
                    int residual;
                    if (d == null)
                    {
                        residual = edge.Value;
                    }
                    else
                    {
                        residual = edge.Value - d.Value;
                    }

                    if (residual == 0 || x.Feature != null) continue;

                    var residentualValues = new List<int>() { vertex.Feature.Value, residual };
                    //if (residual == 0 || x.Feature != null)
                    //{
                    //    continue;
                    //}
                    x.Feature = new Feature()
                    {
                        Value = residentualValues.Min(),
                        Previous = vertex,
                        Direction = edge.IsRes ? '-' : '+'
                    };

                    Q.Enqueue(x);
                }
            }
        }

    }
}
