/*----------------------------------------------------------
This Source Code Form is subject to the terms of the 
Mozilla Public License, v.2.0. If a copy of the MPL 
was not distributed with this file, You can obtain one 
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/
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
		public CalcItem(decimal value)
		{
			Value = value;
		}

		/// <summary>
		/// Значение элемента. Число
		/// </summary>
		[ContextProperty("Значение", "Value")]
		public decimal Value { get; }

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
			if (value is BslNumericValue)
			{
				// Пришло число. Вызов вида "Новый ЭлементВычисления(5)"
				return new CalcItem(value.AsNumber());
			}

			if (value is CalcItem inputItem)
			{
				// Пришло другое слагаемое. Вызов вида "Новый ЭлементВычисления(ДругойЭлемент)"
				return new CalcItem(inputItem.Value);
			}

			// пришло нечто нам неведомое - бросаем исключение
			throw RuntimeException.InvalidArgumentType("value");
		}
	}
}

