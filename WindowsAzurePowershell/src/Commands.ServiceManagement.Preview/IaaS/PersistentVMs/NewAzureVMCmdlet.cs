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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.Preview.IaaS.PersistentVMs
{
    using System.Management.Automation;
    using ServiceManagement.IaaS.PersistentVMs;
    using Utilities.Common;

    [Cmdlet(VerbsCommon.New, "AzureVM", DefaultParameterSetName = "ExistingService"), OutputType(typeof(ManagementOperationContext))]
    public class NewAzureVMCmdlet : NewAzureVMCommand
    {
        [Parameter(HelpMessage = "The name of the reserved IP.")]
        [ValidateNotNullOrEmpty]
        public override string ReservedIPName
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            ServiceManagementPreviewProfile.Initialize();
            base.ProcessRecord();
        }
    }
}
