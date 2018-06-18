// Исполняемое приложение для запуска компоненты под отладчиком

// В проекте TestApp в "Ссылки" ("References") должен быть добавлен проект компоненты
// В проекте TestApp должны быть подключены NuGet пакеты OneScript и OneScript.Library

using System;
using ScriptEngine.HostedScript;
using ScriptEngine.HostedScript.Library;

namespace TestApp
{
	class MainClass : IHostApplication
	{

		static readonly string SCRIPT = @"// Отладочный скрипт
// в котором уже подключена наша компонента
Слагаемое1 = Новый ЭлементВычисления(5);
Слагаемое2 = Новый ЭлементВычисления(Слагаемое1);

Складыватель = Новый Вычисление;
Складыватель.ДобавитьЭлемент(Слагаемое1);
Складыватель.ДобавитьЭлемент(Слагаемое2);
//Складыватель.ДобавитьЭлемент('20150601');

Сумма = Складыватель.Вычислить();

Сообщить(""Получилось: "" + Сумма);
"
			;

		public static HostedScriptEngine StartEngine()
		{
			var engine = new ScriptEngine.HostedScript.HostedScriptEngine();
			engine.Initialize();

			// Тут можно указать любой класс из компоненты
			engine.AttachAssembly(System.Reflection.Assembly.GetAssembly(typeof(oscriptcomponent.MyClass)));

			// Если проектов компонент несколько, то надо взять по классу из каждой из них
			// engine.AttachAssembly(System.Reflection.Assembly.GetAssembly(typeof(oscriptcomponent_2.MyClass_2)));
			// engine.AttachAssembly(System.Reflection.Assembly.GetAssembly(typeof(oscriptcomponent_3.MyClass_3)));

			return engine;
		}

		public static void Main(string[] args)
		{
			var engine = StartEngine();
			var script = engine.Loader.FromString(SCRIPT);
			var process = engine.CreateProcess(new MainClass(), script);

			var result = process.Start(); // Запускаем наш тестовый скрипт

			Console.WriteLine("Result = {0}", result);

			// ВАЖНО: движок перехватывает исключения, для отладки можно пользоваться только точками останова.
		}

		public void Echo(string str, MessageStatusEnum status = MessageStatusEnum.Ordinary)
		{
			Console.WriteLine(str);
		}

		public void ShowExceptionInfo(Exception exc)
		{
			Console.WriteLine(exc.ToString());
		}

		public bool InputString(out string result, int maxLen)
		{
			throw new NotImplementedException();
		}

		public string[] GetCommandLineArguments()
		{
			return new string[] { "1", "2", "3" }; // Здесь можно зашить список аргументов командной строки
		}
	}
}
