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
			TestHelper helper = new TestHelper();
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

		#region TestTeardown
		[TearDown]
		public void Execute_Teardown()
		{
			DeleteField(NewFieldName, _workspaceId, _client);
		}
		#endregion

		#region Golden Flow

		[Test, Description("Golden Flow Unit Test")]
		public void Integration_Test_Golden_Flow_Valid()
		{
			//Arrange and Act
			QueryResult fieldQueryResult = GetField(NewFieldName, _workspaceId, _client);

			//Assert
			Assert.AreEqual(1, fieldQueryResult.TotalCount, "Count is not 1");
			Assert.IsNotNull(fieldQueryResult.QueryArtifacts[0].ArtifactID, "ArtifactID should not be null");
		}

		#endregion

		#region privte helpers

		public static QueryResult GetField(string fieldname, int workspaceID, IRSAPIClient _client)
		{
			_client.APIOptions.WorkspaceID = workspaceID;
			
			Query query = new Query();
			query.Condition = new TextCondition("Name", TextConditionEnum.Like, fieldname);
			QueryResult result = null;

			try
			{
				result = _client.Query(_client.APIOptions, query);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred getting field {fieldname}: {ex.Message}");
			}

			return result;
		}

		public static void DeleteField(string fieldname, int workspaceID, IRSAPIClient _client)
		{
			_client.APIOptions.WorkspaceID = workspaceID;
			QueryResult result = GetField(fieldname, workspaceID, _client);
			try
			{
				_client.Repositories.Field.DeleteSingle(result.QueryArtifacts[0].ArtifactID);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred deleting field {fieldname}: {ex.Message}");
			}
		}

		#endregion
	}
}
