using System;
using kCura.Relativity.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Relativity.Test.Helpers;

namespace IntegrationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var WorkspaceId = 1018783;

            //Create instance of Test Helper & set up services manager and db context
            var helper = new TestHelper();
           var  ServicesManager = helper.GetServicesManager();
           var WorkspaceDbConext = helper.GetDBContext(WorkspaceId);

            //Create client
           var Client = helper.GetServicesManager().GetProxy<IRSAPIClient>(Relativity.Test.Helpers.SharedTestHelpers.ConfigurationHelper.ADMIN_USERNAME, Relativity.Test.Helpers.SharedTestHelpers.ConfigurationHelper.DEFAULT_PASSWORD);
        

    }
}
}
