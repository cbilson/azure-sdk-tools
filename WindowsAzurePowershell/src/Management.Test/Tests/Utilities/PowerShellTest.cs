﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Management.CloudService.Test.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using Microsoft.WindowsAzure.Management.Test.Tests.Utilities;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PowerShellTest
    {
        public static string ErrorIsNotEmptyException = "Test failed due to a non-empty error stream, check the error stream in the test log for more details";

        protected PowerShell powershell;
        protected List<string> modules;

        public PowerShellTest(params string[] modules)
        {
            this.modules = new List<string>();
            this.modules.Add("Azure.psd1");
            this.modules.Add("Assert.ps1");
            this.modules.AddRange(modules);
        }

        protected void AddScenarioScript(string script)
        {
            powershell.AddScript(Testing.GetTestResourceContents(script));
        }

        public virtual Collection<PSObject> RunPowerShellTest(params string[] scripts)
        {
            Collection<PSObject> output = null;
            foreach (string script in scripts)
            {
                Console.WriteLine(script);
                powershell.AddScript(script);
            }
            try
            {
                output = powershell.Invoke();
                
                if (powershell.HadErrors || powershell.Streams.Error.Count > 0)
                {
                    throw new RuntimeException(ErrorIsNotEmptyException);
                }

                return output;
            }
            catch (Exception psException)
            {
                powershell.LogPowerShellException(psException);
                throw;
            }
            finally
            {
                powershell.LogPowerShellResults(output);
            }
        }

        [TestInitialize]
        public virtual void TestSetup()
        {
            powershell = PowerShell.Create();

            foreach (string moduleName in modules)
            {
                powershell.AddScript(string.Format("Import-Module \"{0}\"", Testing.GetTestResourcePath(moduleName)));
            }

            powershell.AddScript("$VerbosePreference='Continue'");
            powershell.AddScript("$DebugPreference='Continue'");
            powershell.AddScript("$ErrorActionPreference='Stop'");
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            powershell.Dispose();
        }
    }
}
