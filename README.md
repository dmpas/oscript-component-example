# Как создать компонент для Односкрипта

1.  Создаём новый проект-библиотеку
2.  Подключаем NuGet пакеты "OneScript" и "OneScript.StandardLibrary" верси 2.0.0 и выше.
    Первый подключать обязательно, второй подключается для возможности использования
    встроенных типов Массив, ТаблицаЗначений и т.д.
3.  Подключаем модули:
        
        using ScriptEngine.Machine.Contexts;
        using ScriptEngine.Machine;
        using OneScript.StandardLibrary; // только если подключили OneScript.StandardLibrary
        
4.  Ставим на класс пометку `[ContextClass("МойКласс", "MyClass")]` и добавляем классу наследование от `AutoContext<MyClass>`
5.  Прописываем в класс конструктор

        [ScriptConstructor]
        public static MyClass Constructor()
        {
            return new MyClass();
        }

6.  После чего в коде можно использовать вызов вида

        ПодключитьВнешнююКомпоненту("oscript-component/bin/Debug/net6.0/oscript-component.dll");
        ОбъектМоегоКласса = Новый МойКласс;
