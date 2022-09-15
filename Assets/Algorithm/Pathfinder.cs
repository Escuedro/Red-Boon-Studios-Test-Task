using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Algorithm
{
	public class Pathfinder : IPathFinder
	{

		private List<Rectangle> _rectangles = new List<Rectangle>();

		private Rectangle? _startRectangle;
		private Rectangle? _finishRectangle;

		private Dictionary<Rectangle, List<Node>> _nodesByRectangle = new Dictionary<Rectangle, List<Node>>();
		private Dictionary<Edge, Node> _nodeByEdge = new Dictionary<Edge, Node>();

		public IEnumerable<Vector2> GetPath(Vector2 a, Vector2 b, IEnumerable<Edge> edges)
		{
			Edge[] edgeArray = edges.ToArray();
			RegisterRectangles(edgeArray);
			if (IsPathPossible(a, b))
			{
				GetStartAndFinishRectangles(a, b);
				if (_finishRectangle != null && _startRectangle != null && _startRectangle.Value.Equals(_finishRectangle.Value))
				{
					yield return a;
					yield return b;
				}
				else if (_startRectangle.HasValue && _finishRectangle.HasValue)
				{
					List<Node> nodes = GetNodes(a, b, edgeArray);
					Graph graph = new Graph(nodes);
					Path path = graph.GetShortestPath(nodes[0], nodes.Last());
					foreach (Node pathNode in path.Nodes)
					{
						yield return pathNode.Position;
					}
				}
			}
			else
			{
				Debug.LogError("Path is impossible to create. Check input edges");
				yield break;
			}
		}

		private List<Node> GetNodes(Vector2 start, Vector2 finish, Edge[] allEdges)
		{
			RegisterNodes(allEdges);
			List<Node> nodes = new List<Node>();
			Node startNode = new Node(start);
			startNode.Connections.AddRange(_nodesByRectangle[_startRectangle.Value]);
			Node finishNode = new Node(finish);
			finishNode.Connections.AddRange(_nodesByRectangle[_finishRectangle.Value]);
			nodes.AddRange(_nodeByEdge.Values);
			nodes.Insert(0, startNode);
			foreach (Node node in _nodesByRectangle[_startRectangle.Value])
			{
				node.AddNode(startNode);
			}
			nodes.Add(finishNode);
			foreach (Node node in _nodesByRectangle[_finishRectangle.Value])
			{
				node.AddNode(finishNode);
			}
			return nodes;
		}

		private void RegisterNodes(Edge[] allEdges)
		{
			_nodesByRectangle.Clear();
			_nodeByEdge.Clear();
			foreach (Rectangle rectangle in _rectangles)
			{
				_nodesByRectangle.Add(rectangle, new List<Node>());
			}
			foreach (Edge edge in allEdges)
			{
				Node node = new Node(edge.GetMiddlePoint());
				_nodesByRectangle[edge.First].Add(node);
				_nodesByRectangle[edge.Second].Add(node);
				_nodeByEdge.Add(edge, node);
			}
			foreach (KeyValuePair<Edge,Node> edgeNodePair in _nodeByEdge)
			{
				foreach (Node node in _nodesByRectangle[edgeNodePair.Key.First])
				{
					edgeNodePair.Value.AddNode(node);
				}
				foreach (Node node in _nodesByRectangle[edgeNodePair.Key.Second])
				{
					edgeNodePair.Value.AddNode(node);
				}
			}
		}

		private Node[] EdgesToNodes(Edge[] edges)
		{
			Node[] nodes = new Node[edges.Length];
			for (int i = 0; i < edges.Length; i++)
			{
				nodes[i] = new Node(edges[i].GetMiddlePoint());
			}
			return nodes;
		}

		private Edge[] GetEdgesInRectangle(Rectangle rectangle, Edge[] allEdges)
		{
			List<Edge> edges = new List<Edge>();
			foreach (Edge edge in allEdges)
			{
				if (edge.First.Equals(rectangle) || edge.Second.Equals(rectangle))
				{
					edges.Add(edge);
				}
			}
			return edges.ToArray();
		}

		private void GetStartAndFinishRectangles(Vector2 start, Vector2 finish)
		{
			_startRectangle = null;
			_finishRectangle = null;
			foreach (Rectangle rectangle in _rectangles)
			{
				if (rectangle.Contains(start))
				{
					_startRectangle = rectangle;
				}
				if (rectangle.Contains(finish))
				{
					_finishRectangle = rectangle;
				}
				if (_startRectangle.HasValue && _finishRectangle.HasValue)
				{
					return;
				}
			}
			if (!_startRectangle.HasValue || !_finishRectangle.HasValue)
			{
				throw new Exception("Not found rectangles for points");
			}
		}

		private bool IsPathPossible(Vector2 start, Vector2 finish)
		{
			bool containsStart = false;
			bool containsEnd = false;

			foreach (Rectangle rectangle in _rectangles)
			{
				containsStart |= rectangle.Contains(start);
				containsEnd |= rectangle.Contains(finish);
				if (containsStart && containsEnd)
				{
					return true;
				}
			}

			return false;
		}

		private void RegisterRectangles(Edge[] edges)
		{
			_rectangles.Clear();
			foreach (Edge edge in edges)
			{
				if (!_rectangles.Contains(edge.First))
				{
					_rectangles.Add(edge.First);
				}
				if (!_rectangles.Contains(edge.Second))
				{
					_rectangles.Add(edge.Second);
				}
			}
		}
		//
		// private bool IsIntersecting(Vector2 startA, Vector2 endA, Vector2 startB, Vector2 endB)
		// {
		// 	bool isIntersecting = false;
		//
		// 	Vector2 aDirection = (endA - startA).normalized;
		// 	Vector2 bDirection = (endB - startB).normalized;
		//
		// 	Vector2 aNormal = new Vector2(-aDirection.y, aDirection.x);
		// 	Vector2 bNormal = new Vector2(-bDirection.y, bDirection.x);
		//
		// 	float a = aNormal.x;
		// 	float b = aNormal.y;
		//
		// 	float c = bNormal.x;
		// 	float d = bNormal.y;
		//
		// 	float k1 = (a * startA.x) + (b * startA.y);
		// 	float k2 = (c * startB.x) + (d * startB.y);
		//
		// 	if (IsParallel(aNormal, bNormal))
		// 	{
		// 		return false;
		// 	}
		// 	if (IsOrthogonal(startA - startB, aNormal))
		// 	{
		// 		return false;
		// 	}
		//
		// 	float xIntersect = (d * k1 - b * k2) / (a * d - b * c);
		// 	float yIntersect = (-c * k1 + a * k2) / (a * d - b * c);
		//
		// 	Vector2 intersectPoint = new Vector2(xIntersect, yIntersect);
		//
		// 	if (IsBetween(startA, endA, intersectPoint) && IsBetween(startB, endB, intersectPoint))
		// 	{
		// 		isIntersecting = true;
		// 	}
		//
		// 	return isIntersecting;
		// }
		//
		// private bool IsParallel(Vector2 v1, Vector2 v2)
		// {
		// 	if (Vector2.Angle(v1, v2) == 0f || Math.Abs(Vector2.Angle(v1, v2) - 180f) < Tolerance)
		// 	{
		// 		return true;
		// 	}
		//
		// 	return false;
		// }
		//
		// private bool IsOrthogonal(Vector2 v1, Vector2 v2)
		// {
		// 	if (Mathf.Abs(Vector2.Dot(v1, v2)) < Tolerance)
		// 	{
		// 		return true;
		// 	}
		// 	return false;
		// }
		//
		// private bool IsBetween(Vector2 start, Vector2 end, Vector2 pointToCheck)
		// {
		// 	bool isBetween = false;
		//
		// 	Vector2 ab = end - start;
		// 	Vector2 ac = pointToCheck - start;
		//
		// 	if (Vector2.Dot(ab, ac) > 0f && ab.sqrMagnitude >= ac.sqrMagnitude)
		// 	{
		// 		isBetween = true;
		// 	}
		//
		// 	return isBetween;
		// }
	}
}