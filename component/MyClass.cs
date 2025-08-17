/*----------------------------------------------------------
This Source Code Form is subject to the terms of the 
Mozilla Public License, v.2.0. If a copy of the MPL 
was not distributed with this file, You can obtain one 
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/
using System;
using OneScript.Contexts;
using ScriptEngine.Machine.Contexts;

namespace oscriptcomponent
{
	/// <summary>
	/// Некоторый класс
	/// </summary>
	[ContextClass("МойКласс", "MyClass")]
	public class MyClass : AutoContext<MyClass>
	{
		public MyClass()
		{
		}

		/// <summary>
		/// Некоторое свойство только для чтения.
		/// </summary>
		[ContextProperty("СвойствоДляЧтения", "ReadonlyProperty")]
		public string ReadonlyProperty
		{
			get
			{
				return "MyValue";
			}
		}

		/// <summary>
		/// Некоторый конструктор
		/// </summary>
		/// <returns></returns>
		[ScriptConstructor]
		public static MyClass Constructor()
		{
			return new MyClass();
		}
	}
}

