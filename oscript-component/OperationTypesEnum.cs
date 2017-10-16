using System;
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
		[EnumItem("Сложение", "Addition")]
		Addition,

		/// <summary>
		/// Рассчитыват произведение элементов
		/// </summary>
		[EnumItem("Умножение", "Multiplication")]
		Multiplication
	}
}
