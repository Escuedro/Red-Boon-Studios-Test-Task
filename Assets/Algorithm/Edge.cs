using UnityEngine;

namespace Algorithm
{
	[System.Serializable]
	public struct Edge
	{
		public Rectangle First;
		public Rectangle Second;
		public Vector2 Start;
		public Vector2 End;

		public Vector2 GetMiddlePoint()
		{
			return Vector2.Lerp(Start, End, 0.5f);
		}
	}
}