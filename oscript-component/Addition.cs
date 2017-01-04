using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;
using System.Collections.Generic;
using System.Collections;

namespace oscriptcomponent
{
	[ContextClass("Сложение", "Addition")]
	public class Addition : AutoContext<Addition>
	, ICollectionContext   // Скажем ОдноСкрипту, что это коллекция
	, IEnumerable<SumItem> // IEnumerable<> нужен, чтобы работать с классом как с коллекцией из C#

	/*
	 * До 15-й версии движка ICollectionContext включает в себя IEnumerable<IValue>.
	 * Начиная с 15-й версии IEnumerable<> необходимо указывать явно с нужным типом элемента
	*/
	{
		// Возьмём Массив из стандартной библиотеки
		private readonly ArrayImpl items;

		public Addition()
		{
			items = ArrayImpl.Constructor() as ArrayImpl;

			// Также можно написать items = new ArrayIml();
			// но лучше вызвать предлагаемый библиотекой конструктор
		}

		// Здесь объявляется свойство только для чтения Слагаемые
		// Несмотря на то, что само свойство только для чтения, содержимое массива
		// можно изменять из кода:
		//     Сложение.Слагаемые.Добавить("Что угодно");

		[ContextProperty("Слагаемые", "Items")]
		public ArrayImpl Items
		{
			get { return items; }
		}

		[ContextProperty("ВидОперации", "OperationType")]
		public OperationTypesEnum OperationType { get; set; }

		[ContextMethod("ДобавитьСлагаемое", "AddItem")]
		public void AddItem(IValue item)
		{
			var itemToAdd = SumItem.Constructor(item);
			items.Add(ValueFactory.Create(itemToAdd));
		}

		[ContextMethod("Вычислить", "Calculate")]
		public IValue Calculate()
		{
			Decimal result = OperationType == OperationTypesEnum.Addition ? 0 : 1;
			foreach (var item in items)
			{
				var sumItem = item as SumItem;

				if (OperationType == OperationTypesEnum.Addition)
					result += sumItem.Value;
				else
					result *= sumItem.Value;
			}
			return ValueFactory.Create(result);
		}

		// Пример нескольких конструкторов

		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor()
		{
			return new Addition();
		}

		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor(IValue item1, IValue item2)
		{
			var addition = new Addition();
			addition.AddItem(item1);
			addition.AddItem(item2);
			return addition;
		}

		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor(IValue oldAdditionParam)
		{
			var oldAddition = oldAdditionParam.GetRawValue() as Addition;
			if (oldAddition == null)
			{
				throw RuntimeException.InvalidArgumentType();
			}

			var addition = new Addition();
			foreach (var item in oldAddition.items)
			{
				addition.items.Add(item);
			}

			return addition;
		}

		#region Методы коллекции

		// Данные методы необходимы, чтобы ОдноСкрипт мог обходить объект циклом Для Каждого
		public int Count()
		{
			return ((ICollectionContext)items).Count();
		}

		public CollectionEnumerator GetManagedIterator()
		{
			return ((ICollectionContext)items).GetManagedIterator();
		}

		#endregion

		#region IEnumerable<>
		public IEnumerator<SumItem> GetEnumerator()
		{
			foreach (var item in items)
			{
				// ArrayImpl воплощает IEnumerable<IValue> - необходимо явно приводить к SumItem
				yield return (item as SumItem);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}

