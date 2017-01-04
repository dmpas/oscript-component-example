using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace oscriptcomponent
{
	[ContextClass("МойКласс", "MyClass")]
	public class MyClass : AutoContext<MyClass>
	{
		public MyClass()
		{
		}

		[ContextProperty("СвойствоДляЧтения", "ReadonlyProperty")]
		public string ReadonlyProperty
		{
			get
			{
				return "MyValue";
			}
		}

		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor()
		{
			return new MyClass();
		}
	}
}

