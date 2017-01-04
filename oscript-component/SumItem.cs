using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;

namespace oscriptcomponent
{
	[ContextClass("Слагаемое", "SumItem")]
	public class SumItem : AutoContext<SumItem>
	{
		private Decimal value;

		public SumItem(Decimal value)
		{
			this.value = value;
		}

		[ContextProperty("Значение", "Value")]
		public Decimal Value
		{
			get { return value; }
		}

		// можем переопределить строковое отображение наших объектов
		public override string ToString()
		{
			return string.Format("[Слагаемое: Значение={0}]", Value);
		}

		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor(IValue value)
		{
			// В value может быть не значение, а ссылка, содержащая значение.
			// Поэтому необходимо приводить входящие параметры к сырому значению.
			IValue rawValue = value.GetRawValue();

			// После этого можно оценивать, что за параметр пришёл

			if (rawValue.DataType == DataType.Number)
			{
				// Пришло число. Вызов вида "Новый Слагаемое(5)"
				return new SumItem(rawValue.AsNumber());
			}

			if (rawValue.DataType == DataType.Object)
			{
				// Пришло другое слагаемое. Вызов вида "Новый Слагаемое(ДругоеСлагаемое)"
				var inputItem = rawValue as SumItem;
				if (inputItem != null)
				{
					return new SumItem(inputItem.Value);
				}
			}

			// пришло нечто нам неведомое - бросаем исключение
			throw RuntimeException.InvalidArgumentType("value");
		}
	}
}

