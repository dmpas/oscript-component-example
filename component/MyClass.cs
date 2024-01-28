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

