using System;
using System.IO;
using NUnit.Framework;
using ScriptEngine.HostedScript;
using ScriptEngine.Machine;
using ScriptEngine.Environment;
using oscriptcomponent;

// Используется NUnit 3.6

namespace NUnitTests
{
	[TestFixture]
	public class MainTestClass
	{

		private EngineHelpWrapper host;

		[OneTimeSetUp]
		public void Initialize()
		{
			host = new EngineHelpWrapper();
			host.StartEngine();
		}

		[Test]
		public void TestAsInternalObjects()
		{
			var item1 = new SumItem(1);
			var item2 = new SumItem(2);
			var sum = new Addition();

			sum.AddItem(item1);
			sum.AddItem(item2);

			Assert.AreEqual(sum.Calculate(), ValueFactory.Create(3));

			sum.AddItem(new SumItem(3));
			Assert.AreEqual(sum.Calculate(), ValueFactory.Create(6));

			sum.AddItem(new SumItem(-1));
			Assert.AreEqual(sum.Calculate(), ValueFactory.Create(5));
		}

		[Test]
		public void TestAsInternalCollection()
		{
			var item1 = new SumItem(1);
			var item2 = new SumItem(2);
			var sum = new Addition();

			sum.AddItem(item1);
			sum.AddItem(item2);

			foreach (var item in sum)
			{
				// В случае, если Addition не воплощает IEnumerable,
				// этот цикл не скомпилируется
			}
		}

		[Test]
		public void TestAsExternalObjects()
		{
			host.RunTestScript("NUnitTests.Tests.external.os");
		}
	}
}
