/*----------------------------------------------------------
This Source Code Form is subject to the terms of the 
Mozilla Public License, v.2.0. If a copy of the MPL 
was not distributed with this file, You can obtain one 
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/
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
