using kCura.Relativity.Client;
using NUnit.Framework;
using Relativity.API;
using Relativity.Test.Helpers;
using Relativity.Test.Helpers.SharedTestHelpers;
using Relativity.Test.Helpers.WorkspaceHelpers;
using IServicesMgr = Relativity.Test.Helpers.Interface.IServicesMgr;

namespace AgentNunitIntegrationTest
{
	[TestFixture]
	public class AgentIntegrationTest
	{
		#region Variables

		public IRSAPIClient _client;
		public int _workspaceId;
		public IDBContext WorkspaceDbConext;
		public IServicesMgr ServicesManager;
		public IDBContext EddsDbContext;
		public int FieldArtifactId;
		public const string NewFieldName = "Demo Document Field";

		#endregion


		#region TestfixtureSetup

		[TestFixtureSetUp]
		public void Execute_TestFixtureSetup()
		{
			_workspaceId = 1017101; //CreateWorkspace.CreateWorkspaceAsync(_workspaceName, ConfigurationHelper.TEST_WORKSPACE_TEMPLATE_NAME, servicesManager, ConfigurationHelper.ADMIN_USERNAME, ConfigurationHelper.DEFAULT_PASSWORD).Result;

			//Create instance of Test Helper & set up services manager and db context
			var helper = new TestHelper();
			ServicesManager = helper.GetServicesManager();
			WorkspaceDbConext = helper.GetDBContext(_workspaceId);

			//Create client
			_client = helper.GetServicesManager().GetProxy<IRSAPIClient>(Relativity.Test.Helpers.SharedTestHelpers.ConfigurationHelper.ADMIN_USERNAME, Relativity.Test.Helpers.SharedTestHelpers.ConfigurationHelper.DEFAULT_PASSWORD);
		}

		#endregion

		#region TestfixtureTeardown

		[TestFixtureTearDown]
		public void Execute_TestFixtureTeardown()
		{
			//Delete Workspace
			DeleteWorkspace.DeleteTestWorkspace(_workspaceId, ServicesManager, ConfigurationHelper.ADMIN_USERNAME, ConfigurationHelper.DEFAULT_PASSWORD);

		}

		#endregion

		#region Golden Flow

		[Test, Description("Golden Flow Unit Test")]
		public void Integration_Test_Golden_Flow_Valid()
		{
			//Arrange and Act
			// Get the Field Artifact ID of "Demo Document Field"
			FieldArtifactId = Relativity.Test.Helpers.ArtifactHelpers.Fields.GetFieldArtifactID(NewFieldName, WorkspaceDbConext);

			//Get the field Count of the field.
			var fieldCount = Relativity.Test.Helpers.ArtifactHelpers.Fields.GetFieldCount(WorkspaceDbConext, FieldArtifactId);

			//Assert
			Assert.AreEqual(fieldCount, 1);
		}

		#endregion
	}
}
