/*----------------------------------------------------------
This Source Code Form is subject to the terms of the 
Mozilla Public License, v.2.0. If a copy of the MPL 
was not distributed with this file, You can obtain one 
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/
using OneScript.Compilation;
using OneScript.Contexts;
using OneScript.Execution;
using OneScript.Sources;
using OneScript.StandardLibrary.Collections;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using System;
using System.Collections.Generic;

namespace NUnitTests
{
    [ContextClass("ВнешнийТест")]
    public class ExternalTest : AutoScriptDrivenObject<ExternalTest>
    {
        public ExternalTest(IExecutableModule module) : base(module) {

        }

        public List<ExternalTestCase> GetTestCases(ExternalTestRunner runner, IBslProcess process) {

            var result = new List<ExternalTestCase>();

            var methodId = GetMethodNumber("ПолучитьСписокТестов");
            CallAsFunction(methodId, new IValue[] { runner }, out var ivTests, process);
            if (ivTests is ArrayImpl array) {
                foreach (var ivTest in array) {
                    result.Add(new ExternalTestCase(this, ivTest.ExplicitString()));
                }
            }
            return result;
        }

        public void RunTest(string methodName, IBslProcess process) {
            var methodId = GetMethodNumber(methodName);
            CallAsProcedure(methodId, Array.Empty<IValue>(), process);
        }

        public static ExternalTest Create(ICompilerFrontend compiler, SourceCode src, IBslProcess process) {
            var module = CompileModule(compiler, src, typeof(ExternalTest), process);
            return new ExternalTest(module);
        }

    }
}
