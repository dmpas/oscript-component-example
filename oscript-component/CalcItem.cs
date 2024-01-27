using System;
using OneScript.Contexts;
using OneScript.Exceptions;
using OneScript.Values;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;

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
		public static CalcItem Constructor(IValue value)
		{
			// В value может быть не значение, а ссылка, содержащая значение.
			// Поэтому необходимо приводить входящие параметры к сырому значению.
			IValue rawValue = value.GetRawValue();

			// После этого можно оценивать, что за параметр пришёл

			if (rawValue is BslNumericValue)
			{
				// Пришло число. Вызов вида "Новый ЭлементВычисления(5)"
				return new CalcItem(rawValue.AsNumber());
			}

			if (rawValue is CalcItem inputItem)
			{
				// Пришло другое слагаемое. Вызов вида "Новый ЭлементВычисления(ДругойЭлемент)"
				return new CalcItem(inputItem.Value);
			}

			// пришло нечто нам неведомое - бросаем исключение
			throw RuntimeException.InvalidArgumentType("value");
		}
	}
}

