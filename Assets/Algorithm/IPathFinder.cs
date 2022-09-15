using System.Collections.Generic;
using UnityEngine;

namespace Algorithm
{
	public interface IPathFinder
	{
		IEnumerable<Vector2> GetPath(Vector2 a, Vector2 b, IEnumerable<Edge> edges);
	}
}