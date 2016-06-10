using System;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using ScriptEngine.HostedScript.Library;

namespace oscriptcomponent
{
    [ContextClass("Сложение", "Addition")]
    public class Addition : AutoContext<Addition>
    {
        // Возьмём Массив из стандартной библиотеки
        private ArrayImpl items;

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
            get  { return items; }
        }

        [ContextMethod("ДобавитьСлагаемое", "AddItem")]
        public void AddItem(IValue item)
        {
            var itemToAdd = SumItem.Constructor(item);
            items.Add(ValueFactory.Create(itemToAdd));
        }

        [ContextMethod("Вычислить", "Calculate")]
        public IValue Calculate()
        {
            Decimal result = 0;
            foreach (var item in items)
            {
                var sumItem = item as SumItem;
                result += sumItem.Value;
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

    }
}

