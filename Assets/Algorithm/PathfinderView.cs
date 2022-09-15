using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Algorithm
{
	public class PathfinderView : MonoBehaviour
	{
		[SerializeField]
		private RectTransform _rectPrefab;
		[SerializeField]
		private RectPointView _rectPointPrefab;
		[SerializeField]
		private RectTransform _edgePrefab;
		[SerializeField]
		private RectTransform _pathSegmentPrefab;
		[SerializeField]
		private Canvas _canvas;
		[SerializeField]
		private Rectangle[] _rectangles;
		[SerializeField]
		private Edge[] _edges;
		[SerializeField]
		private Vector2 _pointStart;
		[SerializeField]
		private Vector2 _pointEnd;

		private IPathFinder _pathFinder = new Pathfinder();

		private void Start()
		{
			DrawRectangles();
			DrawEdges();
			DrawStartAndFinalPoints();
			CalculatePath();
		}

		private void CalculatePath()
		{
			IEnumerable<Vector2> points = _pathFinder.GetPath(_pointStart, _pointEnd, _edges);
			IEnumerable<Vector2> enumerable = points as Vector2[] ?? points.ToArray();
			Vector2[] pointsArray = enumerable.ToArray();
			for (int i = 0; i < pointsArray.Length - 1; i++)
			{
				RectTransform pathSegmentInstance = Instantiate(_pathSegmentPrefab, _canvas.transform);
				SetEdgeTransform(pointsArray[i], pointsArray[i+1], pathSegmentInstance, 10f);
			}
		}

		private void DrawStartAndFinalPoints()
		{
			RectPointView pointStart = Instantiate(_rectPointPrefab, _canvas.transform);
			pointStart.SetPosition(_pointStart);
			pointStart.SetColor(Color.green);
			RectPointView pointEnd = Instantiate(_rectPointPrefab, _canvas.transform);
			pointEnd.SetPosition(_pointEnd);
			pointEnd.SetColor(Color.red);
		}

		private void DrawRectangles()
		{
			foreach (Rectangle rectangle in _rectangles)
			{
				RectTransform rectangleInstance = Instantiate(_rectPrefab, _canvas.transform);
				Vector2 rectCoords = new Vector2((rectangle.Min.x + rectangle.Max.x) / 2,
						(rectangle.Min.y + rectangle.Max.y) / 2);
				float width = rectangle.Max.x - rectangle.Min.x;
				float height = rectangle.Max.y - rectangle.Min.y;
				rectangleInstance.anchoredPosition = rectCoords;
				rectangleInstance.sizeDelta = new Vector2(width, height);
				// RectPointView pointMin = Instantiate(_rectPointPrefab, _canvas.transform);
				// pointMin.SetPosition(rectangle.Min);
				// RectPointView pointMax = Instantiate(_rectPointPrefab, _canvas.transform);
				// pointMax.SetPosition(rectangle.Max);
			}
		}

		private void DrawEdges()
		{
			foreach (Edge edge in _edges)
			{
				RectTransform edgeInstance = Instantiate(_edgePrefab, _canvas.transform);
				SetEdgeTransform(edge.Start, edge.End, edgeInstance, 5f);
			}
		}

		private void SetEdgeTransform(Vector2 start, Vector2 end, RectTransform edgeRect, float edgeWidth)
		{
			Vector2 edgeCoords = (start + end) / 2;
			float angle = GetAngle(start, end);
			edgeRect.anchoredPosition = edgeCoords;
			edgeRect.eulerAngles = new Vector3(0f, 0f, angle);
			edgeRect.sizeDelta = new Vector2(Vector2.Distance(start, end), edgeWidth);
		}

		private float GetAngle(Vector2 start, Vector2 end)
		{
			Vector2 differenceVector = end - start;
			return Mathf.Atan2(differenceVector.y, differenceVector.x)*Mathf.Rad2Deg;
		}
	}
}