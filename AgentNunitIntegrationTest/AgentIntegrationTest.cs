using kCura.Relativity.Client;
using NUnit.Framework;
using Relativity.API;
using Relativity.Test.Helpers;
using Relativity.Test.Helpers.ServiceFactory.Extentions;
using Relativity.Test.Helpers.SharedTestHelpers;
using System;

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
		public const string NewFieldName = "Demo Document Field";

		#endregion

		#region TestfixtureSetup

		[TestFixtureSetUp]
		public void Execute_TestFixtureSetup()
		{
			//_workspaceId = CreateWorkspace.CreateWorkspaceAsync(_workspaceName, ConfigurationHelper.TEST_WORKSPACE_TEMPLATE_NAME, servicesManager, ConfigurationHelper.ADMIN_USERNAME, ConfigurationHelper.DEFAULT_PASSWORD).Result;
			_workspaceId = 1017097;

			//Create instance of Test Helper & set up services manager and db context
			var helper = new TestHelper();
			ServicesManager = helper.GetServicesManager();
			WorkspaceDbConext = helper.GetDBContext(_workspaceId);

			//Create client
			_client = helper.GetServicesManager().GetProxy<IRSAPIClient>(ConfigurationHelper.ADMIN_USERNAME, ConfigurationHelper.DEFAULT_PASSWORD);
		}

		#endregion

		#region TestfixtureTeardown

		[TestFixtureTearDown]
		public void Execute_TestFixtureTeardown()
		{
			//Delete Workspace
			//DeleteWorkspace.DeleteTestWorkspace(_workspaceId, ServicesManager, ConfigurationHelper.ADMIN_USERNAME, ConfigurationHelper.DEFAULT_PASSWORD);
		}

		#endregion

		#region Golden Flow

		[Test, Description("Golden Flow Unit Test")]
		public void Integration_Test_Golden_Flow_Valid()
		{
			//Arrange and Act
			var field = GetField(NewFieldName, _workspaceId, _client);

			//Get the field Count of the field.
			//int fieldCount = Relativity.Test.Helpers.ArtifactHelpers.Fields.GetFieldCount(WorkspaceDbConext, fieldArtifactId);

			//Assert
			Assert.AreEqual(field.TotalCount, 1, "Count is not 1");
			Assert.IsNotNull(field.QueryArtifacts[0].ArtifactID, "ArtifactID should not be null");
		}

		#endregion

		#region privte helpers

		public static QueryResult GetField(string fieldname, int workspaceID, IRSAPIClient _client)
		{
			var query = new Query
			{
				ArtifactTypeID = (int)ArtifactType.Field,
				Condition = new TextCondition("Name", TextConditionEnum.Like, fieldname)
			};
			QueryResult result = null;

			try
			{
				result = _client.Query(_client.APIOptions, query);
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occurred: {0}", ex.Message);
			}

			return result;
		}

		#endregion
	}
}
