using System;
using ScriptEngine;

namespace oscriptcomponent
{
	[EnumerationType("ВидыОперации", "OperationTypes")]
	public enum OperationTypesEnum
	{
		[EnumItem("Сложение", "Addition")]
		Addition,

		[EnumItem("Умножение", "Multiplication")]
		Multiplication
	}
}
