using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Algorithm
{
	public class RectPointView : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _coordsText;
		[SerializeField]
		private RectTransform _rectTransform;
		[SerializeField]
		private Image _imageComponent;

		public void SetPosition(Vector2 position)
		{
			_rectTransform.anchoredPosition = position;
			_coordsText.text = $"[X:{position.x};Y:{position.y}]";
		}

		public void SetColor(Color color)
		{
			_imageComponent.color = color;
		}
	}
}