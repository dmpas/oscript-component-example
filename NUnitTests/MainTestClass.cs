/*----------------------------------------------------------
This Source Code Form is subject to the terms of the 
Mozilla Public License, v.2.0. If a copy of the MPL 
was not distributed with this file, You can obtain one 
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/
using NUnit.Framework;
using oscriptcomponent;
using System.Collections.Generic;

namespace NUnitTests
{
	[TestFixture]
	public class MainTestClass
	{

		private static readonly string[] TestResourceNames = { "NUnitTests.Tests.external.os" };
		private static readonly EngineHelpWrapper host = new();

        [OneTimeSetUp]
		public static void Initialize()
		{
        }

        [Test]
		public void TestAsInternalObjects()
		{
			var item1 = new CalcItem(1);
			var item2 = new CalcItem(2);
			var sum = new Calculation();

			sum.AddItem(item1);
			sum.AddItem(item2);

			Assert.AreEqual(sum.Calculate(), 3);

			sum.AddItem(new CalcItem(3));
			Assert.AreEqual(sum.Calculate(), 6);

			sum.AddItem(new CalcItem(-1));
			Assert.AreEqual(sum.Calculate(), 5);
		}

		[Test]
		public void TestAsInternalCollection()
		{
			var item1 = new CalcItem(1);
			var item2 = new CalcItem(2);
			var sum = new Calculation();

			sum.AddItem(item1);
			sum.AddItem(item2);

			foreach (var item in sum)
			{
				// В случае, если Addition не воплощает IEnumerable,
				// этот цикл не скомпилируется
			}
		}

		[Test, TestCaseSource("GetExternalTests")]
		public void TestAsExternalObjects(ExternalTestCase testCase)
		{
			testCase.Run(host.Process);
		}

		private static ExternalTestCase[] GetExternalTests() {
            
			var list = new List<ExternalTestCase>();
			foreach (var resourceName in TestResourceNames) {
				host.AddTestCases(list, resourceName);
			}

			return list.ToArray();
		}
	}
}
