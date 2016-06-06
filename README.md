# Как создать компонент для Односкрипта

1.  Создаём новый проект-библиотеку
2.  Подключаем NuGet пакет OneScript runtime core
3.  Подключаем модули:
        
        using ScriptEngine.Machine.Contexts;
        using ScriptEngine.Machine;
        
4.  Ставим на класс пометку `[ContextClass("МойКласс", "MyClass")]` и добавляем классу наследование от `AutoContext<MyClass>`
5.  Прописываем в класс конструктор

        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new MyClass();
        }
