using UnityEngine;

namespace Game.Utils.Extensions
{
	public static class TransformExtensions
	{
		public static void DestroyChildren(this Transform transform)
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				Transform child = transform.GetChild(i);
				Object.Destroy(child);
			}
		}
	}
}