using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Algorithm
{
	public class Graph
	{
		private readonly List<Node> _nodes;

		public Graph(List<Node> nodes)
		{
			_nodes = nodes;
		}

		public virtual Path GetShortestPath(Node start, Node end)
		{
			if (start == null || end == null)
			{
				throw new ArgumentNullException();
			}
			Path path = new Path();
			if (start == end)
			{
				path.Nodes.Add(start);
				return path;
			}

			List<Node> unvisited = new List<Node>();
			Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
			Dictionary<Node, float> distances = new Dictionary<Node, float>();

			for (int i = 0; i < _nodes.Count; i++)
			{
				Node node = _nodes[i];
				unvisited.Add(node);
				distances.Add(node, float.MaxValue);
			}

			distances[start] = 0f;
			while (unvisited.Count != 0)
			{
				unvisited = unvisited.OrderBy(node => distances[node]).ToList();
				Node current = unvisited[0];
				unvisited.Remove(current);
				if (current == end)
				{
					while (previous.ContainsKey(current))
					{
						path.Nodes.Insert(0, current);
						current = previous[current];
					}
					path.Nodes.Insert(0, current);
					break;
				}
				for (int i = 0; i < current.Connections.Count; i++)
				{
					Node neighbour = current.Connections[i];
					float length = Vector2.Distance(current.Position, neighbour.Position);
					float alt = distances[current] + length;
					if (alt < distances[neighbour])
					{
						distances[neighbour] = alt;
						previous[neighbour] = current;
					}
				}
			}
			path.Bake();
			return path;
		}
	}
}