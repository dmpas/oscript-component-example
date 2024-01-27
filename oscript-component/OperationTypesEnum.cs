using System;
using OneScript.Contexts.Enums;
using ScriptEngine;

namespace oscriptcomponent
{
	/// <summary>
	/// Перечисление ВидыОперации
	/// </summary>
	[EnumerationType("ВидыОперации", "OperationTypes")]
	public enum OperationTypesEnum
	{
		/// <summary>
		/// Рассчитывать сумму элементов
		/// </summary>
		[EnumValue("Сложение", "Addition")]
		Addition,

		/// <summary>
		/// Рассчитывает произведение элементов
		/// </summary>
		[EnumValue("Умножение", "Multiplication")]
		Multiplication
	}
}
