using System.Collections.Generic;
using UnityEngine;

namespace Algorithm
{
	public class Node
	{
		public Vector2 Position;

		public Node(Vector2 position)
		{
			Position = position;
		}

		public List<Node> Connections = new List<Node>();

		public Node this[int index] => Connections[index];

		public void AddNode(Node node)
		{
			if (node.Position != Position)
			{
				Connections.Add(node);
			}
		}
	}
}