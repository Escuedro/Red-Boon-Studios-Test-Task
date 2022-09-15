using System;
using System.Collections.Generic;

namespace Core.ReactiveField
{
	public class ReactiveField<T>
	{
		public Action<T> OnChange;

		private T _value;
		private readonly EqualityComparer<T> _equalityComparer = EqualityComparer<T>.Default;

		public ReactiveField(T value)
		{
			_value = value;
		}

		public ReactiveField(T value, EqualityComparer<T> equalityComparer)
		{
			_value = value;
			_equalityComparer = equalityComparer;
		}

		public ReactiveField()
		{
		}

		public T Value
		{
			get => _value;
			set
			{
				if (!_equalityComparer.Equals(value, _value))
				{
					_value = value;
					OnChange?.Invoke(_value);
				}
			}
		}
	}
}