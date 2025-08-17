/*----------------------------------------------------------
This Source Code Form is subject to the terms of the 
Mozilla Public License, v.2.0. If a copy of the MPL 
was not distributed with this file, You can obtain one 
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/
using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Collections.Generic;
using System.Collections;
using OneScript.Contexts;
using OneScript.Exceptions;
using OneScript.StandardLibrary.Collections;
using OneScript.Execution;

namespace oscriptcomponent
{
	/// <summary>
	/// Производит необходимое вычисление
	/// </summary>
	[ContextClass("Вычисление", "Calculation")]
	public class Calculation : AutoContext<Calculation>, ICollectionContext<CalcItem> // Скажем ОдноСкрипту, что это коллекция
	{
		// Возьмём Массив из стандартной библиотеки

		public Calculation()
		{
			Items = ArrayImpl.Constructor();

			// Также можно написать items = new ArrayIml();
			// но лучше вызвать предлагаемый библиотекой конструктор
		}

		// Здесь объявляется свойство только для чтения Слагаемые
		// Несмотря на то, что само свойство только для чтения, содержимое массива
		// можно изменять из кода:
		//     Сложение.Слагаемые.Добавить("Что угодно");

		/// <summary>
		/// Элементы
		/// </summary>
		[ContextProperty("Элементы", "Items")]
		public ArrayImpl Items { get; }

		/// <summary>
		/// Вид операции, производимый над элементами
		/// </summary>
		[ContextProperty("ВидОперации", "OperationType")]
		public OperationTypesEnum OperationType { get; set; }

		/// <summary>
		/// Добавляет новый элемент
		/// </summary>
		/// <param name="item">Число</param>
		[ContextMethod("ДобавитьЭлемент", "AddItem")]
		public void AddItem(IValue item)
		{
			var itemToAdd = CalcItem.Constructor(item);
			Items.Add(itemToAdd);
		}

		/// <summary>
		/// Выполняет вычисление
		/// </summary>
		/// <returns>Итог вычисления. Число</returns>
		[ContextMethod("Вычислить", "Calculate")]
		public decimal Calculate()
		{
			Decimal result = OperationType == OperationTypesEnum.Addition ? 0 : 1;
			foreach (var item in Items)
			{
				var sumItem = item as CalcItem;

				if (OperationType == OperationTypesEnum.Addition)
					result += sumItem.Value;
				else
					result *= sumItem.Value;
			}
			return result;
		}

		// Пример нескольких конструкторов

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		/// <returns>Вычисление</returns>
		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor()
		{
			return new Calculation();
		}

		/// <summary>
		/// На основании значений элементов.
		/// </summary>
		/// <param name="item1">Значение1. Число</param>
		/// <param name="item2">Значение2. Число</param>
		/// <returns>Вычисление</returns>
		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor(IValue item1, IValue item2)
		{
			var addition = new Calculation();
			addition.AddItem(item1);
			addition.AddItem(item2);
			return addition;
		}

		/// <summary>
		/// На основании Вычисления.
		/// </summary>
		/// <param name="calcSource">Копируемое вычисление</param>
		/// <returns>Вычисление</returns>
		/// <exception cref="RuntimeException"></exception>
		[ScriptConstructor]
		public static IRuntimeContextInstance Constructor(Calculation calcSource)
		{
			var addition = new Calculation();
			addition.OperationType = calcSource.OperationType;
			foreach (var item in calcSource.Items)
			{
				addition.Items.Add(item);
			}

			return addition;
		}

		#region Методы коллекции

		// Данные методы необходимы, чтобы ОдноСкрипт мог обходить объект циклом Для Каждого
		public int Count()
		{
			return Items.Count();
		}

		
		public CollectionEnumerator GetManagedIterator()
		{
			return new CollectionEnumerator(Items.GetEnumerator());
		}

		#endregion

		#region IEnumerable<>
		public IEnumerator<CalcItem> GetEnumerator()
		{
			foreach (var item in Items)
			{
				// ArrayImpl воплощает IEnumerable<IValue> - необходимо явно приводить к SumItem
				yield return (item as CalcItem);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

        public int Count(IBslProcess process) {
			return Count();
        }
        #endregion
    }
}

