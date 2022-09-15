using Core.ReactiveField;
using TMPro;
using UnityEngine;

namespace Game.View.UI
{
	public class ReactiveFieldTextView<T> : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _text;

		private ReactiveField<T> _reactiveField;

		public void Init(ReactiveField<T> reactiveField)
		{
			_reactiveField = reactiveField;
			reactiveField.OnChange += OnChange;
			OnChange(_reactiveField.Value);
		}

		private void OnChange(T value)
		{
			_text.text = value.ToString();
		}

		private void OnDestroy()
		{
			_reactiveField.OnChange -= OnChange;
		}
	}
}