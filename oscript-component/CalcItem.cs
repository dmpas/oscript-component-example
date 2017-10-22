using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace oscriptcomponent
{
	/// <summary>
	/// Определяет объект слагаемого в многочлене
	/// </summary>
	[ContextClass("ЭлементВычисления", "CalcItem")]
	public class CalcItem : AutoContext<CalcItem>
	{
		public CalcItem(Decimal value)
		{
			Value = value;
		}

		/// <summary>
		/// Значение элемента. Число
		/// </summary>
		[ContextProperty("Значение", "Value")]
		public Decimal Value { get; }

		// можем переопределить строковое отображение наших объектов
		public override string ToString()
		{
			return string.Format("[Слагаемое: Значение={0}]", Value);
		}

		/// <summary>
		/// По значению.
		/// </summary>
		/// <param name="value">Значение элемента. Число</param>
		/// <returns>ЭлементВычисления</returns>
		/// <exception cref="RuntimeException"></exception>
		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor(IValue value)
		{
			// В value может быть не значение, а ссылка, содержащая значение.
			// Поэтому необходимо приводить входящие параметры к сырому значению.
			IValue rawValue = value.GetRawValue();

			// После этого можно оценивать, что за параметр пришёл

			if (rawValue.DataType == DataType.Number)
			{
				// Пришло число. Вызов вида "Новый ЭлементВычисления(5)"
				return new CalcItem(rawValue.AsNumber());
			}

			if (rawValue.DataType == DataType.Object)
			{
				// Пришло другое слагаемое. Вызов вида "Новый ЭлементВычисления(ДругойЭлемент)"
				var inputItem = rawValue as CalcItem;
				if (inputItem != null)
				{
					return new CalcItem(inputItem.Value);
				}
			}

			// пришло нечто нам неведомое - бросаем исключение
			throw RuntimeException.InvalidArgumentType("value");
		}
	}
}

