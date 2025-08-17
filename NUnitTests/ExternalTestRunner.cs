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
using ScriptEngine.Machine.Contexts;

namespace NUnitTests
{
    [ContextClass("ЗапускательТестов")]
    public class ExternalTestRunner : AutoScriptDrivenObject<ExternalTestRunner>
    {
        public ExternalTestRunner(IExecutableModule module) : base(module) {

        }
        public static ExternalTestRunner Create(ICompilerFrontend compiler, SourceCode src, IBslProcess process) {
            var module = CompileModule(compiler, src, typeof(ExternalTestRunner), process);
            return new ExternalTestRunner(module);
        }

    }
}
