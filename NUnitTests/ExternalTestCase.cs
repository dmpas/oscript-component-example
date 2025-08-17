/*----------------------------------------------------------
This Source Code Form is subject to the terms of the 
Mozilla Public License, v.2.0. If a copy of the MPL 
was not distributed with this file, You can obtain one 
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/

using OneScript.Execution;

namespace NUnitTests
{
    public class ExternalTestCase
    {
        public readonly ExternalTest test;
        public readonly string testName;

        public ExternalTestCase(ExternalTest test, string testName) {
            this.test = test;
            this.testName = testName;
        }

        public void Run(IBslProcess process) {
            test.RunTest(testName, process);
        }

        public override string ToString() {
            return testName;
        }

    }
}
