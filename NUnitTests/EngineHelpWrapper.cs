/*----------------------------------------------------------
This Source Code Form is subject to the terms of the 
Mozilla Public License, v.2.0. If a copy of the MPL 
was not distributed with this file, You can obtain one 
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/
using OneScript.Execution;
using OneScript.StandardLibrary;
using OneScript.StandardLibrary.Collections;
using ScriptEngine;
using ScriptEngine.HostedScript;
using ScriptEngine.HostedScript.Extensions;
using ScriptEngine.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

namespace NUnitTests
{
	public class EngineHelpWrapper : IHostApplication
	{
		public EngineHelpWrapper()
		{
            Engine = DefaultEngineBuilder.Create()
				.SetupConfiguration(providers => { })
				.SetDefaultOptions()
                .UseImports()
                .UseNativeRuntime()
                .UseFileSystemLibraries()
				.Build();

            // Регистрируем сборку по имени любого из стандартных классов движка
            Engine.AttachAssembly(System.Reflection.Assembly.GetAssembly(typeof(ArrayImpl)));

            // Тут можно указать любой класс из компоненты
            Engine.AttachExternalAssembly(System.Reflection.Assembly.GetAssembly(typeof(oscriptcomponent.MyClass)));
            // Если проектов компонент несколько, то надо взять по классу из каждой из них
            // engine.AttachAssembly(System.Reflection.Assembly.GetAssembly(typeof(oscriptcomponent_2.MyClass_2)));
            // engine.AttachAssembly(System.Reflection.Assembly.GetAssembly(typeof(oscriptcomponent_3.MyClass_3)));

            Hosted = new HostedScriptEngine(Engine);
            Hosted.Initialize();

            var cs = Engine.GetCompilerService();
            var testRunnerSource = Engine.Loader.FromString(LoadCodeFromAssemblyResource("NUnitTests.Tests.testrunner.os"));

            Process = Engine.NewProcess();
            TestRunner = ExternalTestRunner.Create(cs, testRunnerSource, Process);
        }

		private ScriptingEngine Engine { get; }

		private ExternalTestRunner TestRunner { get; }

		public IBslProcess Process { get; }

		private HostedScriptEngine Hosted {  get; }

		public void AddTestCases(List<ExternalTestCase> list, string resourceName) {
            var testSource = Engine.Loader.FromString(LoadCodeFromAssemblyResource(resourceName));
            var cs = Engine.GetCompilerService();
            var test = ExternalTest.Create(cs, testSource, Process);
            var testArray = test.GetTestCases(TestRunner, Process);
			list.AddRange(testArray);
        }

		private static string LoadCodeFromAssemblyResource(string resourceName)
		{
			var asm = System.Reflection.Assembly.GetExecutingAssembly();
			using var resourceStream = asm.GetManifestResourceStream(resourceName) ??
			                           throw new NullReferenceException(resourceName);
			using var reader = new StreamReader(resourceStream);
			var codeSource = reader.ReadToEnd();
			return codeSource;
		}

		public void Echo(string str, MessageStatusEnum status = MessageStatusEnum.Ordinary)
		{
			Console.WriteLine(str);
		}

		public bool InputString(out string result, string prompt, int maxLen, bool multiline)
		{
			throw new NotImplementedException();
		}

		public string[] GetCommandLineArguments()
		{
			return new string[] { };
		}

		public bool InputString(out string result, int maxLen)
		{
			result = "";
			return false;
		}

		public void ShowExceptionInfo(Exception exc)
		{
		}
	}
}
