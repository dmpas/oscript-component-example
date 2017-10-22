using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

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
		public static IRuntimeContextInstance Constructor()
		{
			return new MyClass();
		}
	}
}

