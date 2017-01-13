using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxFlowNetwork
{
    public class Edge
    {
        public int VertexA;
        public int VertexB;
        public float Weight;
    }
    public class AdjacencyMatrix
    {
        protected List<List<double?>> matrix;
        public List<Edge> Edges;
        public int Order { get { return matrix.Count; } }

        public AdjacencyMatrix(int verticleCount)
        {
            Edges = new List<Edge>();
            matrix = new List<List<double?>>();
            for (int i = 0; i < verticleCount; i++)
            {
                AddVertex();
            }
        }

        public int AddVertex()
        {
            matrix.Add(Enumerable.Repeat<double?>(null, matrix.Count).ToList());
            foreach (var item in matrix)
            {
                item.Add(null);
            }

            return matrix.Count;
        }

        public void AddEdge(int vertexA, int vertexB, float weight)
        {
            if (weight < 0)
            {
                Console.WriteLine("ERROR! Weight must be positive! This weight is " + weight);
                return;
            }
            vertexA -= 1;
            vertexB -= 1;
            matrix[vertexA][vertexB] = weight;

            Edges.Add(new Edge() { VertexA = vertexA, VertexB = vertexB, Weight = weight });
        }

        public void AddDuplexEdge(int vertexA, int vertexB, float weight)
        {
            if (weight < 0)
            {
                Console.WriteLine("ERROR! Weight must be positive! This weight is " + weight);
                return;
            }
            vertexA -= 1;
            vertexB -= 1;
            matrix[vertexA][vertexB] = weight;
            matrix[vertexB][vertexA] = weight;

            Edges.Add(new Edge() { VertexA = vertexA, VertexB = vertexB, Weight = weight });
            Edges.Add(new Edge() { VertexA = vertexB, VertexB = vertexA, Weight = weight });
        }

        public AdjacencyMatrix Transpose()
        {
            AdjacencyMatrix graph = new AdjacencyMatrix(matrix.Count);
            foreach (var item in Edges)
            {
                graph.AddEdge(item.VertexB + 1, item.VertexA + 1, item.Weight);
            }
            return graph;
        }

        public void PrintAdjacency()
        {
            Console.WriteLine("Adjacency Matrix:");

            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (matrix[i][j] != null)
                        Console.Write(matrix[i][j] + " ");
                    else
                        Console.Write(". ");

                }
                Console.WriteLine();
            }
        }

        public List<int> Neighbours(int vertex)
        {
            var neighbours = new List<int>();

            for (int i = 0; i < matrix[vertex - 1].Count; i++)
            {
                if (matrix[vertex - 1][i] != null)
                    neighbours.Add(i + 1);
            }
            return neighbours;
        }

        public Edge GetEdge(int vertexA, int vertexB)
        {
            return Edges.Find(e => e.VertexA == vertexA && e.VertexB == vertexB);
        }

        public bool IsEdge(int vertexA, int vertexB)
        {
            var edge = Edges.Find(e => e.VertexA == vertexA && e.VertexB == vertexB);
            if (edge != null)
                return true;

            return false;
        }

        public Edge GetEdgeByLabels(int vertexA, int vertexB)
        {
            return Edges.Find(e => e.VertexA == vertexA - 1 && e.VertexB == vertexB - 1);
        }

        public bool IsEdgeByLabels(int vertexA, int vertexB)
        {
            var edge = Edges.Find(e => e.VertexA == vertexA - 1 && e.VertexB == vertexB - 1);
            if (edge != null)
                return true;

            return false;
        }
    }
}