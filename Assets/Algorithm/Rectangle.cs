using UnityEngine;

namespace Algorithm
{
	[System.Serializable]
	public struct Rectangle
	{
		public Vector2 Min;
		public Vector2 Max;

		public bool Contains(Vector2 point)
		{
			return point.x >= Min.x && point.x <= Max.x && point.y >= Min.y && point.y <= Max.y;
		}

		public readonly bool Equals(Rectangle other)
		{
			return Min.Equals(other.Min) && Max.Equals(other.Max);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Min.GetHashCode() * 397) ^ Max.GetHashCode();
			}
		}
	}
}