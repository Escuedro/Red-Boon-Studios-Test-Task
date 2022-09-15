using System.Collections.Generic;
using UnityEngine;

namespace Algorithm
{
	public class Path
	{
		public List<Node> Nodes = new List<Node>();
		public float Length { get; private set; }
		
		public virtual void Bake()
		{
			List<Node> calculated = new List<Node>();
			Length = 0f;
			for (int i = 0; i < Nodes.Count; i++)
			{
				Node node = Nodes[i];
				for (int j = 0; j < node.Connections.Count; j++)
				{
					Node connection = node.Connections[j];

					if (Nodes.Contains(connection) && !calculated.Contains(connection))
					{
						Length += Vector3.Distance(node.Position, connection.Position);
					}
				}
				calculated.Add(node);
			}
		}
	}
}