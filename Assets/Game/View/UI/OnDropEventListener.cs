using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.View.UI
{
	public class OnDropEventListener : MonoBehaviour, IDropHandler
	{
		[SerializeField]
		private Image _image;
		[SerializeField]
		private Color _enabledColor;
		[SerializeField]
		private Color _disabledColor;

		private Action _onDropAction;

		public void SetOnDrop(Action onDropAction)
		{
			_onDropAction = onDropAction;
		}
		public void OnDrop(PointerEventData eventData)
		{
			_onDropAction?.Invoke();
		}

		public void SetState(bool canInteract)
		{
			_image.color = canInteract ? _enabledColor : _disabledColor;
		}
	}
}