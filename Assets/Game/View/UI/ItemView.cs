using System;
using Game.Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.View.UI
{
	public class ItemView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
	{
		[SerializeField]
		private Image _icon;
		[SerializeField]
		private TextMeshProUGUI _costText;
		[SerializeField]
		private CanvasGroup _canvasGroup;

		public Item Item { get; private set; }

		public CanvasGroup CanvasGroup => _canvasGroup;

		private Action<ItemView, Vector2> _onClick;
		private Action<ItemView> _onRelease;
		private Action<Vector2> _onDrag;

		public void Init(Item item, int cost)
		{
			Item = item;
			_icon.sprite = item.Icon;
			_costText.text = cost.ToString();
		}

		public void SetOnClick(Action<ItemView, Vector2> onClick)
		{
			_onClick = onClick;
		}

		public void SetOnRelease(Action<ItemView> onRelease)
		{
			_onRelease = onRelease;
		}

		public void SetOnDrag(Action<Vector2> onDrag)
		{
			_onDrag = onDrag;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_onClick?.Invoke(this, eventData.position);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_onRelease?.Invoke(this);
		}

		public void OnDrag(PointerEventData eventData)
		{
			_onDrag?.Invoke(eventData.position);
		}
	}
}